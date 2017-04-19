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
        static void Main(string[] args)
        {

            string dir = ConfigurationManager.AppSettings["AirlineDirectory"];
            string pattern = ConfigurationManager.AppSettings["PlanePattern"];

            IAirline airline = new Airline(new AirlineFactory(dir, pattern));

            Console.WriteLine("Total cargo: {0}\tTotal passengers: {1}", airline.TotalCargoCapacity, airline.TotalPassengersCapacity);

            foreach (var plane in airline.Planes)
            {
                plane.WritePlaneInfo();
                Console.WriteLine();
            }

            

            Console.ReadKey();
        }
    }
}
