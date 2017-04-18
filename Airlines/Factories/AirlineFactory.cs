using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NAlex.Airlines.Interfaces;
using NAlex.Airlines.Planes;

namespace NAlex.Airlines.Factories
{
    public class AirlineFactory: IAirlineFactory
    {
        public ICollection<IPlane> CreateAirlinePlanes()
        {
            throw new NotImplementedException();
        }
    }
}
