using NAlex.Airlines.Interfaces;
using NAlex.Airlines.Planes;
using System;

namespace NAlex.Airlines.Flights
{
    public class FlightDirector: IFlightPreparer
    {
        protected FlightParams flightParams;

        public FlightDirector(FlightParams flightParams)
        {
            this.flightParams = flightParams;           
			LastResult = string.Empty;
        }

        public virtual bool CanFly(IPlane plane)
        {
			LastResult = string.Empty;
			bool result = flightParams.FlightRange <= plane.FlightRange;

			if (!result)
				LastResult = string.Format ("Plane cannot flight {1} rm, maximum flight range is {0} km.", plane.FlightRange, flightParams.FlightRange);

			if (flightParams.CargoWeight > 0)
			{
				ICargoable cargoable = plane as ICargoable;
				result &= (cargoable != null);// && (cargoable.CargoCapacity >= (flightParams.CargoWeight + cargoable.CargoLoad)));
				if (cargoable == null)
				{
					LastResult += (string.IsNullOrEmpty (LastResult) ? "" : Environment.NewLine)
						+ string.Format ("Plane is not cargoable and cannot carry {0} kg", flightParams.CargoWeight);
				} 
				else if (cargoable.CargoCapacity < (flightParams.CargoWeight + cargoable.CargoLoad))
				{
					result = false;
					LastResult += (string.IsNullOrEmpty (LastResult) ? "" : Environment.NewLine)
						+ string.Format ("Plane has {0} kg cargo and cannot carry more than {1} kg", cargoable.CargoLoad, cargoable.CargoCapacity);
				}
			}

			if (flightParams.PassgengersCount > 0)
			{
				IPassengers passengers = plane as IPassengers;
				result &= (passengers != null);// && (passengers.PassengersCapacity >= (flightParams.PassgengersCount + passengers.PassengersOnBoard)));
				if (passengers == null)
				{
					LastResult += (string.IsNullOrEmpty (LastResult) ? "" : Environment.NewLine)
						+ string.Format ("Plane is not passengers and cannot carry {0} passengers.", flightParams.PassgengersCount);
				}
				else if (passengers.PassengersCapacity < (flightParams.PassgengersCount + passengers.PassengersOnBoard))
				{
					result = false;
					LastResult += (string.IsNullOrEmpty (LastResult) ? "" : Environment.NewLine)
						+ string.Format ("Plane has {0} passengers and cannot carry more than {1} passengers", passengers.PassengersOnBoard, passengers.PassengersCapacity);
				}
			}

            return result;
        }

		public string LastResult
		{
			get;
			protected set;
		}

        public virtual double GetFuel(IPlane plane)
        {
            return plane.FuelTankSize;
        }

        public virtual int GetCargo()
        {
            return flightParams.CargoWeight;
        }

        public virtual int GetPassengers()
        {
            return flightParams.PassgengersCount;
        }


        public virtual int GetFlightRange()
        {
            return flightParams.FlightRange;
        }
    }
}