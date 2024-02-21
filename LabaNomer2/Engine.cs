using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabaNomer2
{
    public class Engine
    {
        public string Id { get; set; }
        public double MaxSpeed { get; set; }
        public double MaxLoad { get; set; }

        public Engine(string id, double maxSpeed, double maxLoad)
        {
            Id = id;
            MaxSpeed = maxSpeed;
            MaxLoad = maxLoad;

        }

        public double CalculateSpeed(double load)
        {
            double maxSpeed = 120;

            double minSpeed = 60;

            double speed = maxSpeed - ((load / MaxLoad) * (maxSpeed - minSpeed));

            return speed;
        }
    }
}