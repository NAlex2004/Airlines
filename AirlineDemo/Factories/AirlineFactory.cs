using System;
using System.Collections.Generic;
using NAlex.Airlines.Interfaces;
using System.IO;
using System.Linq;

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
                .ToDictionary(k => k[0], v => v[1]);

            return plane;
        }
    }
}
