﻿using System;
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
            if (base.PrepareForFlight (preparer))
            {
                PassengersOnBoard += preparer.GetPassengers ();
            }
                
            return preparedForFlight;
        }

        public override bool Flight(out string flightMessage)
        {
            bool res = base.Flight(out flightMessage);
            if (res)
                flightMessage += Environment.NewLine + "Passengers arrived.";
            return res;
        }

        public override string ToString ()
        {			
            return base.ToString ()
                + string.Format ("PassengersCapacity: {0}", PassengersCapacity)
                + Environment.NewLine;			
        }
            
    }
}
