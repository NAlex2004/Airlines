using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NAlex.Airlines.Interfaces;
using NAlex.Airlines.Flights;
using NAlex.Airlines.Planes;

namespace NAlex.Airlines.Collections
{
    public class Airline: IAirline
    {
        private ICollection<IPlane> planes;

        public Airline(IAirlineFactory factory)
        {
            planes = factory.CreateAirlinePlanes();
        }

        public int TotalPassengersCapacity
        {
            get 
            {
                return planes.OfType<IPassengers>().Sum(p => p.PassengersCapacity);
            }
        }

        public int TotalCargoCapacity
        {
            get
            {
                return planes.OfType<ICargoable>().Sum(p => p.CargoCapacity);
            }
        }

        /// <summary>
        /// Ordered by flight range
        /// </summary>
        public IEnumerable<IPlane> Planes
        {
            get
            {
                return planes.OrderBy(p => p.FlightRange);
            }
        }

        public IEnumerable<IPlane> FindByFuelConsumption(double minValue, double maxValue)
        {
            return planes.Where(p => p.FuelConsumption >= minValue && p.FuelConsumption <= maxValue);
        }

        public IEnumerable<IPlane> CanCarryCargo()
        {
            return planes.OfType<ICargoable>().Cast<IPlane>();
        }

        public IEnumerable<IPlane> CanCarryPassengers()
        {
            return planes.OfType<IPassengers>().Cast<IPlane>();
        }
    }
}
