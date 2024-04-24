using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlightTrackerGUI;
using Mapsui.Projections;
using NetTopologySuite.Mathematics;

namespace OOD_Project.sources
{
    internal class DataFlightHandler
    {        
        public List<Airport> airports { get; set; } = [];
        public List<Flight> flights { get; set; } = [];

        public void FindFlightFTR(DataHandler dataHandler)
        {            
            //List<string> Records = dataHandler.ConvertFileToList(FilePaths.InputFilePath);
            //dataHandler.ReadData(Records);

            airports = dataHandler.airports;
            flights = dataHandler.flights;
        }

        public void FindFlightNetwork(List<string> records)
        {
            List<string> copy = new List<string>(records);
            DataHandler dataHandler = new();
            dataHandler.ReadData(copy);

            airports = dataHandler.airports;
            flights = dataHandler.flights;
        }

        public List<FlightGUI> LocateFlight()
        {
            //FindFlightNetwork();

            List<FlightGUI> flightGUI = new List<FlightGUI>();
            double x = 0;
            double y = 0;
            foreach (var flight in flights)
            {
                //UInt64 departure = flight.OriginId;
                UInt64 arrival = flight.TargetId;

                //Airport departureAirport = null;
                Airport arrivalAirport = null;

                foreach (var airport in airports)
                {
                    //if (airport.Id == departure)
                    //{
                    //    departureAirport = airport;
                    //}
                    if (airport.Id == arrival)
                    {
                        arrivalAirport = airport;
                    }
                }
                if(ShouldBeDisplayed(flight))
                {
                    (x, y) = flight.GetCurrentPosition(DateTime.Now, arrivalAirport);
                    WorldPosition worldPosition = new WorldPosition(y, x);
                    FlightGUI flightGUIData = new()
                    {
                        ID = flight.Id,
                        WorldPosition = worldPosition,
                        MapCoordRotation = flight.GetAngle(arrivalAirport)
                    };
                    flightGUI.Add(flightGUIData);
                }
            }

            return flightGUI;
        }

        public static bool ShouldBeDisplayed (Flight flight)
        {
            if(IsOvernight(flight))
            {
                if(DateTime.Now>flight.TakeoffTime || DateTime.Now<flight.LandingTime)
                {
                    return true;
                }

                return false;
                
            }
            else
            {
                if(DateTime.Now>flight.TakeoffTime && DateTime.Now<flight.LandingTime)
                {
                    return true;
                }

                return false;
            }

        }

        public static bool IsOvernight(Flight flight)
        {
            DateTime takeoff = flight.TakeoffTime;
            DateTime landing = flight.LandingTime;
            if (takeoff.Hour > landing.Hour)
            {
                return true;
            }
            return false;
        }

        public void PrintFlightsFTR(DataHandler dataHandler)
        {
            this.FindFlightFTR(dataHandler);
            System.Timers.Timer timer = new System.Timers.Timer(1000);
            timer.Elapsed += (sender, e) =>
            {                
                FlightsGUIData temp = new FlightsGUIData(this.LocateFlight());
                Runner.UpdateGUI(temp);
            };
            timer.Start();

        }
    }
}
