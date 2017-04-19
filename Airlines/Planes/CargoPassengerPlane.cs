using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NAlex.Airlines.Interfaces;
using NAlex.Airlines.Flights;

namespace NAlex.Airlines.Planes
{
    public class CargoPassengerPlane: CargoPlane, IPassengers
    {
        private int passengersCapacity;
        private int passengersOnBoard;

        public int PassengersCapacity
        {
            get { return passengersCapacity; }
            protected set
            {
                passengersCapacity = Math.Max(0, value);
            }
        }

        public int PassengersOnBoard
        {
            get { return passengersOnBoard; }
            set
            {
                passengersOnBoard = Math.Min(Math.Max(0, value), passengersCapacity);
            }
        }

        public CargoPassengerPlane(int flightRange, double fuelTankSize, string manufacture, int cargoCapacity, int passengersCapasity)
            : base(flightRange, fuelTankSize, manufacture, cargoCapacity)
        {
            PassengersCapacity = passengersCapasity;
        }

        public override bool PrepareForFlight(IFlightPreparer preparer)
        {
            base.PrepareForFlight(preparer);
            int passengers = preparer.GetPassengers() + PassengersOnBoard;
            if (passengers > PassengersCapacity)
            {
                preparedForFlight = false;
                Console.WriteLine("Plane has {0} passengers and cannot carry more than {1} passengers", PassengersOnBoard, PassengersCapacity);
            }

            return preparedForFlight;
        }

        public override bool Flight()
        {
            bool res = base.Flight();
            if (res)
                Console.WriteLine("Passengers arrived.");
            return res;
        }
    }
}
