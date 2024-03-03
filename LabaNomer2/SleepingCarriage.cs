using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabaNomer2
{
    class SleepingCarriage : Carriage
    {
        public int CompartmentsCount { get; set; }
        public bool HasShowers { get; set; }
        public int MaxPassengers { get; private set; }
        public int CurrentPassengers { get; set; }


        public SleepingCarriage(string id, string type, double loadCapacity, int compartmentsCount, bool hasShowers)
            : base(id, type, loadCapacity)
        {
            CompartmentsCount = compartmentsCount;
            HasShowers = hasShowers;
            MaxPassengers = 50;
            CurrentPassengers = 0;
        }

        public bool AddPassengers(int passengers)
        {
            if (CurrentPassengers + passengers > MaxPassengers)
            {
                return false;
            }
            CurrentPassengers += passengers;
            return true;
        }
        public void StationLoadPassengers()
        {
            Console.Write("Введіть кількість пасажирів для завантаження: ");
            int passengers = int.Parse(Console.ReadLine());

            if (AddPassengers(passengers))
            {
                Console.WriteLine($"Завантажено {passengers} пасажирів.");
            }
            else
            {
                Console.WriteLine("Кількість пасажирів не може перевищувати максимальну кількість місць.");
            }
        }
        public void StationUnloadPassengers()
        {
            Console.Write("Введіть кількість пасажирів для висадки: ");
            int passengers = int.Parse(Console.ReadLine());

            if (passengers <= CurrentPassengers)
            {
                CurrentPassengers -= passengers;
                Console.WriteLine($"Висаджено {passengers} пасажирів.");
            }
            else
            {
                Console.WriteLine("Ви не можете висадити більше пасажирів, ніж є в вагоні.");
            }
        }
    }
}
