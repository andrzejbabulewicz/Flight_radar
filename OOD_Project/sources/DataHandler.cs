using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace OOD_Project
{
    public class DataHandler
    {
        public List<Airport> airports { get; set; } = [];
        public List<Flight> flights { get; set; } = [];

        public List<PassengerPlane> passengerPlanes { get; set; } = [];

        public List<CargoPlane> cargoPlanes { get; set; } = [];

        public Dictionary<string, Func<string[], object>> Shortcuts = new()
        {
            ["C"] = Crew.CreateCrew,
            ["P"] = Passenger.CreatePassenger,
            ["CA"] = Cargo.CreateCargo,
            ["CP"] = CargoPlane.CreateCargoPlane,
            ["PP"] = PassengerPlane.CreatePassengerPlane,
            ["AI"] = Airport.CreateAirport,
            ["FL"] = Flight.CreateFlight
        };

        public List<object> ReadData(List<string> Stringlist)
        {
            List<string> CopyList = new List<string>(Stringlist);
            List<object> TempData = new();

            foreach(var line in CopyList)
            {
                string[] Entries = line.Split(',');
                
                if (Entries.Length > 0)
                {
                    string init = Entries[0];
                    if (Shortcuts.ContainsKey(init))
                    {
                        TempData.Add(Shortcuts[init](Entries));

                        if(init == "AI")
                        {
                            airports.Add(Airport.CreateAirport(Entries));
                        }
                        if(init=="FL")
                        {
                            flights.Add(Flight.CreateFlight(Entries));
                        }
                        if(init=="PP")
                        {
                            passengerPlanes.Add(PassengerPlane.CreatePassengerPlane(Entries));
                        }
                        if(init=="CP")
                        {
                            cargoPlanes.Add(CargoPlane.CreateCargoPlane(Entries));
                        }
                    }
                }
            }

            return TempData;
        }

        public void SerializeData(List<object> List, string OutputPath)
        {
            string Json = JsonSerializer.Serialize(List, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(OutputPath, Json);
        }

        public List<string> ConvertFileToList(string input)
        {
            List<string> data = File.ReadAllLines(input).ToList();
            return data;
        }

        
    }
}