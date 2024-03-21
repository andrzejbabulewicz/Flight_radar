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

        public void ThreadHandler(string InputFilePath, string OutputFilePath, int minDelay, int maxDelay)
        {
            NetworkSourceSimulator.NetworkSourceSimulator networkSource = 
                new NetworkSourceSimulator.NetworkSourceSimulator(InputFilePath, minDelay, maxDelay);

            networkSource.OnNewDataReady += this.NetworkSource_OnNewDataReady;
            Thread dataSourceThread = new Thread(networkSource.Run);
            dataSourceThread.Start();

            ShowPlanes showPlanes = new ShowPlanes(this);
            //MakeSnapshots(OutputFilePath);            
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

        public void MakeSnapshots(string OutputFilePath)
        {
            
            while (true)
            {
                Console.WriteLine("print/exit:");
                string? TerminalCommand = Console.ReadLine();

                if (TerminalCommand == "print")
                {
                    List<string> StringList = GetData();

                    DataHandler dataHandler = new();                    
                    List<object> ObjectList = dataHandler.ReadData(StringList);
                    string SnapPath = OutputFilePath;
                    SnapPath += "snapshot_" + DateTime.Now.ToString("HH_mm_ss") + ".json";
                    dataHandler.SerializeData(ObjectList, SnapPath);
                }
                else if (TerminalCommand == "exit")
                {
                    Environment.Exit(0);
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

    }
}
