using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace LabaNomer2
{
    public class Train
    {
        public string Name { get; set; }
        public double MaxWeight { get; set; }
        public List<Carriage> Carriages { get; set; }

        public Train(string name, double maxWeight)
        {
            Name = name;
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
    }
    public void SimulateJourney()
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
        Console.WriteLine($"Назва потягу: {Name}");
        Console.WriteLine($"Максимальна вага: {MaxWeight}");
        Console.WriteLine($"Кількість вагонів: {Carriages.Count}");
        Console.WriteLine("Інформація про вагони:");
        foreach (var carriage in Carriages)
        {
            Console.WriteLine($"ID: {carriage.Id}, Тип: {carriage.Type}, Вага: {carriage.Weight}, Довжина: {carriage.Length}");
            if (carriage is PassengerCarriage passengerCarriage)
            {
                Console.WriteLine($"Кількість пасажирів: {passengerCarriage.Passengers}");
            }
            else if (carriage is FreightCarriage freightCarriage)
            {
                Console.WriteLine($"Вантаж: {freightCarriage.Load}");
            }
        }
    }
}
