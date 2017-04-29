
namespace NAlex.Airlines.Interfaces
{
    public interface IFlightPreparer
    {
        bool CanFly(IPlane plane);
        string LastResult { get; }
        int GetFlightRange();
        double GetFuel(IPlane plane);
        int GetCargo();
        int GetPassengers();
    }
}
