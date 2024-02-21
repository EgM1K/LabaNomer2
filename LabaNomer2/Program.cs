namespace LabaNomer2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Введіть назву потягу:");
            string trainName = Console.ReadLine();
            Console.WriteLine("Введіть максимальну вагу потягу:");
            double maxWeight = Convert.ToDouble(Console.ReadLine());
            Train train = new Train(trainName, maxWeight);

            Console.WriteLine("Введіть кількість вагонів:");
            int carriageCount = Convert.ToInt32(Console.ReadLine());
            for (int i = 0; i < carriageCount; i++)
            {
                Console.WriteLine($"Введіть дані для вагона {i + 1}:");
                Console.WriteLine("Тип вагона (passenger, freight):");
                string type = Console.ReadLine();
                if (type == "passenger")
                {
                    Console.WriteLine("Введіть кількість пасажирів:");
                    int passengers = Convert.ToInt32(Console.ReadLine());
                    PassengerCarriage passengerCarriage = new PassengerCarriage(i.ToString(), 25, 10, 100, passengers);
                    train.AddCarriage(passengerCarriage);
                }
                else if (type == "freight")
                {
                    Console.WriteLine("Введіть вагу вантажу:");
                    double load = Convert.ToDouble(Console.ReadLine());
                    FreightCarriage freightCarriage = new FreightCarriage(i.ToString(), 25, 10, load);
                    train.AddCarriage(freightCarriage);
                }
            }

            Console.WriteLine("Введіть відстань подорожі:");
            double distance = Convert.ToDouble(Console.ReadLine());

            train.PrintTrainInfo();

            Console.WriteLine("Моделювання подорожі...");
            train.SimulateJourney(distance);
            train.PrintTrainInfo();
        }
    }
}
