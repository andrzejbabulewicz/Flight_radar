#define STAGE_1
//#define STAGE_2
//#define STAGE_3a
#define STAGE_3b

using FlightTrackerGUI;
using OOD_Project.sources;



namespace OOD_Project
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            //STAGE 1
#if STAGE_1

            DataHandler dataHandler = new();
            List<string> Records = dataHandler.ConvertFileToList(FilePaths.InputFilePath);
            List<object> ListedObjects = dataHandler.ReadData(Records);
            dataHandler.SerializeData(ListedObjects, FilePaths.OutputFilePath);
#endif
            //STAGE 2
#if STAGE_2
            
            //int minDelay = 5;
            //int maxDelay = 10;

            //NetworkDataHandler networkDataHandler = new();
            //networkDataHandler.ThreadHandler(InputFilePath, SnapshotOutputPath, minDelay, maxDelay);
#endif
            //STAGE 3
#if STAGE_3a
            
            //READING FORM FILE
            DataFlightHandler dataFlightHandler = new();
            dataFlightHandler.PrintFlightsFTR();

#endif

#if STAGE_3b
            //READING FROM STREAM
            NetworkDataHandler networkDataHandler = new();
            networkDataHandler.ThreadHandler(FilePaths.InputFilePath, FilePaths.SnapshotOutputPath, 200, 400);
            
            Runner.Run();
#endif  
        }        
    }
}