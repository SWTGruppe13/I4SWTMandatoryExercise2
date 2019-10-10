using System;
using System.Globalization;
using System.IO;

namespace I4SWTMandatoryExercise2
{
    public interface ILogger
    {
        void Log(FlightData a, FlightData b);
    }

    public class Logger : ILogger
    {
        public void Log(FlightData a, FlightData b)
        {
            StreamWriter sw = new StreamWriter("log.txt");
            sw.WriteLine("Alarm triggered at {0}. Involved planes: {0} and {0}", DateTime.Now.ToString(new CultureInfo("en-GB")), a.ID, b.ID);
        }
    }
}