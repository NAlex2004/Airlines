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
            base.PrepareForFlight(preparer);

            int cargo = preparer.GetCargo() + CargoLoad;
            if (cargo > CargoCapacity)
            {
                preparedForFlight = false;
                Console.WriteLine("Plane has {0} kg cargo and cannot carry more than {1} kg", CargoLoad, CargoCapacity);
            }

            return preparedForFlight;
        }

        public override bool Flight()
        {
            bool res = base.Flight();
            if (res)
                Console.WriteLine("Cargo delivered.");
            return res;
        }

        public override void WritePlaneInfo()
        {
            base.WritePlaneInfo();
            Console.WriteLine("CargoCapacity: {0}", CargoCapacity);
        }
    }
}
