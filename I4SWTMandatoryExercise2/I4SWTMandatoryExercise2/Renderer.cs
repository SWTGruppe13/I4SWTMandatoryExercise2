using System;
using System.Collections.Generic;

namespace I4SWTMandatoryExercise2
{
    public interface IRenderer
    {
        void Display(List<FlightData> flightDataList);
    }

    public class Renderer : IRenderer
    {
        public void Display(List<FlightData> flightDataList)
        {
            Console.WriteLine("list count in render: " + flightDataList.Count);
            foreach (var fd in flightDataList)
            {
                Console.WriteLine("Flight ID: {0}", fd.ID.Substring(0, 6));
                Console.WriteLine("x-coordinate: {0}", fd.xCoordinate);
                Console.WriteLine("y-coordinate: {0}", fd.yCoordinate);
                Console.WriteLine("Altitude: {0}", fd.zCoordinate);
                Console.WriteLine("Horizontal Velocity: {0}", fd.Velocity);
                Console.WriteLine("Course direction: {0}", fd.CompassCourse);
            }
        }
    }
}