using static LabaNomer2.Train;

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

            Console.WriteLine("Введіть кількість рушіїв:");
            int engineCount = Convert.ToInt32(Console.ReadLine());
            List<Engine> engines = new List<Engine>();
            for (int i = 0; i < engineCount; i++)
            {
                string engineId = $"R{i}";
                engines.Add(new Engine( 120, 800, 25));
            }

            Train train = new Train(trainName, engines, maxWeight);


            Console.WriteLine("Виберіть тип вагонів (1 - пасажирський, 2 - вантажний, 3 - вагон-ресторан, 4 - спальний вагон):");
            int type = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Введіть кількість вагонів:");
            int carriageCount = Convert.ToInt32(Console.ReadLine());

            train.AddCarriages(carriageCount, type);


            Console.WriteLine("Введіть відстань подорожі:");
            double distance = Convert.ToDouble(Console.ReadLine());

            Console.Clear();

            Console.WriteLine("Моделювання подорожі...");
            train.SimulateJourney(distance);
            train.PrintTrainInfo();

            JourneyInfo journeyInfo = train.GetJourneyInfo(distance);
            journeyInfo.PrintInfo();
        }
    }
}
