using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabaNomer2
{
    public class JourneyInfo
    {
        public double Distance { get; set; }
        public double Time { get; set; }
        public double Speed { get; set; }
        public double Load { get; set; }

        public JourneyInfo(double distance, double time, double speed, double load)
        {
            Distance = distance;
            Time = time;
            Speed = speed;
            Load = load;
        }

        public void PrintInfo()
        {
            Console.WriteLine($"Відстань подорожі: {Distance} км");
            Console.WriteLine($"Час подорожі: {Time} годин");
            Console.WriteLine($"Швидкість потягу: {Speed} км/год");
            Console.WriteLine($"Вага вантажу: {Load} тон");
        }
    }
}
