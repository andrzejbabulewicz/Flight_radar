using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOD_Project
{
    public class Television : IMediable
    {
        public string Name { get; set; }

        public Television(string name)
        {
            this.Name = name;
        }

        public string Report(Airport airport)
        {
            return $"<An image of {airport.Name} airport>";
        }

       public string Report(CargoPlane cargoPlane)
        {
            return $"<An image of {cargoPlane.Serial} cargo plane>";
        }

        public string Report(PassengerPlane passengerPlane)
        {
            return $"<An image of {passengerPlane.Serial} passenger plane>";
        }
    }
}
