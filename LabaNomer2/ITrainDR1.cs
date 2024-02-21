using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabaNomer2
{
    public interface ITrain
    {
        string Name { get; set; }
        double MaxWeight { get; set; }
        List<Carriage> Carriages { get; set; }

        void AddCarriage(Carriage carriage);
        void SimulateJourney();
        void PrintTrainInfo();
    }
}
