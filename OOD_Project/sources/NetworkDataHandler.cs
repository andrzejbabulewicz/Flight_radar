using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Unicode;
using System.Threading.Tasks;
using FlightTrackerGUI;
using NetworkSourceSimulator;
using OOD_Project.sources;

namespace OOD_Project
{
    public sealed class NetworkDataHandler
    {
        public static string OutputPath = @"..\..\..\outputs\testing.txt";
        public List<string> Data { get; set; } = [];
        public List<AirportObjects> airportObjects { get; set; } = [];
        public DataHandler dataHandler { get; set; } 

        public Dictionary<string, Func<BinaryReader, string>> ShortcutTypes= new()
        {
            ["NCR"] = Crew.CreateStringCrew,
            ["NPA"] = Passenger.CreateStringPassenger,
            ["NCA"] = Cargo.CreateStringCargo,
            ["NCP"] = CargoPlane.CreateStringCargoPlane,
            ["NPP"] = PassengerPlane.CreateStringPassengerPlane,
            ["NAI"] = Airport.CreateStringAirport,
            ["NFL"] = Flight.CreateStringFlight
        };

        public NetworkDataHandler(DataHandler dataHandler)
        {
            this.dataHandler = dataHandler;
        }
        public void ThreadHandler(string InputFilePath, int minDelay, int maxDelay)
        {
            NetworkSourceSimulator.NetworkSourceSimulator networkSource = 
                new NetworkSourceSimulator.NetworkSourceSimulator(InputFilePath, minDelay, maxDelay);

            networkSource.OnNewDataReady += this.NetworkSource_OnNewDataReady;
            networkSource.OnIDUpdate += this.NetworkSource_OnIDUpdate;
            networkSource.OnPositionUpdate += this.NetworkSource_OnPositionUpdate;
            networkSource.OnContactInfoUpdate += this.NetworkSource_OnContactInfoUpdate;

            Thread dataSourceThread = new Thread(networkSource.Run);
            dataSourceThread.Start();

            ShowPlanes showPlanes = new ShowPlanes(this);
            
            
        }

        public void NetworkSource_OnContactInfoUpdate(object sender, ContactInfoUpdateArgs e)
        {
            
            if (airportObjects.Find(x => x.Id == e.ObjectID) == null)
            {
                WriteToFile($"can't change the contact info - no ID {e.ObjectID} found!");
            }
            else
            {
                int index = airportObjects.FindIndex(x => x.Id == e.ObjectID);
                airportObjects[index].Phone = e.PhoneNumber;
                airportObjects[index].Email = e.EmailAddress;
                WriteToFile($"contact info  of {airportObjects[index].Id} updated: " +
                    $"Phone: {e.PhoneNumber}, email: {e.EmailAddress}");
            }
        }
        public void NetworkSource_OnPositionUpdate(object sender, PositionUpdateArgs e)
        {
            DataFlightHandler dataFlightHandler = new();
            if(airportObjects.Find(x => x.Id == e.ObjectID)==null)
            //if (obj == null)
            {
                WriteToFile($"can't change the position - no ID {e.ObjectID} found!");
            }
            else
            {
                int index = airportObjects.FindIndex(x => x.Id == e.ObjectID);
                
                int flightIndex = dataHandler.flights.FindIndex(x => x.Id == e.ObjectID);
                if(flightIndex >= 0)
                {
                    dataHandler.flights[flightIndex].Longitude = e.Longitude;
                    dataHandler.flights[flightIndex].Latitude = e.Latitude;
                    dataHandler.flights[flightIndex].AMSL = e.AMSL;
                    dataFlightHandler.PrintFlightsFTR(dataHandler);

                }


                WriteToFile($"position of {airportObjects[index].Id} updated:" +
                    $"Longitude: {e.Longitude}, Latitude: {e.Latitude}, AMSL: {e.AMSL}");
            }           
        }
        public void NetworkSource_OnIDUpdate(object sender, IDUpdateArgs e)
        {
            
            if(airportObjects.Find(x => x.Id == e.ObjectID) == null)
            {
                WriteToFile($"can't update the ID - no ID {e.ObjectID} found!");
            }
            else
            {                
                int index = airportObjects.FindIndex(x => x.Id == e.ObjectID);
                ulong oldId = airportObjects[index].Id;
                airportObjects[index].Id = e.NewObjectID;
                WriteToFile($"ID Updated from {oldId} to {airportObjects[index].Id}");
            }
           
        }
        public void NetworkSource_OnNewDataReady(object sender, NewDataReadyArgs e)
        { 
            Message message = ((NetworkSourceSimulator.NetworkSourceSimulator)sender).GetMessageAt(e.MessageIndex);

            byte[] messageData = message.MessageBytes;
            
            using (var stream = new MemoryStream(messageData))
            {
                using (var reader = new BinaryReader(stream))
                {
                    char[] ShortcutChars = reader.ReadChars(3);
                    UInt32 MessageLength = reader.ReadUInt32();
                    string ShortcutString = new string(ShortcutChars);
                    if(ShortcutTypes.ContainsKey(ShortcutString))
                    {
                        this.Data.Add(ShortcutTypes[ShortcutString](reader));
                        if (ShortcutString == "NFL" || ShortcutString == "NAI")
                        {
                            List<string> copy = GetData();
                            OnShortcutStringFound(new ShortcutStringEventArgs(copy));
                        }
                    }
                }
            }            
        }

        public void MakeSnapshots(string OutputFilePath, NewsGenerator newsGenerator)
        {
            
            while (true)
            {
                string? TerminalCommand = Console.ReadLine();

                if (TerminalCommand == "print")
                {
                    List<string> StringList = GetData();

                    DataHandler dataHandler = new();
                    List<AirportObjects> ObjectList = dataHandler.ReadData(StringList);
                    string SnapPath = OutputFilePath;
                    SnapPath += "snapshot_" + DateTime.Now.ToString("HH_mm_ss") + ".json";
                    dataHandler.SerializeData(ObjectList, SnapPath);
                }
                else if (TerminalCommand == "exit")
                {
                    Environment.Exit(0);
                }
                else if (TerminalCommand == "report")
                {
                    newsGenerator.GenerateNextNews();
                }
                else
                {
                    Console.WriteLine("Invalid command");
                }
            }
        }
        public List<string> GetData()
        {
            return this.Data;
        }

        public event EventHandler<ShortcutStringEventArgs> ShortcutStringFound;
        public void OnShortcutStringFound(ShortcutStringEventArgs e)
        {
            EventHandler<ShortcutStringEventArgs> handler = ShortcutStringFound;
            handler?.Invoke(this, e);
        }

        private void WriteToFile(string text)
        {
            string path = FilePaths.LogFilePath;
            using (StreamWriter writer = new StreamWriter(path, true))
            {
                writer.WriteLine(text);
            }

        }


    }


}

