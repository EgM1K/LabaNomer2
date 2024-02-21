using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabaNomer2
{
    public class Engine
    {
        public string Name { get; set; }
        public double MaxSpeed { get; set; }
        public double MaxLoad { get; set; }

        public Engine(string name, double maxSpeed, double maxLoad)
        {
            Name = name;
            MaxSpeed = maxSpeed;
            MaxLoad = maxLoad;
        }
    }
}