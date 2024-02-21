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

        public Train(string id, List<Engine> engines, double maxWeight)
        {
            Id = id;
            Engines = engines;
            MaxWeight = maxWeight;
            Carriages = new List<Carriage>();
        }

        public void AddCarriage(Carriage carriage)
        {
            double currentWeight = Carriages.Sum(c => c.Weight + c.LoadCapacity);
            if (currentWeight + carriage.Weight + carriage.LoadCapacity > MaxWeight)
            {
                Console.WriteLine("Не можна додати вагон, оскільки це перевищить максимальну вагу потягу.");
            }
            else
            {
                Carriages.Add(carriage);
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
            Console.WriteLine($"Train ID: {Id}");
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
    }
}

