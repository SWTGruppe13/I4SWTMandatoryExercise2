using System;
using System.Collections.Generic;
using System.Linq;
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
    }
}
