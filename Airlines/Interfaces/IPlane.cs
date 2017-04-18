using NAlex.Airlines.Flights;

namespace NAlex.Airlines.Interfaces
{
    public interface IPlane
    {
        int? Id { get; }
        string Number { get; set; }
        string Manufacture { get; }
        /// <summary>
        /// Fuel consumption liters per 100 km
        /// </summary>
        double FuelConsumption { get; }
        /// <summary>
        /// Flight range, km. Greater or equal 1.
        /// </summary>
        int FlightRange { get; }
        /// <summary>
        /// Fuel tank size, liters. Greater or equal 1.
        /// </summary>
        double FuelTankSize { get; }
        double FuelCount { get; set; }

        bool PrepareForFlight(IFlightPreparer preparer);
        bool Flight();
    }
}