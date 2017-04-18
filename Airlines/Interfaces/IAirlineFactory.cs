using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NAlex.Airlines.Interfaces
{
    public interface IAirlineFactory
    {
        ICollection<IPlane> CreateAirlinePlanes();
    }
}
