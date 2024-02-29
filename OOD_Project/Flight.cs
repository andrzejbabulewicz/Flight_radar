using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOD_Project
{
    public class Flight
    {
        public string Type { get; set; } = "FL";
        public int Id { get; set; }
        public int OriginId { get; set; }
        public int TargetId { get; set; }
        public string TakeoffTime { get; set; }
        public string LandingTime { get; set; }
        public Single Longitude { get; set; }
        public Single Latitude { get; set; }
        public Single Amsl { get; set; }
        public int PlaneId { get; set; }
        public int[] CrewId { get; set; }
        public int[] LoadId { get; set; }

        public Flight(int iD, int originID, int targetID, string takeoffTime, string landingTime,
            Single longitude, Single latitude, Single aMSL, int planeID, int[] crewID, int[] loadID)
        {
            Id = iD;
            OriginId = originID;
            TargetId = targetID;
            TakeoffTime = takeoffTime;
            LandingTime = landingTime;
            Longitude = longitude;
            Latitude = latitude;
            Amsl = aMSL;
            PlaneId = planeID;
            CrewId = crewID;
            LoadId = loadID;
        }
    }
}