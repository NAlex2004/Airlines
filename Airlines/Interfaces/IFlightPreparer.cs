
namespace NAlex.Airlines.Interfaces
{
    public interface IFlightPreparer
    {
        int GetFlightRange();
        double GetFuel();
        int GetCargo();
        int GetPassengers();
    }
}
