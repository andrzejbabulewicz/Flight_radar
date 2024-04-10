using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OOD_Project.sources;

namespace OOD_Project
{
    public class Airport : IReportable
    {
        public string Type { get; set; } = "AI";
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public Single Longitude { get; set; }
        public Single Latitude { get; set; }
        public Single Amsl { get; set; }
        public string Country { get; set; }

        public Airport(int id, string name, string code, Single longitude, Single latitude, Single aMSL, string country)
        {
            Id = id;
            Name = name;
            Code = code;
            Longitude = longitude;
            Latitude = latitude;
            Amsl = aMSL;
            Country = country;
        }

        public static Airport CreateAirport(string[] data)
        {

            return new Airport(int.Parse(data[1]), data[2], data[3],
                Single.Parse(data[4], CultureInfo.InvariantCulture),
                Single.Parse(data[5], CultureInfo.InvariantCulture),
                Single.Parse(data[6], CultureInfo.InvariantCulture),
                data[7]);
        }

        public static string CreateStringAirport(BinaryReader reader)
        {
            string Output = "AI,";

            //ID
            Output += reader.ReadUInt64().ToString() + ",";
            //NameLenght
            UInt16 NameLenght = reader.ReadUInt16();
            //Name
            Output += new string(reader.ReadChars(NameLenght)) + ",";
            //Code
            Output += new string(reader.ReadChars(3)) + ",";
            //Longitude
            Output += reader.ReadSingle().ToString(CultureInfo.InvariantCulture) + ",";
            //Latitude
            Output += reader.ReadSingle().ToString(CultureInfo.InvariantCulture) + ",";
            //AMSL
            Output += reader.ReadSingle().ToString(CultureInfo.InvariantCulture) + ",";
            //CountryCode
            Output += new string(reader.ReadChars(3));

            return Output;
        }

       public string Report(IMediable m)
        {
            return m.Report(this);
        }
    }
}