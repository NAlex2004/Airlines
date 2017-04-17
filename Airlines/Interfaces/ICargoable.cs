using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NAlex.Airlines.Interfaces
{
    public interface ICargoable
    {
        /// <summary>
        /// Cargo capacity, kg
        /// </summary>
        int CargoCapacity { get; }
        /// <summary>
        /// Current cargo load, kg
        /// </summary>
        int CargoLoad { get; }
    }
}
