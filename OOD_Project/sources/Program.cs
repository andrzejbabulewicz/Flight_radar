using System.Security.AccessControl;
using System.Security.Cryptography;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Globalization;
using System.Linq.Expressions;
using NetworkSourceSimulator;
using System.Runtime.CompilerServices;
using System.Text;

namespace OOD_Project
{
    internal class Program
    {
        private static void Main(string[] args)
        {            
            string InputFilePath = @"..\..\..\inputs\example_data.ftr";
            string OutputFilePath = @"..\..\..\outputs\output.json";
            string SnapshotOutputPath= @"..\..\..\outputs\snapshots\";
            
            //STAGE 1
            DataHandler dataHandler = new();
            List<string> Records = dataHandler.ConvertFileToList(InputFilePath);
            dataHandler.ReadData(Records);
            dataHandler.SerializeData(dataHandler.List, OutputFilePath);

            //STAGE 2
            int minDelay = 5;
            int maxDelay = 10;
            
            NetworkDataHandler networkDataHandler = new();
            networkDataHandler.ThreadHandler(InputFilePath, SnapshotOutputPath, minDelay, maxDelay);
        }        
    }
}