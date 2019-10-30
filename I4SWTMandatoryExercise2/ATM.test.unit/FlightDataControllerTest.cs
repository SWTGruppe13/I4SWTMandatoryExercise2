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

        [SetUp]
        public void Setup()
        {
            _fakeAirSpacePlaneDetector = Substitute.For<IAirSpacePlaneDetector>();

        }

        //[Test] // Test til event
        //public void PlaneDetectorEvent_FlightdataController_RaisedEvent()
        //{
        //}
    }
}
