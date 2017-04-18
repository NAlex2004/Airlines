using System;
using System.Collections.Generic;
using NAlex.Airlines.Interfaces;
using System.IO;

namespace AirlineDemo.Factories
{
    public class AirlineFactory: IAirlineFactory
    {
        private DirectoryInfo airlineDirectory;
        private string filePattern;

        public AirlineFactory(string airlineDirectoryPath, string planesFilePattern)
        {
            airlineDirectory = new DirectoryInfo(airlineDirectoryPath);
            filePattern = planesFilePattern;
        }

        public ICollection<IPlane> CreateAirlinePlanes()
        {
            List<IPlane> planes = new List<IPlane>();
            var files = airlineDirectory.EnumerateFiles(filePattern);
            foreach (var file in files)
            {
                string[] content = File.ReadAllLines(file.FullName);
                IPlane plane = CreatePlane(content);
                if (plane != null)
                    planes.Add(plane);
            }
            return planes;
        }

        private IPlane CreatePlane(string[] config)
        {
            IPlane plane = null;


            return plane;
        }
    }
}
