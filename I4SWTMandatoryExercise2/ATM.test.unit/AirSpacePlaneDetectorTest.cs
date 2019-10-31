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
        IDecoder _fakeDecoder = Substitute.For<IDecoder>();
        private AirSpacePlaneDetector _uut;
        private PlaneDetectorEventArgs _receivedEventArgs;

        private List<FlightData> _planesTestData; 
        private FlightData fd1 = new FlightData("1");
        private FlightData fd2 = new FlightData("2");

        [SetUp]
        public void Setup()
        {
            _uut = new AirSpacePlaneDetector(_fakeDecoder);
            _planesTestData = new List<FlightData>();
            _receivedEventArgs = null;

            _uut.AirplaneDetected +=
                (o, args) =>
                {
                    _receivedEventArgs = args;
                };
        }

        [Test]
        public void event_fired_on_planeDetector_from_decoder()
        {
            fd1.SetFlightData(100000000, 100000000, 0, new DateTime());
            fd2.SetFlightData(50000, 50000, 5000, new DateTime());

            _planesTestData.Add(fd1);
            _planesTestData.Add(fd2);

            _fakeDecoder.PlaneDecodedEvent += Raise.EventWith(new PlaneDecodedEventArgs{Planes = _planesTestData});
            Assert.That(_receivedEventArgs, Is.Not.Null);
        }

        [TestCase(10000,10000,500)] // Lower boundaries in Airspace
        [TestCase(10001,10001,501)] // Lower boundaries + 1
        [TestCase(90000,90000,20000)] // Upper boundaries in Airspace
        [TestCase(89999,89999,19999)] // Upper boundaries -1
        public void valid_coordinate_for_plane_in_airspace_raisedEvent(int X1, int Y1, int Z1)
        {
            fd1.SetFlightData(X1,Y1,Z1, new DateTime());

            _planesTestData.Add(fd1);

            _fakeDecoder.PlaneDecodedEvent += Raise.EventWith(new PlaneDecodedEventArgs{Planes = _planesTestData});
            Assert.That(_receivedEventArgs, Is.Not.Null);
        }

        [TestCase(9999,9999,499)] // Lower boundaries in Airspace
        [TestCase(9998,9998,498)] // Lower boundaries - 1
        [TestCase(90001,90001,20001)] // Upper boundaries in Airspace
        [TestCase(90002,90002,20002)] // Upper boundaries + 1
        public void Invalid_coordinate_for_plane_in_airspace_NoEvent(int X1, int Y1, int Z1)
        {
            fd1.SetFlightData(X1,Y1,Z1, new DateTime());

            _planesTestData.Add(fd1);

            _fakeDecoder.PlaneDecodedEvent += Raise.EventWith(new PlaneDecodedEventArgs{Planes = _planesTestData});
            Assert.That(_receivedEventArgs, Is.Null);
        }

        [Test]
        public void Invalid_Coordinates_Not_Added_To_Airspace_1Valid_1Invalid_in_List_Expect1()
        {
            _uut.DetectAirplaneInAirspace(new object(), new PlaneDecodedEventArgs{ Planes = _planesTestData });
            //uut.OnAirplaneDetected(new PlaneDetectorEventArgs { PlanesInAirspace = planesTestData });
            Assert.That(_receivedEventArgs.PlanesInAirspace.Count, Is.EqualTo(1));
        }

    }
}
