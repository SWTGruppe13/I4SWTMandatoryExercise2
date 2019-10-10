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

            Console.WriteLine(fdtest.CalculateCompassCourse("test", 10, 10, 0, 0));
          
            
            // Using the real transponder data receiver
            var receiver = TransponderReceiverFactory.CreateTransponderDataReceiver();

            // Dependency injection with the real TDR
            var system = new Decoder(receiver);

            // Let the real TDR execute in the background
            while (true)
                Thread.Sleep(1000);
        }
    }
}
