using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabaNomer2
{
    class DiningCarriage : Carriage
    {
        public int TablesCount { get; set; }
        public bool HasKitchen { get; set; }
        public int StaffCount { get; private set; }
        public int DiningSeats { get; private set; }

        public DiningCarriage(string id, string type, double weight, double length, double loadCapacity, int tablesCount, bool hasKitchen)
            : base(id, type, weight, length, loadCapacity)
        {
            TablesCount = tablesCount;
            HasKitchen = hasKitchen;
            StaffCount = 5;
            DiningSeats = 40;
        }

        public double LoadFood(int passengers)
        {
            double foodPerPassenger = 1.0;
            return passengers * foodPerPassenger;
        }
    }
}
