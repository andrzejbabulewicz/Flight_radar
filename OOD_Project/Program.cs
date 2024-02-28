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

            DataHandler data_Handler = new DataHandler();

            data_Handler.readData(InputFilePath);
            data_Handler.serializeData(OutputFilePath);
        }
    }
}