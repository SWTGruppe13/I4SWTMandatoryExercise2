using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using I4SWTMandatoryExercise2;
using NSubstitute;

namespace ATM.test.unit
{
    class AirSpacePlaneDetectorTest
    {
        IDecoder _decoder = Substitute.For<IDecoder>();
        private AirSpacePlaneDetector uut;
        private PlaneDetectorEventArgs _recivedEventArgs;

        [SetUp]
        public void Setup()
        {
            _recivedEventArgs = null;
            uut = new AirSpacePlaneDetector(_decoder);
            
            uut.AirplaneDetected +=
                (o, args) =>
                {
                    _recivedEventArgs = args;
                };
        }

        [Test]
        public void testname()
        {
            FlightData fd = new FlightData("2");
            fd.SetFlightData(7000,7000,7000,DateTime.Now);
            List<FlightData> planesTestData = new List<FlightData>();
            planesTestData.Add(fd);
            _decoder.PlaneDecodedEvent += Raise.EventWith(new PlaneDecodedEventArgs{Planes = planesTestData});
            Assert.That(_recivedEventArgs, Is.Not.Null);
        }

    }
}
