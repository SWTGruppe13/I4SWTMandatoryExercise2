using System;

namespace I4SWTMandatoryExercise2
{
    public interface IFlightData
    {
        void SetFlightData(int x, int y, int z, DateTime time);
        string ID { get; }
        int xCoordinate { get; }
        int yCoordinate { get; }
        int zCoordinate { get; }
        DateTime timestamp { get; }
        double CompassCourse { get; set; }
        double Velocity { get; set; }
    }

    public class FlightData : IFlightData
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
        public double Velocity { get; set; }
    }
}