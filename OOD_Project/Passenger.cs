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

        public int ID { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Class { get; set; }
        public int Miles { get; set; }

        public Passenger(int iD, string name, int age, string phone, string email, string @class, int miles)
        {
            ID = iD;
            Name = name;
            Age = age;
            Phone = phone;
            Email = email;
            Class = @class;
            Miles = miles;
        }
    }
}