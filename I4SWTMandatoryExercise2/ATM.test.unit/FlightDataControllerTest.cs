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
        private IRenderer _fakeRenderer;
        private List<FlightData> _testPlanes;


        [SetUp]
        public void Setup()
        {
            _fakeAirSpacePlaneDetector = Substitute.For<IAirSpacePlaneDetector>();
            _fakeRenderer = Substitute.For<IRenderer>();
            _uut = new FlightDataController(_fakeAirSpacePlaneDetector);
            _uut.render = _fakeRenderer;
            _testPlanes = new List<FlightData>();


            // ListController Called ONCE so that flightDataList.Count > 0;
            // Otherwise the Calculator and Renderer wont be called
            FlightData F1 = new FlightData("1");
            F1.SetFlightData(8000,8000,8000, new DateTime());
            _testPlanes.Add(F1);
            _uut.ListController(new object(), new PlaneDetectorEventArgs(){PlanesInAirspace = _testPlanes});
        }

        [Test]
        public void FlightDataController_ListController_Called_Once_Render_NotCalled()
        {
            _fakeRenderer.DidNotReceive().DisplayData(Arg.Any<List<FlightData>>());
        }

        [Test]
        public void PlaneDetectorEvent_FlightDataController_ListController_Called()
        {

            FlightData F2 = new FlightData("2");
            FlightData F3 = new FlightData("3");

            F2.SetFlightData(8000,8000,8000, new DateTime());
            F3.SetFlightData(8000,8000,8000, new DateTime());

            _testPlanes.Add(F2);
            _testPlanes.Add(F3);

            _uut.ListController(new object(), new PlaneDetectorEventArgs(){PlanesInAirspace = _testPlanes});

            _fakeRenderer.Received().DisplayData(Arg.Any<List<FlightData>>());
        }


    }
}
