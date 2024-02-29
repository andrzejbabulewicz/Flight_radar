using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOD_Project
{
    public class Crew
    {
        public string Type { get; set; } = "C";
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public int Practice { get; set; }
        public string Role { get; set; }

        public Crew(int iD, string name, int age, string phone, string email, int practice, string role)
        {
            Id = iD;
            Name = name;
            Age = age;
            Phone = phone;
            Email = email;
            Practice = practice;
            Role = role;
        }
    }
}