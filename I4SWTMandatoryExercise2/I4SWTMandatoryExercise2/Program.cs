using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TransponderReceiver;

namespace I4SWTMandatoryExercise2
{
    class Program
    {
        static void Main(string[] args)
        {
            // Using the real transponder data receiver
            var receiver = TransponderReceiverFactory.CreateTransponderDataReceiver();

            // Dependency injection with the real TDR
            var system = new Decoder(receiver);
            var airspacedetector = new AirSpacePlaneDetector(system);
            var angleAndSpeedCalculator = new FlightDataController(airspacedetector);
            var collisionDetector = new CollisionDetector(airspacedetector);

            // Let the real TDR execute in the background
            while (true)
                Thread.Sleep(1000);
        } 
    }

    public class Point
    {
        public Point(double x, double y)
        {
            _x = x;
            _y = y;
        }
        public double _x { get; }
        public double _y { get; }
    }
}
