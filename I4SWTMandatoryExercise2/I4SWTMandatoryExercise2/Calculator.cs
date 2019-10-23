using System;
using System.Collections.Generic;
using System.Linq;

namespace I4SWTMandatoryExercise2
{
    public class Calculator
    {
        public static List<FlightData> CalculateCompassCourse(List<FlightData> PlainDataList1, List<FlightData> PlainDataList2)
        {
            var listOneAndTwo = PlainDataList1.Zip(PlainDataList2, (l1, l2) => new { List1 = l1, List2 = l2 });
            var listToReturn = new List<FlightData>();
            foreach (var flight in listOneAndTwo)
            {
                double width = flight.List2.xCoordinate - flight.List1.xCoordinate;
                double height = flight.List2.yCoordinate - flight.List1.yCoordinate;

                double atan = Math.Atan(height / width) / Math.PI * 180;
                if (width < 0 || height < 0)
                    atan += 180;
                if (width > 0 && height < 0)
                    atan -= 180;
                if (atan < 0)
                    atan += 360;

                double degrees = atan % 360;

                flight.List1.CompassCourse = degrees;
                listToReturn.Add(flight.List1);
            }
            return listToReturn;
        }

        public static List<FlightData> CalculateVelocity(List<FlightData> PlainDataList1, List<FlightData> PlainDataList2)
        {
            var listOneAndTwo = PlainDataList1.Zip(PlainDataList2, (l1, l2) => new { List1 = l1, List2 = l2 });
            var listToReturn = new List<FlightData>();
            foreach (var flight in listOneAndTwo)
            {
                var diffInSecs = (flight.List2.timestamp - flight.List1.timestamp).TotalSeconds;
                flight.List1.Velocity = HorizontalDistance(flight.List2, flight.List1) / diffInSecs;
                listToReturn.Add(flight.List1);
            }
            return listToReturn;
        }

        public static double HorizontalDistance(FlightData fd1, FlightData fd2)
        {
            return Math.Sqrt(Math.Pow((fd2.xCoordinate - fd1.xCoordinate), 2) + Math.Pow((fd2.yCoordinate - fd1.yCoordinate), 2));
        }
        public static double VerticalDistance(FlightData fd1, FlightData fd2)
        {
            return Math.Sqrt(Math.Pow((fd2.xCoordinate - fd1.xCoordinate), 2) + Math.Pow((fd2.zCoordinate - fd1.zCoordinate), 2));
        }
    }
}