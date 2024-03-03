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
        

        public DiningCarriage(string id, string type, double loadCapacity, int tablesCount, bool hasKitchen)
            : base(id, type, loadCapacity)
        {
            TablesCount = tablesCount = 40;
            HasKitchen = hasKitchen = true;
            StaffCount = 5;
            DiningSeats = 40;
        }

        public double LoadFood(int passengers)
        {
            double foodPerPassenger = 0.5;
            return passengers * foodPerPassenger;
        }
        public void StationLoadFood()
        {
            Console.Write("Введіть кількість пасажирів для завантаження їжі: ");
            int passengers = int.Parse(Console.ReadLine());

            double foodWeight = LoadFood(passengers);
            Console.WriteLine($"Завантажено їжу вагою {foodWeight}.");
        }
    }
}
