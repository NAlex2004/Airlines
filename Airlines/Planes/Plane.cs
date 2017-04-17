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
        private int flightRange = 1;
        private double fuelTankSize = 1;

        public int? Id { get; protected set; }
        public string Number { get; set; }
        public string Manufacture { get; protected set; }
        /// <summary>
        /// Fuel consumption liters per 100 km
        /// </summary>
        public double FuelConsumption 
        {
            get
            {
                return 100 * FuelTankSize / FlightRange;
            }
        }
        /// <summary>
        /// Flight range, km. Greater or equal 1.
        /// </summary>
        public int FlightRange 
        {
            get { return flightRange; }
            protected set
            {
                flightRange = Math.Max(1, value);
            }
        }
        /// <summary>
        /// Fuel tank size, liters. Greater or equal 1.
        /// </summary>
        public double FuelTankSize 
        {
            get { return fuelTankSize; }
            protected set
            {
                fuelTankSize = Math.Max(1, value);
            }
        }

        public double FuelCount 
        {
            get;
            protected set
            {
                fuelCount = Math.Min(Math.Max(0, value), FuelTankSize);
            }
        }

        public Plane(int flightRange, int fuelTankSize, string manufacture, string number)
        {
            FlightRange = flightRange;
            FuelTankSize = fuelTankSize;
            Manufacture = manufacture;
            Number = number;
        }

        public abstract bool PrepareForFlight(IFlightPreparer preparer);        
        public abstract bool Flight(out string flightResultMessage);
    }
}
