using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabaNomer2
{
    public class FreightCarriage : Carriage
    {
        public double Load { get; set; }

        public FreightCarriage(string id, double weight, double length, double loadCapacity)
            : base(id, "Freight", weight, length, loadCapacity)
        {
            Load = 0;
        }

        public void LoadCargo()
        {
            Console.WriteLine("Введіть вагу вантажу:");
            double cargoWeight = Convert.ToDouble(Console.ReadLine());
            if (cargoWeight > LoadCapacity)
            {
                Console.WriteLine("Вантаж не може перевищувати максимальну вантажопідйомність вагона.");
            }
            else
            {
                Load = cargoWeight;
            }
        }
    }
}
