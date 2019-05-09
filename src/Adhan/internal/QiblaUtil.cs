using System;
using Batoulapps.Adhan;

namespace Batoulapps.Adhan.Internal
{
    internal class QiblaUtil 
    {
        private readonly static Coordinates MAKKAH = new Coordinates(21.4225241, 39.8261818);

        internal static double CalculateQiblaDirection(Coordinates coordinates)
        {
            // Equation from "Spherical Trigonometry For the use of colleges and schools" page 50
            double longitudeDelta =
                MathHelper.ToRadians(MAKKAH.Longitude) -  MathHelper.ToRadians(coordinates.Longitude);
            double latitudeRadians = MathHelper.ToRadians(coordinates.Latitude);
            double term1 = Math.Sin(longitudeDelta);
            double term2 = Math.Cos(latitudeRadians) * Math.Tan( MathHelper.ToRadians(MAKKAH.Latitude));
            double term3 = Math.Sin(latitudeRadians) * Math.Cos(longitudeDelta);

            double angle = Math.Atan2(term1, term2 - term3);
            return DoubleUtil.UnwindAngle(MathHelper.ToDegrees(angle));
        }
    }
}