using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NAlex.Airlines.Interfaces;

namespace NAlex.Airlines.Planes
{
    public class Plane
    {
        public int? Id { get; protected set; }
        public string Number { get; set; }
        public string Manufacture { get; protected set; }
        /// <summary>
        /// Fuel consumption litres per hour
        /// </summary>
        public double FuelConsumption { get; protected set; }
        /// <summary>
        /// Flight range, km
        /// </summary>
        public int FlightRange { get; protected set; }
    }
}
