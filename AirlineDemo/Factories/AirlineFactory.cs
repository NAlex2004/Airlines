using System;
using System.Collections.Generic;
using NAlex.Airlines.Interfaces;
using System.IO;

namespace AirlineDemo.Factories
{
    public class AirlineFactory: IAirlineFactory
    {
        public ICollection<IPlane> CreateAirlinePlanes()
        {
            throw new NotImplementedException();
        }
    }
}
