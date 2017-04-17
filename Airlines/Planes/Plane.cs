using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NAlex.Airlines.Interfaces;

namespace NAlex.Airlines.Planes
{
    public abstract class Plane
    {
        private double fuelCount;

        public int? Id { get; protected set; }
        public string Number { get; set; }
        public string Manufacture { get; protected set; }
        /// <summary>
        /// Fuel consumption litres per 100 km
        /// </summary>
        public double FuelConsumption { get; protected set; }
        /// <summary>
        /// Flight range, km
        /// </summary>
        public int FlightRange { get; protected set; }
        public double FuelTankSize { get; protected set; }
        public double FuelCount 
        {
            get;
            protected set
            {
                fuelCount = Math.Min(Math.Max(0, value), FuelTankSize);
            }
        }

        public abstract bool PrepareForFlight(IFlightPreparer preparer);        
        public abstract bool Flight(out string flightResultMessage);
    }
}
