using System;

namespace I4SWTMandatoryExercise2
{
    public class FlightData
    {
        public FlightData(string id)
        {
            ID = id;
        }
        public void SetFlightData(int x, int y, int z, DateTime time)
        {
            xCoordinate = x;
            yCoordinate = y;
            zCoordinate = z;
            timestamp = time;
        }

        public void SetCompassCourse(double compassCourse)
        {
            CompassCourse = compassCourse;
        }

        public double CalculateCompassCourse(Point a, Point b)
        {
            double w = b._x - a._x;
            double h = b._y - a._y;

            double atan = Math.Atan(h / w) / Math.PI * 180;
            if (w < 0 || h < 0)
                atan += 180;
            if (w > 0 && h < 0)
                atan -= 180;
            if (atan < 0)
                atan += 360;

            double degrees = atan % 360;
            return degrees;
            
            //0 - 22.5 N
            //22.6 - 67.5 - N E
            // 67.6 - 112.5 E
            // 112.6 - 157.5 S E
            // 157.6 - 202.5 S
            // 202.6 - 247.5 S W
            // 247.6 - 292.5 W
            // 292.6 - 337.5 N W
            // 337.6 - 359.9 N

            //string CompassCourse = "";

            //if ((degrees >= 0 && degrees <= 22.5) || (degrees >= 337.6 && degrees <= 359.9))
            //{
            //    CompassCourse = "N";
            //}
            //else if (degrees >= 22.6 && degrees <= 67.5)
            //{
            //    CompassCourse = "N E";
            //}
            //else if (degrees >= 67.6 && degrees <= 112.5)
            //{
            //    CompassCourse = "E";
            //}
            //else if (degrees >= 112.6 && degrees <= 157.5)
            //{
            //    CompassCourse = "S E";
            //}
            //else if (degrees >= 157.6 && degrees <= 202.5)
            //{
            //    CompassCourse = "S";
            //}
            //else if (degrees >= 202.6 && degrees <= 247.5)
            //{
            //    CompassCourse = "S W";
            //}
            //else if (degrees >= 247.6 && degrees <= 292.5)
            //{
            //    CompassCourse = "W";
            //}
            //else if (degrees >= 292.6 && degrees <= 337.5)
            //{
            //    CompassCourse = "N W";
            //}

            //return $"Flight {id} is flying {CompassCourse} with a course {degrees}";

        }

        // Evt. tilføj de resterende data til denne klasse

        public string ID {get; private set; }
        public int xCoordinate { get; private set; }
        public int yCoordinate { get; private set; }
        public int zCoordinate { get; private set; }
        public DateTime timestamp { get; private set; }
        public double CompassCourse { get; private set; }
    }
}