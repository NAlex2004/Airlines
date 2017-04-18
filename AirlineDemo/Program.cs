using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NAlex.Airlines.Planes;
using NAlex.Airlines.Interfaces;
using NAlex.Airlines.Flights;

namespace AirlineDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            List<IPlane> planes = new List<IPlane>()
            {
                new Plane(1000, 100, "Boeng"),
                new CargoPlane(1000, 200, "Tupolev", 200),
                new PassengerPlane(600, 150, "ChinaFly", 50),
                new CargoPassengerPlane(1000, 300, "UFO", 110, 30)
            };
                        
            FlightParams fParams = new FlightParams() { CargoWeight = 400, FlightRange = 10000, PassgengersCount = 300 };

            IFlightPreparer preparer = new FlightDirector(fParams);

            foreach (var plane in planes)
            {
                Console.WriteLine("Plane: {0}\tFuelTank: {1}\tFuelConsumption: {2}", plane.Manufacture, plane.FuelTankSize, plane.FuelConsumption);
                Console.WriteLine("Can fly: {0}", plane.PrepareForFlight(preparer));
                plane.Flight();
            }


            Console.ReadKey();
        }
    }
}
