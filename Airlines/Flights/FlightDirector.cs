using NAlex.Airlines.Interfaces;
using NAlex.Airlines.Planes;

namespace NAlex.Airlines.Flights
{
    public static class FlightDirector
    {
        public static bool CanFly(IPlane plane, FlightParams flightParams)
        {
            bool result = flightParams.FlightRange <= plane.FlightRange;

            if (flightParams.CargoWeight > 0)
            {
                ICargoable cargoable = plane as ICargoable;
                result &= (cargoable != null && (cargoable.CargoCapacity >= flightParams.CargoWeight));
            }

            if (flightParams.PassgengersCount > 0)
            {
                IPassengers passengers = plane as IPassengers;
                result &= (passengers != null && (passengers.PassengersCapacity >= flightParams.PassgengersCount));
            }

            return result;
        }
    }
}