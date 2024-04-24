using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOD_Project
{
    public class Crew : AirportObjects
    {
        public string Type { get; set; } = "C";
        //public UInt64 Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public int Practice { get; set; }
        public string Role { get; set; }

        public Crew(UInt64 iD, string name, int age, string phone, string email, int practice, string role)
        {
            Id = iD;
            Name = name;
            Age = age;
            Phone = phone;
            Email = email;
            Practice = practice;
            Role = role;
        }

        public static Crew CreateCrew(string[] data)
        {

            return new Crew(UInt64.Parse(data[1]), data[2], int.Parse(data[3]), data[4], data[5], int.Parse(data[6]),
                data[7]);
        }

        public static string CreateStringCrew(BinaryReader reader)
        {
            string Output = "C,";
            
            //ID
            Output += reader.ReadUInt64().ToString() + ",";
            //Name
            UInt16 NameLenght = reader.ReadUInt16();
            Output += new string(reader.ReadChars(NameLenght)) + ",";
            //Age
            Output += reader.ReadUInt16().ToString() + ",";
            //Phone
            Output += new string(reader.ReadChars(12)) + ",";
            //Email
            UInt16 EmailLenght = reader.ReadUInt16();
            Output += new string(reader.ReadChars(EmailLenght)) + ",";
            //Practice
            Output += reader.ReadUInt16().ToString() + ",";
            //Role
            Output += new string(reader.ReadChars(1));

            return Output;
        }
    }
}