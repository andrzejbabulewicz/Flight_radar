using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOD_Project
{
    public class PassengerPlane
    {
        public string Type { get; set; } = "PP";

        public int ID { get; set; }
        public string Serial { get; set; }
        public string Country { get; set; }
        public string Model { get; set; }
        public int FirstClassSize { get; set; }
        public int BusinessClassSize { get; set; }
        public int EconomyClassSize { get; set; }

        public PassengerPlane(int id, string serial, string country, string model, int firstClassSize,
            int businessClassSize, int economyClassSize)
        {
            ID = id;
            Serial = serial;
            Country = country;
            Model = model;
            FirstClassSize = firstClassSize;
            BusinessClassSize = businessClassSize;
            EconomyClassSize = economyClassSize;
        }
    }
}