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
            protected set
            {
                fuelCount = Math.Min(Math.Max(0, value), FuelTankSize);
            }
        }

        public Plane(int flightRange, double fuelTankSize, string manufacture)
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
                FuelCount = preparer.GetFuel(this);
                missionFlightRange = preparer.GetFlightRange();
            }

            return preparedForFlight;
        }

        public virtual bool Flight(out string flightMessage)
        {
            if (!preparedForFlight)
            {
                flightMessage = "This plane is not prepared for flight!";
                return false;
            }

            preparedForFlight = false;

            flightMessage = "Flight completed";
            return true;
        }

        public override string ToString ()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(string.Format ("Manufacture: {0}\tFlightRange: {1}", Manufacture, FlightRange));
            sb.AppendLine(string.Format("Number: {0}\nFuelTankSize: {1}\nFuelConsumption: {2}", Number, FuelTankSize, FuelConsumption));
            return sb.ToString ();
        }
            
    }
}
