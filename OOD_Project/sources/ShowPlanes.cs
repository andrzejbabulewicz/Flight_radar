using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlightTrackerGUI;


namespace OOD_Project.sources
{
    public class ShowPlanes
    {
        public ShowPlanes(NetworkDataHandler networkDataHandlerInstance)
        {
            // Subscribe to the event
            networkDataHandlerInstance.ShortcutStringFound += HandleShortcutStringFound;
        }

        // Event handler method
        private void HandleShortcutStringFound(object sender, ShortcutStringEventArgs e)
        {
            DataFlightHandler dataFlightHandler = new();
            // Handle the event, e.g., send a notification
            
            dataFlightHandler.FindFlightNetwork(e.StringList);
            FlightsGUIData temp = new FlightsGUIData(dataFlightHandler.LocateFlight());
            Runner.UpdateGUI(temp);
            
        }
    }
}
