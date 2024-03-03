using static LabaNomer2.Train;

namespace LabaNomer2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;


            List<Engine> engines = new List<Engine> { new Engine(120, 800, 25), new Engine(120, 800, 25) };
            Train train = new Train("someId", engines);
            train.InitializeTrain();
            Console.Write("Введіть відстань подорожі: ");
            double distance = Convert.ToDouble(Console.ReadLine());

            Console.Clear();
            Console.WriteLine("Моделювання подорожі...");

            train.SimulateJourneyWithStops(distance);
            train.PrintTrainInfo();

            JourneyInfo journeyInfo = train.GetJourneyInfo(distance);
            journeyInfo.PrintInfo();
        }
    }
}