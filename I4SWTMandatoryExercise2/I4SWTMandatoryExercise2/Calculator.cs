using System;
using System.Collections.Generic;
using System.Linq;

namespace I4SWTMandatoryExercise2
{
    public static class Calculator
    {
        // Function takes two lists of plane data, one old and one current, and a list of planes with the current coordinates as well as their courses
        public static List<FlightData> CalculateCompassCourse(List<FlightData> planeDataList1, List<FlightData> planeDataList2)
        {
            // Zips the two plane lists together for use in a foreach loop
            var listOneAndTwo = planeDataList1.Zip(planeDataList2, (l1, l2) => new { List1 = l1, List2 = l2 });
            var listToReturn = new List<FlightData>(); // Initializes the list containing the return values

            // Iterates through all of the planes in the parameter lists
            foreach (var flight in listOneAndTwo)
            {
                // Calculates the course in degrees, adds it to the flight object and adds the object to the list of return values
                double xDirection = flight.List2.xCoordinate - flight.List1.xCoordinate;
                double yDirection = flight.List2.yCoordinate - flight.List1.yCoordinate;

                double atan = 0;
                if (xDirection != 0)
                {
                    atan = Math.Atan(xDirection / yDirection) / Math.PI * 180;
                }
                else
                {
                    atan = Math.Atan(xDirection) / Math.PI * 180;
                }
                
                if (yDirection < 0) // 2nd quadrant
                    atan += 180;
                else if (xDirection < 0 && yDirection >= 0) // 4th quadrant
                    atan += 360;

                double degrees = atan;

                flight.List1.CompassCourse = degrees;
                listToReturn.Add(flight.List1);
            }
            return listToReturn;
        }

        public static List<FlightData> CalculateVelocity(List<FlightData> planeDataList1, List<FlightData> planeDataList2)
        {
            // Zips the two plane lists together for use in a foreach loop
            var listOneAndTwo = planeDataList1.Zip(planeDataList2, (l1, l2) => new { List1 = l1, List2 = l2 });
            var listToReturn = new List<FlightData>(); // Initializes the list containing the return values

            // Iterates through all of the planes in the parameter lists
            foreach (var flight in listOneAndTwo)
            {
                var diffInSecs = (flight.List2.timestamp - flight.List1.timestamp).TotalSeconds; // Calculates the time between the two data points of the flight

                flight.List1.Velocity = (HorizontalDistance(flight.List1, flight.List2)+VerticalDistance(flight.List1, flight.List2)) / diffInSecs; // Divides the horizontal distance between the two data points with the time spent between them

                listToReturn.Add(flight.List1);
            }
            return listToReturn;
        }

        // Returns the horizontal distance travelled between two plane objects
        public static double HorizontalDistance(FlightData fd1, FlightData fd2)
        {
            return Math.Sqrt(Math.Pow((fd2.xCoordinate - fd1.xCoordinate), 2) + Math.Pow((fd2.yCoordinate - fd1.yCoordinate), 2));
        }

        // Returns the vertical distance travelled between two plane objects
        public static double VerticalDistance(FlightData fd1, FlightData fd2)
        {
            return Math.Abs(fd2.zCoordinate-fd1.zCoordinate);
        }
    }
}