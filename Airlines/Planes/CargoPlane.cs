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
            protected set
            {
                cargoLoad = Math.Min(Math.Max(0, value), cargoCapacity);
            }
        }

        public CargoPlane(int flightRange, int fuelTankSize, string manufacture, int cargoCapacity)
            : base(flightRange, fuelTankSize, manufacture)
        {
            CargoCapacity = cargoCapacity;
        }

        public override bool PrepareForFlight(IFlightPreparer preparer)
        {
            if (base.PrepareForFlight(preparer))
            {
                int cargo = preparer.GetCargo() + CargoLoad;
                if (cargo > CargoCapacity)
                {
                    preparedForFlight = false;
                    Console.WriteLine("Plane has {0} kg cargo and cannot carry more than {1} kg", CargoLoad, CargoCapacity);
                }
            }
            
            return preparedForFlight;
        }
    }
}
