using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NAlex.Airlines.Planes;
using NAlex.Airlines.Interfaces;
using NAlex.Airlines.Flights;
using NAlex.Airlines.Collections;
using AirlineDemo.Factories;
using System.Configuration;
using System.IO;

namespace AirlineDemo
{
    class Program
    {
        static void WriteAirlineSkills(IAirline airline)
        {
            Console.WriteLine("\n\tAirline capacities:");
            Console.WriteLine("\nTotal cargo: {0}\nTotal passengers: {1}\n\n", airline.TotalCargoCapacity, airline.TotalPassengersCapacity);
        }

        static void WritePlanes(IAirline airline)
        {
            Console.WriteLine("\n\tAirline planes:\n");
            foreach (var plane in airline.Planes)
            {
				Console.WriteLine (plane.ToString ());
            }
        }

        static void WritePlanesByFuelConsumption(IAirline airline, double minValue, double maxValue)
        {
            Console.WriteLine();
            Console.WriteLine("\tPlanes with fuel consumption in [{0}, {1}]:\n", minValue, maxValue);
            foreach (var plane in airline.FindByFuelConsumption(minValue, maxValue))
            {
				Console.WriteLine (plane.ToString ());
            }
        }

        static void WritePassengersPlanes(IAirline airline)
        {
            Console.WriteLine("\n\tPassengers planes:\n");
            foreach (var plane in airline.PassengersPlanes())
            {
				Console.WriteLine (plane.ToString ());
            }
        }

        static IAirline CreateAirline()
        {
            string dir = ConfigurationManager.AppSettings["AirlineDirectory"];
            string pattern = ConfigurationManager.AppSettings["PlanePattern"];

            return new Airline(new AirlineFactory(dir, pattern));
        }

        static void PlanesCanFly(IAirline airline, FlightParams flightParams)
        {
            IFlightPreparer preparer = new FlightDirector(flightParams);
            Console.WriteLine("\n\tFlight parameters:\n");
            Console.WriteLine("Flight range: {0}", flightParams.FlightRange);
            Console.WriteLine("Passengers count: {0}", flightParams.PassgengersCount);
            Console.WriteLine("Cargo weight: {0}", flightParams.CargoWeight);
            Console.WriteLine("\n\tPlanes can make this flight:\n");
            var canFlyPlanes = airline.Planes.Where(p => preparer.CanFly(p));
            foreach (var plane in canFlyPlanes)
            {
				Console.WriteLine (plane.ToString ());
                if (plane.PrepareForFlight(preparer))
                {
                    Console.WriteLine("Flight begins..");
					string flightMessage;
					plane.Flight(out flightMessage);
					Console.WriteLine (flightMessage);
                }
            }
        }

        static void WriteFlightDemo(IAirline airline)
        {
            FlightParams flightParams;
            flightParams.FlightRange = 750;
            flightParams.PassgengersCount = 40;
            flightParams.CargoWeight = 300;
            PlanesCanFly(airline, flightParams);
        }

        static void Main(string[] args)
        {
            IAirline airline = CreateAirline();

            WriteAirlineSkills(airline);
            WritePlanes(airline);		
            WritePassengersPlanes(airline);
            WritePlanesByFuelConsumption(airline, 30, 50);
            WriteFlightDemo(airline);

            Console.Write("Any key to exit.");
            Console.ReadKey();
        }
    }
}
