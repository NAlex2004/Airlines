using NAlex.Airlines.Interfaces;
using NAlex.Airlines.Planes;

namespace NAlex.Airlines.Flights
{
    public class FlightDirector: IFlightPreparer
    {
        protected FlightParams flightParams;

        public FlightDirector(FlightParams flightParams)
        {
            this.flightParams = flightParams;           
        }

        public virtual bool CanFly(IPlane plane)
        {
            bool result = flightParams.FlightRange <= plane.FlightRange;

            if (result && flightParams.CargoWeight > 0)
            {
                ICargoable cargoable = plane as ICargoable;
                result &= (cargoable != null && (cargoable.CargoCapacity >= flightParams.CargoWeight));
            }

            if (result && flightParams.PassgengersCount > 0)
            {
                IPassengers passengers = plane as IPassengers;
                result &= (passengers != null && (passengers.PassengersCapacity >= flightParams.PassgengersCount));
            }

            return result;
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