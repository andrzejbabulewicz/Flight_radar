using System.Security.AccessControl;
using System.Security.Cryptography;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Globalization;
using System.Linq.Expressions;

namespace OOD_Project
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            string InputFilePath = @"..\..\..\inputs\example_data.ftr";
            string OutputFilePath = @"..\..\..\outputs\output.json";

            DataHandler dataHandler = new DataHandler();

            dataHandler.ReadData(InputFilePath);
            dataHandler.SerializeData(OutputFilePath);
        }
    }
}