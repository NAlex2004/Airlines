using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NAlex.Airlines.Flights;
using NAlex.Airlines.Interfaces;

namespace NAlex.Airlines.Planes
{
    public class Plane : IPlane
    {
        private double fuelCount;
        private int flightRange = 1;
        private double fuelTankSize = 1;

        protected bool preparedForFlight = false;
        protected int missionFlightRange = 0;

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
            get { return fuelCount; }
            set
            {
                fuelCount = Math.Min(Math.Max(0, value), FuelTankSize);
            }
        }

        public Plane(int flightRange, int fuelTankSize, string manufacture)
        {
            FlightRange = flightRange;
            FuelTankSize = fuelTankSize;
            Manufacture = manufacture;
            Number = "";
        }

        public virtual bool PrepareForFlight(IFlightPreparer preparer)
        {
            preparedForFlight = preparer.CanFly(this);
            if (preparedForFlight)
            {
                fuelCount = preparer.GetFuel(this);
                missionFlightRange = preparer.GetFlightRange();
            }
            return preparedForFlight;
        }

        public virtual bool Flight()
        {
            if (!preparedForFlight)
            {
                Console.WriteLine("This plane is not prepared for flight!");
                return false;
            }

            preparedForFlight = false;
            //---------
            double maximumRange = FuelCount * 100 / FuelConsumption;
            if (maximumRange < missionFlightRange)
            {
                Console.WriteLine("Not enough fuel.");
                return false;
            }
            //----------
            Console.WriteLine("Flight completed.");
            return true;
        }
    }
}
