using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOD_Project
{
    internal class Cargo : AirportObjects
    {
        public string Type { get; set; } = "CA";
        //public UInt64 Id { get; set; }
        public float Weight { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }

        public Cargo(UInt64 id, float weight, string code, string description)
        {
            Id = id;
            Weight = weight;
            Code = code;
            Description = description;
        }

        public static Cargo CreateCargo(string[] data)
        {
            return new Cargo(UInt64.Parse(data[1]), float.Parse(data[2], CultureInfo.InvariantCulture), data[3], data[4]);
        }

        public static string CreateStringCargo(System.IO.BinaryReader reader)
        {
            string Output = "CA,";
            
            //ID
            Output += reader.ReadUInt64().ToString() + ",";
            //Weight
            Output += reader.ReadSingle().ToString(CultureInfo.InvariantCulture) + ",";
            //Code
            Output += new string(reader.ReadChars(6)) + ",";
            //Description
            UInt16 DescriptionLength = reader.ReadUInt16();
            Output += new string(reader.ReadChars(DescriptionLength));

            return Output;
        }
    }
}