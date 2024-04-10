using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOD_Project
{
    public class CargoPlane : IReportable
    {
        public string Type { get; set; } = "CP";
        public int Id { get; set; }
        public string Serial { get; set; }
        public string Country { get; set; }
        public string Model { get; set; }
        public float MaxLoad { get; set; }

        public CargoPlane(int iD, string serial, string country, string model, float maxLoad)
        {
            Id = iD;
            Serial = serial;
            Country = country;
            Model = model;
            MaxLoad = maxLoad;
        }

        public static CargoPlane CreateCargoPlane(string[] data)
        {
            return new CargoPlane(int.Parse(data[1]), data[2], data[3], data[4],
                float.Parse(data[5], CultureInfo.InvariantCulture));
        }

        public static string CreateStringCargoPlane(System.IO.BinaryReader reader)
        {
            string Output = "CP,";
            
            //ID
            Output += reader.ReadUInt64().ToString() + ",";
            //Serial
            Output += new string(reader.ReadChars(10)).TrimEnd('\0') + ",";
            //Country
            Output += new string(reader.ReadChars(3)) + ",";
            //Model
            UInt16 ModelLength = reader.ReadUInt16();
            Output += new string(reader.ReadChars(ModelLength)) + ",";
            //MaxLoad
            Output += reader.ReadSingle().ToString(CultureInfo.InvariantCulture);

            return Output;
        }

        public string Report(IMediable m)
        {
            return m.Report(this);
        }
    }
}