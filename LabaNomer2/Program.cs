namespace LabaNomer2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Введіть назву потягу:");
            string trainName = Console.ReadLine();
            double maxWeight = 800;
            Console.WriteLine($"Максимальна вага потягу: {maxWeight} тон");
            Train train = new Train(trainName, maxWeight);

            Console.WriteLine("Введіть кількість вагонів:");
            int carriageCount = Convert.ToInt32(Console.ReadLine());
            for (int i = 0; i < carriageCount; i++)
            {
                Console.Clear();
                Console.WriteLine($"Введіть дані для вагона {i + 1}:");
                Console.WriteLine("Тип вагона (1 - пасажирський, 2 - вантажний):");
                int type = Convert.ToInt32(Console.ReadLine());
                switch (type)
                {
                    case 1:
                        Console.WriteLine("Введіть кількість пасажирів (максимум 100):");
                        int passengers = Convert.ToInt32(Console.ReadLine());
                        PassengerCarriage passengerCarriage = new PassengerCarriage(i.ToString(), 25, 10, 100, passengers);
                        train.AddCarriage(passengerCarriage);
                        break;
                    case 2:
                        Console.WriteLine("Введіть вагу вантажу (максимум 100 тон):");
                        double load = Convert.ToDouble(Console.ReadLine());
                        FreightCarriage freightCarriage = new FreightCarriage(i.ToString(), 25, 10, load);
                        train.AddCarriage(freightCarriage);
                        break;
                    default:
                        Console.WriteLine("Невірний вибір. Спробуйте ще раз.");
                        i--;
                        break;
                }
            }

            Console.WriteLine("Введіть відстань подорожі:");
            double distance = Convert.ToDouble(Console.ReadLine());

            Console.Clear();
            train.PrintTrainInfo();

            Console.WriteLine("Моделювання подорожі...");
            train.SimulateJourney(distance);
            train.PrintTrainInfo();
        }
    }
}
