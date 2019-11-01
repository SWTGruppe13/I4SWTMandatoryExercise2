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
        // Sets the ID of the plane when it is constructed as it should not change after construction
        public FlightData(string id)
        {
            ID = id;
        }

        // Sets all FlightData apart from the ID
        public void SetFlightData(int x, int y, int z, DateTime time)
        {
            xCoordinate = x;
            yCoordinate = y;
            zCoordinate = z;
            timestamp = time;
        }

        public string ID { get; }
        public int xCoordinate { get; private set; }
        public int yCoordinate { get; private set; }
        public int zCoordinate { get; private set; }
        public DateTime timestamp { get; private set; }
        public double CompassCourse { get; set; }
        public double Velocity { get; set; }
    }
}