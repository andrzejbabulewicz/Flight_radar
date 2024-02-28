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
        public int ID { get; set; }
        public int OriginID { get; set; }
        public int TargetID { get; set; }
        public string TakeoffTime { get; set; }
        public string LandingTime { get; set; }
        public Single Longitude { get; set; }
        public Single Latitude { get; set; }
        public Single AMSL { get; set; }
        public int PlaneID { get; set; }
        public int[] CrewID { get; set; }
        public int[] LoadID { get; set; }

        public Flight(int iD, int originID, int targetID, string takeoffTime, string landingTime,
            Single longitude, Single latitude, Single aMSL, int planeID, int[] crewID, int[] loadID)
        {
            ID = iD;
            OriginID = originID;
            TargetID = targetID;
            TakeoffTime = takeoffTime;
            LandingTime = landingTime;
            Longitude = longitude;
            Latitude = latitude;
            AMSL = aMSL;
            PlaneID = planeID;
            CrewID = crewID;
            LoadID = loadID;
        }
    }
}