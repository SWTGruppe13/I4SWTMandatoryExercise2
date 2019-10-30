using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using I4SWTMandatoryExercise2;
using NSubstitute;
using NUnit.Framework;

namespace ATM.test.unit
{
    [TestFixture]
    class CollisionDetectorTest
    {
        private CollisionDetector _uut;
        private CollisionDetectedEventArgs receivedArgs;
        private int NumberOfEvents;
        private IAirSpacePlaneDetector _fakeAirspacePlaneDetector;

        [SetUp]
        public void Setup()
        {
            _fakeAirspacePlaneDetector = Substitute.For<IAirSpacePlaneDetector>();
            _uut = new CollisionDetector(_fakeAirspacePlaneDetector);
            receivedArgs = null;
            NumberOfEvents = 0;

            _uut.CollisionDetectedEvent +=
                (s, a) =>
                {
                    receivedArgs = a;
                    NumberOfEvents++;
                };
        }

        //[Test]
        //public void PlaneDetectorEvent_RaisedEvent()
        //{
        //    _fakeAirspacePlaneDetector.AirplaneDetected += Raise.EventWith(new object(), new PlaneDetectorEventArgs());
        //}

        [Test]
        public void CollisionDetectorEvent_RaisedEvent()
        {
            FlightData F1 = new FlightData("1");
            FlightData F2 = new FlightData("2");

            bool wasCalled = false;
            F1.SetFlightData(8000,8000,8000, new DateTime());
            F2.SetFlightData(8000,8000,8000, new DateTime());

            List<FlightData> TestPlanes = new List<FlightData>
            {
                F1,
                F2
            };

            _uut.CollisionDetectedEvent += (o, e) => wasCalled = true;
            _uut.OnPlaneDetectorEvent(new object(), new PlaneDetectorEventArgs(){PlanesInAirspace = TestPlanes});

            Assert.That(wasCalled, Is.True);
        }

        [TestCase(5000,5000,4701,5000,7000,6000)] // 299 horizontal x-distance between two planes
        [TestCase(5000,5000,4702,5000,7000,6000)] // 298 horizontal x-distance between two planes
        [TestCase(4701,5000,5000,5000,7000,6000)] // 299 horizontal y-distance between two planes
        [TestCase(4702,5000,5000,5000,7000,6000)] // 298 horizontal y-distance between two planes
        public void CollisionDetectorEvent_HorizontalCases_RaisedEvent(int X1,int X2,int Y1,int Y2,int Z1,int Z2)
        {
            FlightData F1 = new FlightData("1");
            FlightData F2 = new FlightData("2");

            bool wasCalled = false;
            F1.SetFlightData(X1,Y1,Z1, new DateTime());
            F2.SetFlightData(X2,Y2,Z2, new DateTime());

            List<FlightData> TestPlanes = new List<FlightData>
            {
                F1,
                F2
            };

            _uut.CollisionDetectedEvent += (o, e) => wasCalled = true;
            _uut.OnPlaneDetectorEvent(new object(), new PlaneDetectorEventArgs(){PlanesInAirspace = TestPlanes});

            Assert.That(wasCalled, Is.True);
        }

        [TestCase(5000,5000,4700,5000,7000,6000)] // 300 horizontal x-distance between two planes
        [TestCase(5000,5000,4699,5000,7000,6000)] // 301 horizontal x-distance between two planes
        [TestCase(4700,5000,5000,5000,7000,6000)] // 300 horizontal y-distance between two planes
        [TestCase(4699,5000,5000,5000,7000,6000)] // 301 horizontal y-distance between two planes
        public void CollisionDetectorEvent_HorizontalCases_NoEvent(int X1,int X2,int Y1,int Y2,int Z1,int Z2)
        {
            FlightData F1 = new FlightData("1");
            FlightData F2 = new FlightData("2");

            bool wasCalled = false;
            F1.SetFlightData(X1,Y1,Z1, new DateTime());
            F2.SetFlightData(X2,Y2,Z2, new DateTime());

            List<FlightData> TestPlanes = new List<FlightData>
            {
                F1,
                F2
            };

            _uut.CollisionDetectedEvent += (o, e) => wasCalled = true;
            _uut.OnPlaneDetectorEvent(new object(), new PlaneDetectorEventArgs(){PlanesInAirspace = TestPlanes});

            Assert.That(wasCalled, Is.False);
        }

        [TestCase(5000,5000,5000,5000,8000,3001)] // 4999 horizontal z-distance between two planes
        [TestCase(5000,5000,5000,5000,8000,3002)] // 4998 horizontal z-distance between two planes
        public void CollisionDetectorEvent_VerticalCases_RaisedEvent(int X1,int X2,int Y1,int Y2,int Z1,int Z2)
        {
            FlightData F1 = new FlightData("1");
            FlightData F2 = new FlightData("2");

            bool wasCalled = false;
            F1.SetFlightData(X1,Y1,Z1, new DateTime());
            F2.SetFlightData(X2,Y2,Z2, new DateTime());

            List<FlightData> TestPlanes = new List<FlightData>
            {
                F1,
                F2
            };

            _uut.CollisionDetectedEvent += (o, e) => wasCalled = true;
            _uut.OnPlaneDetectorEvent(new object(), new PlaneDetectorEventArgs(){PlanesInAirspace = TestPlanes});

            Assert.That(wasCalled, Is.True);
        }

        [TestCase(5000,5000,5000,5000,8000,3000)] // 5000 horizontal z-distance between two planes
        [TestCase(5000,5000,5000,5000,8000,2999)] // 5001 horizontal z-distance between two planes
        public void CollisionDetectorEvent_VerticalCases_NoEvent(int X1,int X2,int Y1,int Y2,int Z1,int Z2)
        {
            FlightData F1 = new FlightData("1");
            FlightData F2 = new FlightData("2");

            bool wasCalled = false;
            F1.SetFlightData(X1,Y1,Z1, new DateTime());
            F2.SetFlightData(X2,Y2,Z2, new DateTime());

            List<FlightData> TestPlanes = new List<FlightData>
            {
                F1,
                F2
            };

            _uut.CollisionDetectedEvent += (o, e) => wasCalled = true;
            _uut.OnPlaneDetectorEvent(new object(), new PlaneDetectorEventArgs(){PlanesInAirspace = TestPlanes});

            Assert.That(wasCalled, Is.False);
        }
    }
}
