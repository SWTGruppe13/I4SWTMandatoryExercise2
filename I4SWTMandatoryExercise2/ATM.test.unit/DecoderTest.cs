using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Castle.Core.Smtp;
using I4SWTMandatoryExercise2;
using NSubstitute;
using NUnit.Framework;
using TransponderReceiver;

namespace ATM.test.unit
{
    class DecoderTest
    {
        public IDecoder Uut { get; set; }
        public ITransponderReceiver FakeTransponderReceiver { get; set; }

        [SetUp]
        public void Setup()
        {
            FakeTransponderReceiver = Substitute.For<ITransponderReceiver>();
            Uut = new I4SWTMandatoryExercise2.Decoder(FakeTransponderReceiver);
        }

        [Test]
        public void TransponderEventReceived()
        {
            FakeTransponderReceiver.TransponderDataReady += Raise.EventWith(new object(),
                new RawTransponderDataEventArgs(Substitute.For<List<string>>()));
            // NO CLUE
            

        }

        [Test]
        public void OnPlaneDecodedEventWasRaised()
        {

            var wasCalled = false;

            Uut.PlaneDecodedEvent += (o, e) => wasCalled = true; //lambda expression to subscribe on the event

            Uut.Decode(new object(),new RawTransponderDataEventArgs(Substitute.For<List<string>>())); //Raise the event

            Assert.True(wasCalled);                       
        }
        [Test]
        public void OnPlaneDecodedEventWasNotRaised()
        {

            var wasCalled = false;

            Uut.PlaneDecodedEvent += (o, e) => wasCalled = true; //lambda expression to subscribe on the event

            Assert.False(wasCalled);
        }

        [Test]
        public void DataConvertedTest() //Converts string to FD class
        {
            var testFlightData = Uut.StringToClass("ATR423;39045;12932;14000;20151006213456789");
            Assert.Multiple(() =>
            {
                StringAssert.AreEqualIgnoringCase("ATR423", testFlightData.ID);
                Assert.That(testFlightData.xCoordinate, Is.EqualTo(39045));
                Assert.That(testFlightData.yCoordinate, Is.EqualTo(12932));
                Assert.That(testFlightData.zCoordinate, Is.EqualTo(14000));
                Assert.That(testFlightData.timestamp, Is.EqualTo(new DateTime(2015,10,06,21,34,56,789)));
            });
        }

    }

    
}
