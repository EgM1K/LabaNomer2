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
        public double TotalLoad { get; set; }
        public void AddCarriage(Carriage carriage)
        {
            Carriages.Add(carriage);
        }

        public Train(string id, List<Engine> engines)
        {
            Id = id;
            Engines = engines;
            MaxWeight = engines.Count * 800;
            Carriages = new List<Carriage>();
        }
        private bool hasFreightCarriages = false;

        public void AddCarriage(int caseNumber, int passengers = 0, double load = 0, int foodLoad = 0, int tablesCount = 0, bool hasKitchen = false, int compartmentsCount = 0, bool hasShowers = false)
        {
            string id;
            switch (caseNumber)
            {
                case 1:
                    Console.Write("Введіть кількість пасажирів: ");
                    passengers = Convert.ToInt32(Console.ReadLine());
                    id = (Carriages.Count + 1).ToString();
                    PassengerCarriage passengerCarriage = new PassengerCarriage(id, 25, passengers);
                    passengerCarriage.LoadPassengers();
                    Carriages.Add(passengerCarriage);
                    break;
                case 2:
                    if (hasFreightCarriages)
                    {
                        Console.WriteLine("Ви не можете додати вантажні вагони до потягу, який вже має інші типи вагонів.");
                        return;
                    }
                    Console.Write("Введіть вагу вантажу: ");
                    load = Convert.ToDouble(Console.ReadLine());
                    if (load > 100)
                    {
                        Console.WriteLine("Вага вантажу не може перевищувати 100 тон.");
                        return;
                    }
                    id = (Carriages.Count + 1).ToString();
                    FreightCarriage freightCarriage = new FreightCarriage(id, load);
                    freightCarriage.Load = load;
                    Carriages.Add(freightCarriage);
                    break;
                case 3:
                    if (foodLoad > 500)
                    {
                        Console.WriteLine("Завантаження їжі не може перевищувати 500 кг.");
                        return;
                    }
                    id = (Carriages.Count + 1).ToString();
                    DiningCarriage diningCarriage = new DiningCarriage(id, "Dining", 25, foodLoad, hasKitchen);
                    Carriages.Add(diningCarriage);
                    break;
                case 4:
                    id = (Carriages.Count + 1).ToString();
                    SleepingCarriage sleepingCarriage = new SleepingCarriage(id, "Sleeping", 25, compartmentsCount, hasShowers);
                    Carriages.Add(sleepingCarriage);
                    break;
                default:
                    Console.WriteLine("Невідомий тип вагону.");
                    break;
            }
        }

        public void InitializeTrain()
        {
            Console.Write("Введіть назву потягу: ");
            Name = Console.ReadLine();

            MaxWeight = 800;
            Console.WriteLine($"Максимальна вага потягу: {MaxWeight} тон");

            Console.Write("Введіть кількість рушіїв: ");
            int engineCount = Convert.ToInt32(Console.ReadLine());

            Engines = new List<Engine>();
            for (int i = 0; i < engineCount; i++)
            {
                Engines.Add(new Engine(120, 800, 25));
            }

            Console.Write("Виберіть тип вагонів (1 - пасажирський, 2 - вантажний, 3 - вагон-ресторан, 4 - спальний вагон): ");
            int type = Convert.ToInt32(Console.ReadLine());

            Console.Write("Введіть кількість вагонів: ");
            int carriageCount = Convert.ToInt32(Console.ReadLine());

            for (int i = 0; i < carriageCount; i++)
            {
                AddCarriage(type);
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
            double totalWeight = 0;
            double totalLength = 0;
            foreach (var carriage in Carriages)
            {
                Console.WriteLine($"ID вагону: {carriage.Id}, Тип: {carriage.Type}, Вага: {carriage.Weight}, Довжина: {carriage.Length}");
                totalLength += carriage.Length;
                totalWeight += carriage.Weight;
                int totalPassengers = 0;
                switch (carriage)
                {
                    case PassengerCarriage passengerCarriage:
                        Console.WriteLine($"Кількість пасажирів: {passengerCarriage.Passengers}");
                        totalPassengers += passengerCarriage.Passengers;
                        break;
                    case FreightCarriage freightCarriage:
                        Console.WriteLine($"Вага вантажу: {freightCarriage.Load}");
                        totalLoad += freightCarriage.Load;
                        break;
                    case DiningCarriage diningCarriage:
                        Console.WriteLine($"Вантаж: {diningCarriage.LoadFood(diningCarriage.DiningSeats)}");
                        totalLoad += diningCarriage.LoadFood(diningCarriage.DiningSeats);
                        break;
                    case SleepingCarriage sleepingCarriage:
                        Console.WriteLine($"Кількість пасажирів: {sleepingCarriage.CurrentPassengers}");
                        totalPassengers += sleepingCarriage.CurrentPassengers;
                        break;
                    default:
                        Console.WriteLine("Невідомий тип вагону.");
                        break;
                }
            }
            Console.WriteLine($"Загальна довжина потягу: {totalLength} метрів");
            Console.WriteLine($"Загальна вага вантажу: {TotalLoad} тон");
        }




        public JourneyInfo GetJourneyInfo(double distance)
        {
            double totalLoad = Carriages.Sum(c => c is FreightCarriage freightCarriage ? freightCarriage.Load + freightCarriage.Weight : 0);
            double totalMaxLoad = Engines.Sum(e => e.MaxLoad);
            double speed = Engines.First().CalculateSpeed(totalLoad);

            double time = Engines.First().CalculateTime(distance);

            return new JourneyInfo(distance, time, speed, totalLoad);
        }
    }
}