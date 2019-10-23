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

        

        // Evt. tilføj de resterende data til denne klasse

        public string ID {get; private set; }
        public int xCoordinate { get; private set; }
        public int yCoordinate { get; private set; }
        public int zCoordinate { get; private set; }
        public DateTime timestamp { get; private set; }
        public double CompassCourse { get; set; }
    }
}