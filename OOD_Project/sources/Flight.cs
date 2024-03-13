using System;
using System.Collections.Generic;
using System.Globalization;
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
        public float Longitude { get; set; }
        public float Latitude { get; set; }
        public float Amsl { get; set; }
        public int PlaneId { get; set; }
        public int[] CrewId { get; set; }
        public int[] LoadId { get; set; }

        public Flight(int iD, int originID, int targetID, string takeoffTime, string landingTime,
            float longitude, float latitude, float aMSL, int planeID, int[] crewID, int[] loadID)
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

        public static object CreateFlight(string[] data)
        {
            int[] crewid = Array.ConvertAll(data[10].Trim('[', ']').Split(';'), int.Parse);
            int[] loadid = Array.ConvertAll(data[11].Trim('[', ']').Split(';'), int.Parse);
            return new Flight(int.Parse(data[1]), int.Parse(data[2]), int.Parse(data[3]), data[4], data[5],
                float.Parse(data[6], CultureInfo.InvariantCulture),
                float.Parse(data[7], CultureInfo.InvariantCulture),
                float.Parse(data[8], CultureInfo.InvariantCulture), int.Parse(data[9]), crewid, loadid);
        }

        public static string CreateStringFlight(BinaryReader reader)
        {
            string Output = "FL,";
            
            //ID
            Output += reader.ReadUInt64().ToString() + ",";
            //OriginID
            Output += reader.ReadUInt64().ToString() + ",";
            //TargetID
            Output += reader.ReadUInt64().ToString() + ",";
            //TakeoffTime
            UInt64 TimeMS = reader.ReadUInt64();
            DateTime Time = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddMilliseconds(TimeMS);
            Output += Time.ToString("HH:mm") + ",";
            //LandingTime
            UInt64 TimeMS2 = reader.ReadUInt64();
            DateTime Time2 = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddMilliseconds(TimeMS2);
            Output += Time2.ToString("HH:mm") + ",";

            //Longitude
            Output += "0,";
            //Latitude
            Output += "0,";
            //AMSL
            Output += "0,";

            //PlaneID
            Output += reader.ReadUInt64().ToString() + ",";
            //Crew
            Output += "[";
            UInt16 CrewCount = reader.ReadUInt16();
            for (int i = 0; i < CrewCount; i++)
            {
                Output += reader.ReadUInt64().ToString();
                if (i != CrewCount - 1)
                {
                    Output += ";";
                }
            }
            Output += "],";

            //Passenger
            Output += "[";
            UInt16 PassengerCount = reader.ReadUInt16();
            for (int i = 0; i < PassengerCount; i++)
            {
                Output += reader.ReadUInt64().ToString();
                if (i != PassengerCount - 1)
                {
                    Output += ";";
                }
            }

            Output += "]";

            return Output;
        }
    }
}