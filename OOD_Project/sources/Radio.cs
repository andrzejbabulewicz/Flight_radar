using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOD_Project
{
    internal class Radio : IMediable
    {
        public string Name { get; set; }

        public Radio(string name)
        {
            this.Name = name;
        }
        public string Report(Airport airport)
        {
            return $"Reporting for {this.Name}, Ladies and Gentlemen, we are at the {airport.Name} airport";
        }

        public string Report(CargoPlane cargoPlane)
        {
            return $"Reporting for {this.Name}, Ladies and Gentlemen, we are seeing the {cargoPlane.Serial} aircraft fly above us.";
        }

        public string Report(PassengerPlane passengerPlane)
        {
            return $"Reporting for {this.Name}, Ladies and Gentlemen, we've just whitnessed {passengerPlane.Serial} take off";
        }
    }
}
