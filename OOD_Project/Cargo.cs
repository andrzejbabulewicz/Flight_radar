using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOD_Project
{
    internal class Cargo
    {
        public string Type { get; set; } = "CA";

        public int ID { get; set; }
        public Single Weight { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }

        public Cargo(int id, Single weight, string code, string description)
        {
            ID = id;
            Weight = weight;
            Code = code;
            Description = description;
        }
    }
}