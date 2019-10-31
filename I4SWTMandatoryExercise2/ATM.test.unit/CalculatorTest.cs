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
        [TestCase(0, 0, 0, 0, 0)]           // Zero Values
        [TestCase(0, 10, 0, 0, 90)]         // No change in y-direction
        [TestCase(0, 0, 0, 50, 0)]          // No change in x-direction
        [TestCase(0, 0, 0, 1, 0)]           // 0 degrees
        [TestCase(0, 1, 0, 100, 0.5)]       // 1st quadrant - Close to 0/360 degrees
        [TestCase(0, 100, 0, 1, 89.5)]      // 1st quadrant - Close to 90 degrees
        [TestCase(0, 1, 0, 0, 90)]          // 90 degrees
        [TestCase(0, 100, 0, -1, 90.5)]     // 2nd quadrant - Close to 90 degrees
        [TestCase(0, 1, 0, -100, 179.5)]    // 2nd quadrant - Close to 180 degrees
        [TestCase(0, 0, 0, -1, 180)]        // 180 degrees
        [TestCase(0, -1, 0, -100, 180.5)]   // 3rd quadrant - Close to 180 degrees
        [TestCase(0, -100, 0, -1, 269.5)]   // 3rd quadrant - Close to 270 degrees
        [TestCase(0, -1, 0, 0, 270)]        // 270 degrees
        [TestCase(0, -100, 0, 1, 270.5)]    // 4th quadrant - Close to 270 degrees
        [TestCase(0, -1, 0, 100, 359.5)]    // 4th quadrant - Close to 0/360 degrees
        public void Calculate_Course(int xCoordinate1, int xCoordinate2, int yCoordinate1, int yCoordinate2, double result)
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

            resultList = Calculator.CalculateCompassCourse(list1, list2);

            Assert.That(resultList[0].CompassCourse, Is.EqualTo(result).Within(0.4));
        }

        [Test]
        public void Calculate_Course_First_List_Longer()
        {
            FlightData fd1_1 = new FlightData("1_1");
            FlightData fd1_2 = new FlightData("1_2");
            FlightData fd2_1 = new FlightData("2_1");

            fd1_1.SetFlightData(0, 0, 0, new DateTime());
            fd1_2.SetFlightData(100, 100, 0, new DateTime());
            fd2_1.SetFlightData(20, 20, 0, new DateTime());

            List<FlightData> list1 = new List<FlightData>();
            List<FlightData> list2 = new List<FlightData>();

            list1.Add(fd1_1);
            list1.Add(fd1_2);
            list2.Add(fd2_1);

            List<FlightData> resultList = new List<FlightData>();

            resultList = Calculator.CalculateCompassCourse(list1, list2);

            Assert.That(resultList[0].CompassCourse, Is.EqualTo(45));
        }

        [Test]
        public void Calculate_Course_Second_List_Longer()
        {
            FlightData fd1_1 = new FlightData("1_1");
            FlightData fd2_1 = new FlightData("1_2");
            FlightData fd2_2 = new FlightData("2_1");

            fd1_1.SetFlightData(0, 0, 0, new DateTime());
            fd2_1.SetFlightData(20, 20, 0, new DateTime());
            fd2_2.SetFlightData(-20, -20, 0, new DateTime());

            List<FlightData> list1 = new List<FlightData>();
            List<FlightData> list2 = new List<FlightData>();

            list1.Add(fd1_1);
            list2.Add(fd2_1);
            list2.Add(fd2_2);

            List<FlightData> resultList = new List<FlightData>();

            resultList = Calculator.CalculateCompassCourse(list1, list2);

            Assert.That(resultList[0].CompassCourse, Is.EqualTo(45));
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
