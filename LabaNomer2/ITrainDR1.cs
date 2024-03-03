using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabaNomer2
{
    public interface ITrainDR1
    {
        string Name { get; set; }
        double MaxWeight { get; set; }
        List<Carriage> Carriages { get; set; }

        void AddCarriage(Carriage carriage);
        void SimulateJourneyWithStops(double distance);
        void PrintTrainInfo();
       
    }
}
