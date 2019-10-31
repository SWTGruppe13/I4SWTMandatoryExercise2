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
        IDecoder fakeDecoder = Substitute.For<IDecoder>();
        private AirSpacePlaneDetector uut;
        private PlaneDetectorEventArgs _recivedEventArgs;

        List<FlightData> planesTestData = new List<FlightData>();
        FlightData fd1 = new FlightData("1");
        FlightData fd2 = new FlightData("2");

        [SetUp]
        public void Setup()
        {
            _recivedEventArgs = null;
            uut = new AirSpacePlaneDetector(fakeDecoder);
            
            uut.AirplaneDetected +=
                (o, args) =>
                {
                    _recivedEventArgs = args;
                };

            fd1.SetFlightData(100000000, 100000000, 0, new DateTime());
            planesTestData.Add(fd1);

            fd2.SetFlightData(50000, 50000, 5000, new DateTime());
            planesTestData.Add(fd2);
        }

        [Test]
        public void event_fired_on_planeDetector_from_decoder_might_not_be_needed()
        {
            fakeDecoder.PlaneDecodedEvent += Raise.EventWith(new PlaneDecodedEventArgs{Planes = planesTestData});
            Assert.That(_recivedEventArgs, Is.Not.Null);
        }

        [Test]
        public void event_fired_from_planeDetector()
        {
            bool wasCalled = false;

            uut.AirplaneDetected += (o, e) => wasCalled = true;
            uut.DetectAirplaneInAirspace(new object(),new PlaneDecodedEventArgs());

            Assert.That(wasCalled, Is.True);

        }


        [Test]
        public void Invalid_Coordinates_Not_Added_To_Airspace_1Valid_1Invalid_in_List_Expect1()
        {
            uut.DetectAirplaneInAirspace(new object(), new PlaneDecodedEventArgs{ Planes = planesTestData });
            //uut.OnAirplaneDetected(new PlaneDetectorEventArgs { PlanesInAirspace = planesTestData });
            Assert.That(_recivedEventArgs.PlanesInAirspace.Count, Is.EqualTo(1));
        }

    }
}
