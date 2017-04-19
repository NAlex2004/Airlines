using System;
using System.Collections.Generic;
using NAlex.Airlines.Interfaces;
using System.IO;
using System.Linq;
using NAlex.Airlines.Planes;

namespace AirlineDemo.Factories
{
    public class AirlineFactory: IAirlineFactory
    {
        private string airlineDirectory;
        private string filePattern;

        public AirlineFactory(string airlineDirectoryPath, string planesFilePattern)
        {
            airlineDirectory = airlineDirectoryPath;
            filePattern = planesFilePattern;
        }

        public ICollection<IPlane> CreateAirlinePlanes()
        {
            List<IPlane> planes = new List<IPlane>();
            try
            {
                var files = Directory.EnumerateFiles(airlineDirectory, filePattern);
                foreach (var file in files)
                {
                    string[] content = File.ReadAllLines(file);
                    if (content.Length > 0)
                    {
                        IPlane plane = CreatePlane(content);
                        if (plane != null)
                            planes.Add(plane);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return planes;
        }

        private IPlane CreatePlane(string[] config)
        {
            IPlane plane = null;
            var dict = config.Where(s => !string.IsNullOrEmpty(s))
                .Select(s => s.Split('='))
                .Where(s => s.Length > 1)
                .Select(s => new string[2] { s[0].Trim(), s[1].Trim() });
                //.ToDictionary(k => k[0].Trim(), v => v[1].Trim());

            string number = string.Empty;
            string manufacture = string.Empty;
            int flightRange = 0;
            double fuelTankSize = 0;
            int cargoCapacity = 0;
            int passengersCapacity = 0;

            foreach (string[] s in dict)
            {
                switch (s[0].ToUpper())
                {
                    case "NUMBER":
                        number = s[1];
                        break;
                    case "MANUFACTURE":
                        manufacture = s[1];
                        break;
                    case "FLIGHTRANGE":
                        Int32.TryParse(s[1], out flightRange);
                        break;
                    case "FUELTANKSIZE":
                        double.TryParse(s[1], out fuelTankSize);
                        break;
                    case "CARGOCAPACITY":
                        Int32.TryParse(s[1], out cargoCapacity);
                        break;
                    case "PASSENGERSCAPACITY":
                        Int32.TryParse(s[1], out passengersCapacity);
                        break;
                    default:
                        break;
                }
            }

            if (flightRange > 0 && fuelTankSize > 0)
            {
                if (cargoCapacity > 0)
                {
                    if (passengersCapacity > 0)
                        plane = new CargoPassengerPlane(flightRange, fuelTankSize, manufacture, cargoCapacity, passengersCapacity);
                    else
                        plane = new CargoPlane(flightRange, fuelTankSize, manufacture, cargoCapacity);
                }
                else
                    if (passengersCapacity > 0)
                        plane = new PassengerPlane(flightRange, fuelTankSize, manufacture, passengersCapacity);
                    else
                        plane = new Plane(flightRange, fuelTankSize, manufacture);
                plane.Number = number;
            }
            
            return plane;
        }
    }
}
