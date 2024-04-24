using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOD_Project
{
    public interface IReportable
    {
        string Report(IMediable mediable);
    }

    public interface IMediable
    {
        string Report(Airport airport);
        string Report(CargoPlane cargoPlane);
        string Report(PassengerPlane passengerPlane);
    }

    public abstract class AirportObjects
    {
        public UInt64 Id;
        public double Longitude;
        public double Latitude;
        public Single AMSL;
        public string? Phone;
        public string? Email;
    }
}
