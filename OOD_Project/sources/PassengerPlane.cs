using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOD_Project
{
    public class PassengerPlane : IReportable
    {
        public string Type { get; set; } = "PP";
        public int Id { get; set; }
        public string Serial { get; set; }
        public string Country { get; set; }
        public string Model { get; set; }
        public int FirstClassSize { get; set; }
        public int BusinessClassSize { get; set; }
        public int EconomyClassSize { get; set; }

        public PassengerPlane(int id, string serial, string country, string model, int firstClassSize,
            int businessClassSize, int economyClassSize)
        {
            Id = id;
            Serial = serial;
            Country = country;
            Model = model;
            FirstClassSize = firstClassSize;
            BusinessClassSize = businessClassSize;
            EconomyClassSize = economyClassSize;
        }

        public static PassengerPlane CreatePassengerPlane(string[] data)
        {
            return new PassengerPlane(int.Parse(data[1]), data[2], data[3], data[4], int.Parse(data[5]),
                int.Parse(data[6]), int.Parse(data[7]));
        }

        public static string CreateStringPassengerPlane(System.IO.BinaryReader reader)
        {
            string Output = "PP,";
            
            //ID
            Output += reader.ReadUInt64().ToString() + ",";
            //Serial
            Output += new string(reader.ReadChars(10)).TrimEnd('\0') + ",";
            //Country
            Output += new string(reader.ReadChars(3)) + ",";
            //Model
            UInt16 ModelLength = reader.ReadUInt16();
            Output += new string(reader.ReadChars(ModelLength)) + ",";
            //FirstClassSize
            Output += reader.ReadUInt16().ToString() + ",";
            //BusinessClassSize
            Output += reader.ReadUInt16().ToString() + ",";
            //EconomyClassSize
            Output += reader.ReadUInt16().ToString();

            return Output;
        }

        public string Report(IMediable m)
        {
            return m.Report(this);
        }
    }
}