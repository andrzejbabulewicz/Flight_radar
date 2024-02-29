using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOD_Project
{
    public class Airport
    {
        public string Type { get; set; } = "AI";

        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public Single Longitude { get; set; }
        public Single Latitude { get; set; }
        public Single Amsl { get; set; }
        public string Country { get; set; }

        public Airport(int id, string name, string code, Single longitude, Single latitude, Single aMSL, string country)
        {
            Id = id;
            Name = name;
            Code = code;
            Longitude = longitude;
            Latitude = latitude;
            Amsl = aMSL;
            Country = country;
        }
    }
}