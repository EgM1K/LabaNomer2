using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabaNomer2
{
    public class PassengerCarriage : Carriage
    {
        public int SeatsCount { get; set; }
        public int Passengers { get; set; }

        public PassengerCarriage(string id, int seatsCount, double loadCapacity)
            : base(id, "Passenger", loadCapacity)
        {
            SeatsCount = 100;
        }

        public void LoadPassengers()
        {
            Console.WriteLine("Введіть кількість пасажирів:");
            Passengers = Convert.ToInt32(Console.ReadLine());
            if (Passengers > SeatsCount)
            {
                Console.WriteLine("Кількість пасажирів не може перевищувати кількість місць.");
                Passengers = SeatsCount;
            }
        }
    }
}
