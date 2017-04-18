using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NAlex.Airlines.Flights;
using NAlex.Airlines.Interfaces;

namespace NAlex.Airlines.Planes
{
    public class PassengerPlane: Plane, IPassengers
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

        public PassengerPlane(int flightRange, int fuelTankSize, string manufacture, int passengersCapasity)
            : base(flightRange, fuelTankSize, manufacture)
        {
            PassengersCapacity = passengersCapasity;
        }

        public override bool PrepareForFlight(IFlightPreparer preparer)
        {
            if (base.PrepareForFlight(preparer))
            {
                int passengers = preparer.GetPassengers() + PassengersOnBoard;
                if (passengers > PassengersCapacity)
                {
                    preparedForFlight = false;
                    Console.WriteLine("Plane has {0} passengers and cannot carry more than {1} passengers", PassengersOnBoard, PassengersCapacity);
                }
            }

            return preparedForFlight;
        }
    }
}
