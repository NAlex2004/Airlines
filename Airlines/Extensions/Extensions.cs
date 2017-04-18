using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NAlex.Airlines.Interfaces;

namespace NAlex.Airlines.Extensions
{
    public static class Extensions
    {
        public static bool IsCargoPlane(this IPlane plane)
        {
            return plane is ICargoable;
        }

        public static bool IsPassengersPlane(this IPlane plane)
        {
            return plane is IPassengers;
        }
    }
}
