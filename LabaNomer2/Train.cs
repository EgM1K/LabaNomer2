using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace LabaNomer2
{
    public class Train : ITrainDR1
    {
        public Engine Engine { get; set; }
        public string Id { get; set; }
        public string Name { get; set; }
        public double MaxWeight { get; set; }
        public List<Carriage> Carriages { get; set; }
        public List<Engine> Engines { get; set; }
        public void AddCarriage(Carriage carriage)
        {
            Carriages.Add(carriage);
        }

        public Train(string id, List<Engine> engines, double maxWeight)
        {
            Id = id;
            Engines = engines;
            MaxWeight = maxWeight;
            Carriages = new List<Carriage>();
        }

        public void AddCarriages(int carriageCount, int type)
        {
            for (int i = 1; i <= carriageCount; i++)
            {
                if (type == 1)
                {
                    Console.WriteLine("Введіть кількість пасажирів:");
                    int passengers = Convert.ToInt32(Console.ReadLine());
                    PassengerCarriage passengerCarriage = new PassengerCarriage(i.ToString(), 25, 10, 100, passengers);
                    AddCarriage(passengerCarriage);
                }
                else if (type == 2)
                {
                    Console.WriteLine("Введіть вагу вантажу:");
                    double load = Convert.ToDouble(Console.ReadLine());
                    FreightCarriage freightCarriage = new FreightCarriage(i.ToString(), 25, 10, load);
                    AddCarriage(freightCarriage);
                }
                else if (type == 3)
                {
                    Console.WriteLine("Введіть кількість столів:");
                    int tablesCount = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine("Чи є кухня? (y/n)");
                    bool hasKitchen = Console.ReadLine().ToLower() == "y";
                    DiningCarriage diningCarriage = new DiningCarriage(i.ToString(), "Dining", 25, 10, 100, tablesCount, hasKitchen);
                    AddCarriage(diningCarriage);
                }
                else if (type == 4)
                {
                    Console.WriteLine("Введіть кількість купе:");
                    int compartmentsCount = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine("Чи є душ? (y/n)");
                    bool hasShowers = Console.ReadLine().ToLower() == "y";
                    SleepingCarriage sleepingCarriage = new SleepingCarriage(i.ToString(), "Sleeping", 25, 10, 100, compartmentsCount, hasShowers);
                    AddCarriage(sleepingCarriage);
                }
            }
        }
        public void SimulateJourney(double distance)
        {
            Random random = new Random();
            double accidentChance = random.NextDouble();
            if (accidentChance < 0.001)
            {
                Console.WriteLine("Сталася аварія! Потяг перекинувся.");
            }
            else
            {
                foreach (var carriage in Carriages)
                {
                    if (carriage is PassengerCarriage passengerCarriage)
                    {
                        double passengerAccidentChance = random.NextDouble();
                        if (passengerAccidentChance < 0.02)
                        {
                            int affectedPassengers = random.Next(1, passengerCarriage.Passengers + 1);
                            Console.WriteLine($"У {affectedPassengers} пасажирів сталися неприємності.");
                            passengerCarriage.Passengers -= affectedPassengers;
                        }
                    }
                }
            }
        }
        public void PrintTrainInfo()
        {
            Console.WriteLine($"Train Name: {Name}");
            Console.WriteLine($"Max Weight: {MaxWeight}");
            Console.WriteLine($"Number of Engines: {Engines.Count}");
            foreach (var engine in Engines)
            {
                Console.WriteLine($"ID рушію: {engine.Id}, Max Speed: {engine.MaxSpeed}, Max Load: {engine.MaxLoad}");
            }
            Console.WriteLine($"Кількість вагонів: {Carriages.Count}");
            Console.WriteLine("Інформація про вагони:");
            double totalLoad = 0;
            foreach (var carriage in Carriages)
            {
                Console.WriteLine($"ID вагону: {carriage.Id}, Тип: {carriage.Type}, Вага: {carriage.Weight}, Довжина: {carriage.Length}");
                if (carriage is PassengerCarriage passengerCarriage)
                {
                    Console.WriteLine($"Кількість пасажирів: {passengerCarriage.Passengers}");
                }
                else if (carriage is FreightCarriage freightCarriage)
                {
                    Console.WriteLine($"Вантаж: {freightCarriage.Load}");
                    totalLoad += freightCarriage.Load;
                }
            }
        }
        public JourneyInfo GetJourneyInfo(double distance)
        {
            double totalLoad = Carriages.Sum(c => c.LoadCapacity);
            double totalMaxLoad = Engines.Sum(e => e.MaxLoad);

            // Використовуємо метод CalculateSpeed з класу Engine
            double speed = Engines.First().CalculateSpeed(totalLoad);

            // Використовуємо метод CalculateTime з класу Engine
            double time = Engines.First().CalculateTime(distance);

            return new JourneyInfo(distance, time, speed, totalLoad);
        }
    }
}

