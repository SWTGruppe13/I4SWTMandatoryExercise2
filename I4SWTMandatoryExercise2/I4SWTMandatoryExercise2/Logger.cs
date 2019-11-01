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
            StreamWriter sw = File.AppendText("log.txt");
            sw.WriteLine("Alarm triggered at {0}. Involved planes: {1} and {2}", DateTime.Now.ToString(new CultureInfo("en-GB")), a.ID, b.ID);
            sw.Close();
        }
    }
}