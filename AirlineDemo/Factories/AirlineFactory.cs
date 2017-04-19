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
            var files = Directory.EnumerateFiles(filePattern);
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
            return planes;
        }

        private IPlane CreatePlane(string[] config)
        {
            IPlane plane = null;
            var dict = config.Where(s => !string.IsNullOrEmpty(s))
                .Select(s => s.Split('='))
                .Where(s => s.Length > 1)
                .ToDictionary(k => k[0].Trim(), v => v[1].Trim());

            string number = string.Empty;
            string manufacture = string.Empty;
            int flightRange = 0;
            double fuelTankSize = 0;
            int cargoCapacity = 0;
            int passengersCapacity = 0;

            foreach (string s in dict.Keys)
            {
                switch (s.ToUpper())
                {
                    case "NUMBER":
                        number = dict[s];
                        break;
                    case "MANUFACTURE":
                        manufacture = dict[s];
                        break;
                    case "FLIGHTRANGE":
                        Int32.TryParse(dict[s], out flightRange);
                        break;
                    case "FUELTANKSIZE":
                        double.TryParse(dict[s], out fuelTankSize);
                        break;
                    case "CARGOCAPACITY":
                        Int32.TryParse(dict[s], out cargoCapacity);
                        break;
                    case "PASSENGERSCAPACITY":
                        Int32.TryParse(dict[s], out passengersCapacity);
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
                    return plane;
                }

                if (passengersCapacity > 0)
                    plane = new PassengerPlane(flightRange, fuelTankSize, manufacture, passengersCapacity);
                else
                    plane = new Plane(flightRange, fuelTankSize, manufacture);
            }

            return plane;
        }
    }
}
