using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOD_Project
{
    internal class Newspaper : IMediable
    {
        public string Name { get; set; }

        public Newspaper(string name)
        {
            this.Name = name;
        }

        public string Report(Airport airport)
        {
            return $"{this.Name} - a report from the {airport.Name} airport, {airport.Country}";
        }

        public string Report(CargoPlane cargoPlane)
        {
            return $"{this.Name} - an interview with the crew of {cargoPlane.Serial}";
        }

        public string Report(PassengerPlane passengerPlane)
        {
            return $"{this.Name} - Breaking news! {passengerPlane.Model} aircraft loses EASA fails certification after inspection of {passengerPlane.Serial}";
        }
    }
}
