using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using NUnit.Framework.Internal;
using I4SWTMandatoryExercise2;
using NSubstitute;

namespace ATM.test.unit
{
    [TestFixture]
    class FlightDataControllerTest
    {
        private FlightDataController _uut;
        private IAirSpacePlaneDetector _fakeAirSpacePlaneDetector;
        private List<FlightData> _testPlanes;

        [SetUp]
        public void Setup()
        {
            _testPlanes = new List<FlightData>();
            FlightData F1 = new FlightData("1");
            FlightData F2 = new FlightData("2");
            F1.SetFlightData(50,50,500,new DateTime());
            F2.SetFlightData(50,50,500,new DateTime());
            _testPlanes.Add(F1);
            _testPlanes.Add(F2);

            _fakeAirSpacePlaneDetector = Substitute.For<IAirSpacePlaneDetector>();

        }

        [Test] // Test til event
        public void PlaneDetectorEvent_FlightdataController_RaisedEvent()
        {
            _fakeAirSpacePlaneDetector.AirplaneDetected += Raise.EventWith<PlaneDetectorEventArgs>(
                this,
                new PlaneDetectorEventArgs() { PlanesInAirspace = _testPlanes });

        }
    }
}
