using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NAlex.Airlines.Interfaces;
using NAlex.Airlines.Flights;

namespace NAlex.Airlines.Planes
{
    public class CargoPlane: Plane, ICargoable
    {
        private int cargoCapacity;
        private int cargoLoad;

        public int CargoCapacity
        {
            get { return cargoCapacity; }
            protected set
            {
                cargoCapacity = Math.Max(0, value);
            }
        }

        public int CargoLoad
        {
            get { return cargoLoad; }
            set
            {
                cargoLoad = Math.Min(Math.Max(0, value), cargoCapacity);
            }
        }

        public CargoPlane(int flightRange, double fuelTankSize, string manufacture, int cargoCapacity)
            : base(flightRange, fuelTankSize, manufacture)
        {
            CargoCapacity = cargoCapacity;
        }

        public override bool PrepareForFlight(IFlightPreparer preparer)
        {
            if (base.PrepareForFlight (preparer))
            {
                CargoLoad += preparer.GetCargo ();
            }
                
            return preparedForFlight;
        }

        public override bool Flight(out string flightMessage)
        {
            bool res = base.Flight(out flightMessage);
            if (res)
                flightMessage += Environment.NewLine + "Cargo delivered.";
            return res;
        }

        public override string ToString ()
        {			
            return base.ToString ()
                + string.Format ("CargoCapacity: {0}", CargoCapacity)
                   + Environment.NewLine;
        }
                   
    }
}
