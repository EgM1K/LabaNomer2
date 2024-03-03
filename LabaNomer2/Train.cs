using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        public double LoadCapacity { get; set; }
        public MaterialType Material { get; set; }
        public string OtherMaterial { get; set; }
        public double Weight { get; set; }
        public List<Carriage> Carriages { get; set; }
        public List<Engine> Engines { get; set; }
        public double TotalLoad { get; set; }
        public List<FreightCarriage> FreightCarriages { get; set; }
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
            FreightCarriages = new List<FreightCarriage>();
        }
        private bool hasFreightCarriages = false;

        public void AddCarriage(int caseNumber, int passengers = 0, double load = 0, int foodLoad = 0, int tablesCount = 0, bool hasKitchen = false, int compartmentsCount = 0, bool hasShowers = false, double loadCapacity = 0)
        {
            string id;
            switch (caseNumber)
            {
                case 1:
                    id = (Carriages.Count + 1).ToString();
                    PassengerCarriage passengerCarriage = new PassengerCarriage(id, 25, load);
                    passengerCarriage.LoadPassengers();
                    Carriages.Add(passengerCarriage);
                    break;
                case 2:
                    Console.WriteLine("Виберіть тип матеріалу:");
                    foreach (var material in Enum.GetValues(typeof(MaterialType)))
                    {
                        Console.WriteLine($"{(int)material}. {material}");
                    }
                    MaterialType chosenMaterial = (MaterialType)Enum.Parse(typeof(MaterialType), Console.ReadLine());

                    string materialName = chosenMaterial.ToString();
                    if (chosenMaterial == MaterialType.Other)
                    {
                        Console.Write("Введіть назву матеріалу: ");
                        materialName = Console.ReadLine();
                    }

                    Console.Write("Введіть вагу вантажу: ");
                    double cargoWeight = Convert.ToDouble(Console.ReadLine());

                    double maxLoad = LoadCapacity;
                    switch (chosenMaterial)
                    {
                        case MaterialType.Chemical:
                            maxLoad = 120;
                            break;
                        case MaterialType.Oil:
                        case MaterialType.Gas:
                            maxLoad = 80;
                            break;
                        case MaterialType.Brittle:
                            maxLoad = 50;
                            break;
                        case MaterialType.Wood:
                        case MaterialType.Metal:
                        case MaterialType.Coal:
                        case MaterialType.Other:
                            maxLoad = 100;
                            break;
                    }

                    if (cargoWeight > maxLoad)
                    {
                        Console.WriteLine($"Вантаж не може перевищувати максимальну вантажопідйомність вагона для {materialName}.");
                    }
                    else
                    {
                        id = (Carriages.Count + 1).ToString();
                        FreightCarriage freightCarriage = new FreightCarriage(id, maxLoad, chosenMaterial);
                        if (chosenMaterial == MaterialType.Other)
                        {
                            freightCarriage.OtherMaterial = materialName;
                        }
                        freightCarriage.Load = cargoWeight;
                        Carriages.Add(freightCarriage);
                        Console.WriteLine($"Вантаж {materialName} завантажено.");
                    }
                        break;

                case 3:
                    if (loadCapacity > 500)
                    {
                        Console.WriteLine("Завантаження їжі не може перевищувати 500 кг.");
                        return;
                    }
                    id = (Carriages.Count + 1).ToString();
                    DiningCarriage diningCarriage = new DiningCarriage(id, "Dining", loadCapacity, tablesCount, hasKitchen);
                    Carriages.Add(diningCarriage);
                    break;
                case 4:
                    if (!Carriages.Any(c => c is DiningCarriage))
                    {
                        Console.WriteLine("Купе не може бути без вагона-ресторана.");
                        return;
                    }
                    id = (Carriages.Count + 1).ToString();
                    SleepingCarriage sleepingCarriage = new SleepingCarriage(id, "Sleeping", loadCapacity, compartmentsCount, hasShowers);
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

        public void SimulateJourneyWithStops(double distance)
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
            double currentDistance = 0;
            double previousStop = 0;
            while (currentDistance < distance)
            {
                double nextStop = random.Next(50, 150);
                currentDistance += nextStop;
                if (currentDistance > distance)
                {
                    Console.WriteLine("Потяг доїхав до кінцевої точки.");
                    break;
                }
                Console.WriteLine($"Потяг зупинився на відстані {currentDistance} км. Відстань від попередньої зупинки: {currentDistance - previousStop} км.");
                previousStop = currentDistance;
                for (int i = 0; i < Carriages.Count; i++)
                {
                    var carriage = Carriages[i];
                    if (carriage is FreightCarriage freightCarriage)
                    {
                        Console.WriteLine($"Вагон {i + 1}: ID вагону: {carriage.Id}, Тип: {carriage.Type}, Вага: {carriage.Weight}, Довжина: {carriage.Length}, Тип матеріалу: {freightCarriage.Material}");
                        Console.WriteLine("Виберіть дію: 1 - Розвантажити вагон, 2 - Завантажити вагон, 3 - Продовжити подорож");
                        int action = Convert.ToInt32(Console.ReadLine());
                        switch (action)
                        {
                            case 1:
                                freightCarriage.StationUnloadCargo();
                                Console.WriteLine("Виберіть дію: 1 - Завантажити вагон, 2 - Продовжити подорож");
                                action = Convert.ToInt32(Console.ReadLine());
                                if (action == 1)
                                {
                                    freightCarriage.StationLoadCargo();
                                }
                                break;
                            case 2:
                                freightCarriage.StationLoadCargo();
                                break;
                            case 3:
                                break;
                            default:
                                Console.WriteLine("Невідома дія.");
                                break;
                        }
                    }
                    else if (carriage is PassengerCarriage passengerCarriage)
                    {
                        Console.WriteLine($"Вагон {i + 1}: ID вагону: {carriage.Id}, Тип: {carriage.Type}, Вага: {carriage.Weight}, Довжина: {carriage.Length}");
                        Console.WriteLine("Виберіть дію: 1 - Висадити пасажирів, 2 - Засадити пасажирів, 3 - Продовжити подорож");
                        int action = Convert.ToInt32(Console.ReadLine());
                        switch (action)
                        {
                            case 1:
                                passengerCarriage.StationUnloadPassengers();
                                Console.WriteLine("Виберіть дію: 1 - Засадити пасажирів, 2 - Продовжити подорож");
                                action = Convert.ToInt32(Console.ReadLine());
                                if (action == 1)
                                {
                                    passengerCarriage.StationLoadPassengers();
                                }
                                break;
                            case 2:
                                passengerCarriage.StationLoadPassengers();
                                break;
                            case 3:
                                break;
                            default:
                                Console.WriteLine("Невідома дія.");
                                break;
                        }
                    }
                    else if (carriage is DiningCarriage diningCarriage)
                    {
                        Console.WriteLine($"Вагон {i + 1}: ID вагону: {carriage.Id}, Тип: {carriage.Type}, Вага: {carriage.Weight}, Довжина: {carriage.Length}");
                        Console.WriteLine("Виберіть дію: 1 - Завантажити їжу, 2 - Продовжити подорож");
                        int action = Convert.ToInt32(Console.ReadLine());
                        switch (action)
                        {
                            case 1:
                                diningCarriage.StationLoadFood();
                                break;
                            case 2:
                                break;
                            default:
                                Console.WriteLine("Невідома дія.");
                                break;
                        }
                    }
                    else if (carriage is SleepingCarriage sleepingCarriage)
                    {
                        Console.WriteLine($"Вагон {i + 1}: ID вагону: {carriage.Id}, Тип: {carriage.Type}, Вага: {carriage.Weight}, Довжина: {carriage.Length}");
                        Console.WriteLine("Виберіть дію: 1 - Висадити пасажирів, 2 - Засадити пасажирів, 3 - Продовжити подорож");
                        int action = Convert.ToInt32(Console.ReadLine());
                        switch (action)
                        {
                            case 1:
                                sleepingCarriage.StationUnloadPassengers();
                                Console.WriteLine("Виберіть дію: 1 - Засадити пасажирів, 2 - Продовжити подорож");
                                action = Convert.ToInt32(Console.ReadLine());
                                if (action == 1)
                                {
                                    sleepingCarriage.StationLoadPassengers();
                                }
                                break;
                            case 2:
                                sleepingCarriage.StationLoadPassengers();
                                break;
                            case 3:
                                break;
                            default:
                                Console.WriteLine("Невідома дія.");
                                break;
                        }
                    }
                }
                Console.WriteLine("Виберіть дію: 1 - Додати вагон, 2 - Продовжити подорож");
                int stationAction = Convert.ToInt32(Console.ReadLine());
                if (stationAction == 1)
                {
                    Console.WriteLine("Виберіть тип вагону: 1 - Пасажирський, 2 - Вантажний, 3 - Ресторан, 4 - Купе");
                    int caseNumber = Convert.ToInt32(Console.ReadLine());
                    AddCarriage(caseNumber);
                }
            }
            Console.WriteLine($"Загальний шлях: {currentDistance} км.");
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
            int totalPassengers = 0;
            foreach (var carriage in Carriages)
            {
                string cargoType = "";
                switch (carriage)
                {
                    case PassengerCarriage passengerCarriage:
                        cargoType = "Люди";
                        totalPassengers += passengerCarriage.Passengers;
                        break;
                    case FreightCarriage freightCarriage:
                        cargoType = freightCarriage.Material.ToString();
                        if (freightCarriage.Material == MaterialType.Other)
                        {
                            cargoType = freightCarriage.OtherMaterial;
                        }
                        totalLoad += freightCarriage.Load;
                        carriage.Weight += freightCarriage.Load;
                        break;
                    case DiningCarriage diningCarriage:
                        cargoType = "Їжа";
                        totalLoad += diningCarriage.LoadFood(diningCarriage.DiningSeats);
                        Console.WriteLine($"Кількість столів: {diningCarriage.TablesCount}");
                        Console.WriteLine($"Наявність кухні: {diningCarriage.HasKitchen}");
                        Console.WriteLine($"Кількість персоналу: {diningCarriage.StaffCount}");
                        Console.WriteLine($"Кількість місць: {diningCarriage.DiningSeats}");
                        break;
                    case SleepingCarriage sleepingCarriage:
                        cargoType = "Люди";
                        totalPassengers += sleepingCarriage.CurrentPassengers;
                        Console.WriteLine($"Кількість купе: {sleepingCarriage.CompartmentsCount}");
                        Console.WriteLine($"Наявність душу: {sleepingCarriage.HasShowers}");
                        Console.WriteLine($"Максимальна кількість пасажирів: {sleepingCarriage.MaxPassengers}");
                        Console.WriteLine($"Поточна кількість пасажирів: {sleepingCarriage.CurrentPassengers}");
                        break;
                    default:
                        Console.WriteLine("Невідомий тип вагону.");
                        break;
                }
                Console.WriteLine($"ID вагону: {carriage.Id}, Тип: {carriage.Type}, Вага: {carriage.Weight}, Довжина: {carriage.Length}, Тип матеріалу: {cargoType}");
                totalWeight += carriage.Weight;
                totalLength += carriage.Length;
            }
            Console.WriteLine($"Загальна довжина потягу: {totalLength} метрів");
            Console.WriteLine($"Загальна кількість пасажирів: {totalPassengers}");
            Console.WriteLine($"Загальна вага потягу: {totalWeight} тон");
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