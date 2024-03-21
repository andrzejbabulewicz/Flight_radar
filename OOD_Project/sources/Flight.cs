using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mapsui.Extensions;
using Mapsui.Projections;

namespace OOD_Project
{
    public class Flight
    {
        public string Type { get; set; } = "FL";
        public UInt64 Id { get; set; }
        public int OriginId { get; set; }
        public int TargetId { get; set; }
        public DateTime TakeoffTime { get; set; }
        public DateTime LandingTime { get; set; }
        public float Longitude { get; set; }
        public float Latitude { get; set; }
        public float Amsl { get; set; }
        public int PlaneId { get; set; }
        public int[] CrewId { get; set; }
        public int[] LoadId { get; set; }

        public Flight(UInt64 iD, int originID, int targetID, DateTime takeoffTime, DateTime landingTime,
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

        public static Flight CreateFlight(string[] data)
        {
            int[] crewid = Array.ConvertAll(data[10].Trim('[', ']').Split(';'), int.Parse);
            int[] loadid = Array.ConvertAll(data[11].Trim('[', ']').Split(';'), int.Parse);
            return new Flight(UInt64.Parse(data[1]), int.Parse(data[2]), int.Parse(data[3]), 
                DateTime.ParseExact(data[4], "HH:mm", CultureInfo.InvariantCulture),
                DateTime.ParseExact(data[5], "HH:mm", CultureInfo.InvariantCulture),
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

        public (double, double) GetCurrentPosition(DateTime current, Airport departureAirport, Airport arrivalAirport)
        {
            DateTime takeoff = TakeoffTime;
            DateTime landing = LandingTime;

            double flightTime = (landing>takeoff) ? (landing - takeoff).TotalHours : (landing - takeoff).TotalHours + 24;
            double timeFromStart = (takeoff<current) ? (current - takeoff).TotalHours : (current - takeoff).TotalHours + 24;

            double progress= timeFromStart / flightTime;

            double longitude = CalculatePosition(departureAirport.Longitude, arrivalAirport.Longitude, progress);
            double latitude = CalculatePosition(departureAirport.Latitude, arrivalAirport.Latitude, progress);

            return (longitude, latitude);
        }

        public double CalculatePosition(double start, double end, double progress)
        {
            return start + ((end - start) * progress);
        }

        public double GetAngle(Airport departureAirport, Airport arrivalAirport)
        {
            (double x1, double y1) = SphericalMercator.FromLonLat(departureAirport.Longitude, departureAirport.Latitude);
            (double x2, double y2) = SphericalMercator.FromLonLat(arrivalAirport.Longitude, arrivalAirport.Latitude);
            
            double difx = x2 - x1;
            double dify = y2 - y1;
            double angle = Math.Atan2(difx, dify);
            
            return angle;
        }

       
    }
}