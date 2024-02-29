using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace OOD_Project
{
    public class DataHandler
    {
        public List<object> List = [];

        public Dictionary<string, Func<string[], object>> Shortcuts = new()
        {
            ["C"] = CreateCrew,
            ["P"] = CreatePassenger,
            ["CA"] = CreateCargo,
            ["CP"] = CreateCargoPlane,
            ["PP"] = CreatePassengerPlane,
            ["AI"] = CreateAirport,
            ["FL"] = CreateFlight
        };

        public void ReadData(string input)
        {
            List<string> data = new();

            data = File.ReadAllLines(input).ToList();

            foreach (var line in data)
            {
                string[] Entries = line.Split(',');
                if (Entries.Length > 0)
                {
                    string init = Entries[0];
                    if (Shortcuts.ContainsKey(init))
                    {
                        List.Add(Shortcuts[init](Entries));
                    }
                }
            }
        }

        public void SerializeData(string output)
        {
            string Json = JsonSerializer.Serialize(List, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(output, Json);
        }

        public static object CreateCrew(string[] data)
        {
            return new Crew(int.Parse(data[1]), data[2], int.Parse(data[3]), data[4], data[5], int.Parse(data[6]),
                data[7]);
        }

        public static object CreatePassenger(string[] data)
        {
            return new Passenger(int.Parse(data[1]), data[2], int.Parse(data[3]), data[4], data[5], data[6],
                int.Parse(data[7]));
        }

        public static object CreateCargo(string[] data)
        {
            return new Cargo(int.Parse(data[1]), Single.Parse(data[2], CultureInfo.InvariantCulture), data[3], data[4]);
        }

        public static object CreateCargoPlane(string[] data)
        {
            return new CargoPlane(int.Parse(data[1]), data[2], data[3], data[4],
                Single.Parse(data[5], CultureInfo.InvariantCulture));
        }

        public static object CreatePassengerPlane(string[] data)
        {
            return new PassengerPlane(int.Parse(data[1]), data[2], data[3], data[4], int.Parse(data[5]),
                int.Parse(data[6]), int.Parse(data[7]));
        }

        public static object CreateAirport(string[] data)
        {
            return new Airport(int.Parse(data[1]), data[2], data[3],
                Single.Parse(data[4], CultureInfo.InvariantCulture),
                Single.Parse(data[5], CultureInfo.InvariantCulture),
                Single.Parse(data[6], CultureInfo.InvariantCulture), data[7]);
        }

        public static object CreateFlight(string[] data)
        {
            int[] crewid = Array.ConvertAll(data[10].Trim('[', ']').Split(';'), int.Parse);
            int[] loadid = Array.ConvertAll(data[11].Trim('[', ']').Split(';'), int.Parse);
            return new Flight(int.Parse(data[1]), int.Parse(data[2]), int.Parse(data[3]), data[4], data[5],
                Single.Parse(data[6], CultureInfo.InvariantCulture),
                Single.Parse(data[7], CultureInfo.InvariantCulture),
                Single.Parse(data[8], CultureInfo.InvariantCulture), int.Parse(data[9]), crewid, loadid);
        }
    }
}