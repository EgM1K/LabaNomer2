using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
}
