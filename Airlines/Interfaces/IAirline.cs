using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NAlex.Airlines.Interfaces
{
    public interface IAirline
    {
        int TotalPassengersCapacity { get; }
        int TotalCargoCapacity { get; }
        IEnumerable<IPlane> Planes { get; }

        IEnumerable<IPlane> FindByFuelConsumption(double minValue, double maxValue);
        IEnumerable<IPlane> CargoPlanes();
        IEnumerable<IPlane> PassengersPlanes();
    }
}
