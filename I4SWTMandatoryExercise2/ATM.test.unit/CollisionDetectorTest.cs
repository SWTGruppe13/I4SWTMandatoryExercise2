﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using I4SWTMandatoryExercise2;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using NSubstitute.ReceivedExtensions;
using NUnit.Framework;

namespace ATM.test.unit
{
    [TestFixture]
    class CollisionDetectorTest
    {
        private CollisionDetector _uut;
        private IAirSpacePlaneDetector _fakeAirspacePlaneDetector;
        private List<FlightData> _testPlanes;
        private ILogger _fakeLogger;

        [SetUp]
        public void Setup()
        {
            _testPlanes = new List<FlightData>();
            _fakeAirspacePlaneDetector = Substitute.For<IAirSpacePlaneDetector>();
            _fakeLogger = Substitute.For<ILogger>();
            _uut = new CollisionDetector(_fakeAirspacePlaneDetector);
            _uut.logger = _fakeLogger;
        }

        [Test]
        public void CollisionDetected_LoggerCalled()
        {
            FlightData F1 = new FlightData("1");
            FlightData F2 = new FlightData("2");

            F1.SetFlightData(8000,8000,8000, new DateTime());
            F2.SetFlightData(8000,8000,8000, new DateTime());

            _testPlanes.Add(F1);
            _testPlanes.Add(F2);

            _uut.OnPlaneDetectorEvent(new object(), new PlaneDetectorEventArgs(){PlanesInAirspace = _testPlanes});
            
            _fakeLogger.Received().Log(Arg.Is<FlightData>(F1),Arg.Is<FlightData>(F2));
        }

        [TestCase(5000, 5000, 4701, 5000, 7000, 6000)] // 299 horizontal x-distance between two planes
        [TestCase(5000, 5000, 4702, 5000, 7000, 6000)] // 298 horizontal x-distance between two planes
        [TestCase(4701, 5000, 5000, 5000, 7000, 6000)] // 299 horizontal y-distance between two planes
        [TestCase(4702, 5000, 5000, 5000, 7000, 6000)] // 298 horizontal y-distance between two planes
        public void PlaneDetectorEvent_HorizontalCases_LoggerCalled(int X1, int X2, int Y1, int Y2, int Z1, int Z2)
        {
            FlightData F1 = new FlightData("1");
            FlightData F2 = new FlightData("2");

            F1.SetFlightData(X1, Y1, Z1, new DateTime());
            F2.SetFlightData(X2, Y2, Z2, new DateTime());

            _testPlanes.Add(F1);
            _testPlanes.Add(F2);

            _fakeAirspacePlaneDetector.AirplaneDetected += Raise.EventWith<PlaneDetectorEventArgs>(
                this,
                new PlaneDetectorEventArgs() { PlanesInAirspace = _testPlanes });

            _fakeLogger.Received().Log(Arg.Is<FlightData>(F1),Arg.Is<FlightData>(F2));
        }

        [TestCase(5000, 5000, 4700, 5000, 7000, 6000)] // 300 horizontal x-distance between two planes
        [TestCase(5000, 5000, 4699, 5000, 7000, 6000)] // 301 horizontal x-distance between two planes
        [TestCase(4700, 5000, 5000, 5000, 7000, 6000)] // 300 horizontal y-distance between two planes
        [TestCase(4699, 5000, 5000, 5000, 7000, 6000)] // 301 horizontal y-distance between two planes
        public void PlaneDetectorEvent_HorizontalCases_LoggerNotCalled(int X1, int X2, int Y1, int Y2, int Z1, int Z2)
        {
            FlightData F1 = new FlightData("1");
            FlightData F2 = new FlightData("2");

            F1.SetFlightData(X1, Y1, Z1, new DateTime());
            F2.SetFlightData(X2, Y2, Z2, new DateTime());

            _testPlanes.Add(F1);
            _testPlanes.Add(F2);

            _fakeAirspacePlaneDetector.AirplaneDetected += Raise.EventWith<PlaneDetectorEventArgs>(
                this,
                new PlaneDetectorEventArgs() { PlanesInAirspace = _testPlanes });

            _fakeLogger.DidNotReceive().Log(Arg.Any<FlightData>(),Arg.Any<FlightData>());
        }

        [TestCase(5000, 5000, 5000, 5000, 8000, 3001)] // 4999 vertical z-distance between two planes
        [TestCase(5000, 5000, 5000, 5000, 8000, 3002)] // 4998 vertical z-distance between two planes
        public void CollisionDetected_VerticalCases_LoggerCalled(int X1, int X2, int Y1, int Y2, int Z1, int Z2)
        {
            FlightData F1 = new FlightData("1");
            FlightData F2 = new FlightData("2");

            bool wasCalled = false;
            F1.SetFlightData(X1, Y1, Z1, new DateTime());
            F2.SetFlightData(X2, Y2, Z2, new DateTime());

            _testPlanes.Add(F1);
            _testPlanes.Add(F2);

            _fakeAirspacePlaneDetector.AirplaneDetected += Raise.EventWith<PlaneDetectorEventArgs>(
                this,
                new PlaneDetectorEventArgs() { PlanesInAirspace = _testPlanes });

            _fakeLogger.Received().Log(Arg.Is<FlightData>(F1),Arg.Is<FlightData>(F2));
        }

        [TestCase(5000, 5000, 5000, 5000, 8000, 3000)] // 5000 vertical z-distance between two planes
        [TestCase(5000, 5000, 5000, 5000, 8000, 2999)] // 5001 vertical z-distance between two planes
        public void CollisionNotDetected_VerticalCases_LoggerNotCalled(int X1, int X2, int Y1, int Y2, int Z1, int Z2)
        {
            FlightData F1 = new FlightData("1");
            FlightData F2 = new FlightData("2");

            bool wasCalled = false;
            F1.SetFlightData(X1, Y1, Z1, new DateTime());
            F2.SetFlightData(X2, Y2, Z2, new DateTime());

            _testPlanes.Add(F1);
            _testPlanes.Add(F2);

            _fakeAirspacePlaneDetector.AirplaneDetected += Raise.EventWith<PlaneDetectorEventArgs>(
                this,
                new PlaneDetectorEventArgs() { PlanesInAirspace = _testPlanes });

            _fakeLogger.DidNotReceive().Log(Arg.Any<FlightData>(),Arg.Any<FlightData>());
        }
    }
}
