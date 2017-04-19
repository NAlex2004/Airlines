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
            Console.WriteLine("\tAirline capacities:");
            Console.WriteLine("\nTotal cargo: {0}\nTotal passengers: {1}\n\n", airline.TotalCargoCapacity, airline.TotalPassengersCapacity);
        }

        static void WritePlanes(IAirline airline)
        {
            Console.WriteLine("\tAirline planes:\n");
            foreach (var plane in airline.Planes)
            {
                plane.WritePlaneInfo();
                Console.WriteLine();
            }
        }

        static void WritePlanesByFuelConsumption(IAirline airline, double minValue, double maxValue)
        {
            Console.WriteLine();
            Console.WriteLine("\tPlanes with fuel consumption in [{0}, {1}]:\n", minValue, maxValue);
            foreach (var plane in airline.FindByFuelConsumption(minValue, maxValue))
            {
                plane.WritePlaneInfo();
                Console.WriteLine();
            }
        }

        static IAirline CreateAirline()
        {
            string dir = ConfigurationManager.AppSettings["AirlineDirectory"];
            string pattern = ConfigurationManager.AppSettings["PlanePattern"];

            return new Airline(new AirlineFactory(dir, pattern));
        }

        static void Main(string[] args)
        {
            IAirline airline = CreateAirline();

            WriteAirlineSkills(airline);

            WritePlanes(airline);

            WritePlanesByFuelConsumption(airline, 30, 50);

            Console.ReadKey();
        }
    }
}
