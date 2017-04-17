using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NAlex.Airlines.Interfaces
{
    public interface IFlightPreparer
    {
        int GetFlightRange();
        double GetFuel();
        int GetCargo();
        int GetPassengers();
    }
}
