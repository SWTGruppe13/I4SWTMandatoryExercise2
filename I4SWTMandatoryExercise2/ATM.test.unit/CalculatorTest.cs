using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using I4SWTMandatoryExercise2;
using NUnit.Framework;

namespace ATM.test.unit
{
    class CalculatorTest
    {
        [TestCase(0, 0, 0, 0, 0)]   // Zero Values
        public void Calculate_Course(int xCoordinate1, int xCoordinate2, int yCoordinate1, int yCoordinate2, int result)
        {
            FlightData fd1 = new FlightData("1");
            FlightData fd2 = new FlightData("2");

            fd1.SetFlightData(xCoordinate1, yCoordinate1, 0, new DateTime());
            fd2.SetFlightData(xCoordinate2, yCoordinate2, 0, new DateTime());

            List<FlightData> list1 = new List<FlightData>();
            List<FlightData> list2 = new List<FlightData>();

            list1.Add(fd1);
            list2.Add(fd2);

            List<FlightData> resultList = new List<FlightData>();

            Assert.That(resultList[0].CompassCourse, Is.EqualTo(result));
        }

        [Test]
        public void Calculate_Course_Uneven_List_Length()
        {

        }

        [TestCase(1, 5, 1, 5, 5.6)]     // Simple functionality
        [TestCase(0, 0, 0, 0, 0)]       // Zero values
        [TestCase(-10, -5, -10, -5, 7)] // Coordinates below zero
        public void HorizontalDistance(int xCoordinate1, int xCoordinate2, int yCoordinate1, int yCoordinate2, double result)
        {
            FlightData fd1 = new FlightData("1");
            FlightData fd2 = new FlightData("2");

            fd1.SetFlightData(xCoordinate1, yCoordinate1,0, new DateTime());
            fd2.SetFlightData(xCoordinate2, yCoordinate2, 0, new DateTime());

            Assert.That(Calculator.HorizontalDistance(fd1, fd2), Is.EqualTo(result).Within(0.1));
        }

        [TestCase(1, 5, 4)]     // Simple functionality
        [TestCase(0, 0, 0)]     // Zero values
        [TestCase(-10, -5, 5)]  // Coordinates below zero
        public void VerticalDistance(int zCoordinate1, int zCoordinate2, double result)
        {
            FlightData fd1 = new FlightData("1");
            FlightData fd2 = new FlightData("2");

            fd1.SetFlightData(0, 0, zCoordinate1, new DateTime());
            fd2.SetFlightData(0, 0, zCoordinate2, new DateTime());

            Assert.That(Calculator.VerticalDistance(fd1, fd2), Is.EqualTo(result).Within(0.1));
        }
    }
}
