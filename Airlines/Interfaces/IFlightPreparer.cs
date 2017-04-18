
namespace NAlex.Airlines.Interfaces
{
    public interface IFlightPreparer
    {
        bool CanFly(IPlane plane);
        int GetFlightRange();
        double GetFuel(IPlane plane);
        int GetCargo();
        int GetPassengers();
    }
}
