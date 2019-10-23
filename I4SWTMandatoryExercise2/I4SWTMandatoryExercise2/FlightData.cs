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

        }

        // Evt. tilføj de resterende data til denne klasse

        public string ID {get; private set; }
        public int xCoordinate { get; set; }
        public int yCoordinate { get; set; }
        public int zCoordinate { get; set; }
        public DateTime timestamp { get; set; }
        public double CompassCourse { get; set; }
    }
}