using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOD_Project
{
    public class Passenger
    {
        public string Type { get; set; } = "P";
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Class { get; set; }
        public int Miles { get; set; }

        public Passenger(int iD, string name, int age, string phone, string email, string @class, int miles)
        {
            Id = iD;
            Name = name;
            Age = age;
            Phone = phone;
            Email = email;
            Class = @class;
            Miles = miles;
        }

        public static object CreatePassenger(string[] data)
        {
            return new Passenger(int.Parse(data[1]), data[2], int.Parse(data[3]), data[4], data[5], data[6],
                int.Parse(data[7]));
        }

        public static string CreateStringPassenger(System.IO.BinaryReader reader)
        {
            string Output = "P,";

            //ID
            Output += reader.ReadUInt64().ToString() + ",";
            //Name
            UInt16 NameLength = reader.ReadUInt16();
            Output += new string(reader.ReadChars(NameLength)) + ",";
            //Age
            Output += reader.ReadUInt16().ToString() + ",";
            //Phone
            Output += new string(reader.ReadChars(12)) + ",";
            //Email
            UInt16 EmailLength = reader.ReadUInt16();
            Output += new string(reader.ReadChars(EmailLength)) + ",";
            //Class
            Output += new string(reader.ReadChars(1)) + ",";
            //Miles
            Output += reader.ReadUInt64().ToString();

            return Output;
        }
    }
}