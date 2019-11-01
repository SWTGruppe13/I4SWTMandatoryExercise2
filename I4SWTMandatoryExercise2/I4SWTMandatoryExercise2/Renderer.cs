using System.Collections.Generic;

namespace I4SWTMandatoryExercise2
{
    public interface IRenderer
    {
        void DisplayData(List<FlightData> flightDataList);
        void DisplayAlarm();
    }

    public class Renderer : IRenderer
    {
        public IConsoleWriter Cw { get; set; } = new ConsoleWriter(); // ConsoleWriter object allows printing to the console

        // Prints flight data to the console
        public void DisplayData(List<FlightData> flightDataList)
        {
            // Separator between flight lists
            Cw.WriteLine("");
            Cw.WriteLine("------------------------------------------");
            Cw.WriteLine("");

            foreach (var fd in flightDataList) // Iterates through the planes and prints the data of each one
            {
                Cw.WriteLine(string.Format($"Flight ID: {fd.ID}"));
                Cw.WriteLine(string.Format($"x-coordinate: {fd.xCoordinate}"));
                Cw.WriteLine(string.Format($"y-coordinate: {fd.yCoordinate}"));
                Cw.WriteLine(string.Format($"Altitude: {fd.zCoordinate}"));
                Cw.WriteLine(string.Format($"Horizontal Velocity: {fd.Velocity}"));
                Cw.WriteLine(string.Format($"Course direction: {fd.CompassCourse}"));
                Cw.WriteLine("");
            }
        }

        public void DisplayAlarm()
        {
            Cw.WriteLine("ALARM!!!!!");
        }
    }
}