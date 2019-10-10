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
            FlightData fdtest = new FlightData();

            Console.WriteLine(fdtest.CalculateCompassCourse(new Point(1,1), new Point(2,2)));
          
            
            // Using the real transponder data receiver
            var receiver = TransponderReceiverFactory.CreateTransponderDataReceiver();

            // Dependency injection with the real TDR
            var system = new Decoder(receiver);

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
        public double _x { get; private set; }
        public double _y { get; private set; }
        public double _z { get; private set; }
    }

    //Ignore this - Doesnt work
    public class Vector
    {
        public Vector(Point a, Point b)
        {
            _x = b._x - a._x;
            _y = b._y - a._y;
        }
        public double Direction()
        {
            return -(Math.Atan(_y / _x) * (180 / Math.PI) - 90);
        }
        public double _x { get; set; }
        public double _y { get; set; }
    }
}
