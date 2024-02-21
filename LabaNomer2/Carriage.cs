﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabaNomer2
{
    public abstract class Carriage
    {
        public string Id { get; set; }
        public string Type { get; set; }
        public double Weight { get; set; }
        public double Length { get; set; }
        public double LoadCapacity { get; set; }

        protected Carriage(string id, string type, double weight, double length, double loadCapacity)
        {
            Id = id;
            Type = type;
            Weight = weight;
            Length = length;
            LoadCapacity = loadCapacity;
        }
    }
}
