using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOD_Project
{
    public class CargoPlane
    {
        public string Type { get; set; } = "CP";
        public int Id { get; set; }
        public string Serial { get; set; }
        public string Country { get; set; }
        public string Model { get; set; }
        public Single MaxLoad { get; set; }

        public CargoPlane(int iD, string serial, string country, string model, Single maxLoad)
        {
            Id = iD;
            Serial = serial;
            Country = country;
            Model = model;
            MaxLoad = maxLoad;
        }
    }
}