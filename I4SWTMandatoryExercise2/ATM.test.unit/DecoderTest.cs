using System;
using System.Collections.Generic;
using I4SWTMandatoryExercise2;
using NSubstitute;
using NUnit.Framework;
using TransponderReceiver;

namespace ATM.test.unit
{
    class DecoderTest
    {
        public IDecoder Uut;
        public ITransponderReceiver FakeTransponderReceiver;
        public PlaneDecodedEventArgs DecodeEventArgs; //Used to save event args from Decoder.decode function.

        [SetUp]
        public void Setup()
        {
            FakeTransponderReceiver = Substitute.For<ITransponderReceiver>();
            Uut = new I4SWTMandatoryExercise2.Decoder(FakeTransponderReceiver);

        }


        [Test]
        public void DataConvertedTest() //Converts string to FD class
        {
            var testFlightData = Uut.StringToClass("ATR423;39045;12932;14000;20151006213456789");
            Assert.Multiple(() =>                                   //check data in Flight Data was input correctly
            {
                StringAssert.AreEqualIgnoringCase("ATR423", testFlightData.ID);
                Assert.That(testFlightData.xCoordinate, Is.EqualTo(39045));
                Assert.That(testFlightData.yCoordinate, Is.EqualTo(12932));
                Assert.That(testFlightData.zCoordinate, Is.EqualTo(14000));
                Assert.That(testFlightData.timestamp, Is.EqualTo(new DateTime(2015, 10, 06, 21, 34, 56, 789))); 
            });
        }

        [Test]
        public void TransponderEventReceived()
        {
            var list = new List<string>();
            list.Add("ATR423;39045;12932;14000;20151006213456789");

            Uut.PlaneDecodedEvent += (o, e) => { DecodeEventArgs = e; };    //Subscribe with lambda so eventargs can be accessed

            FakeTransponderReceiver.TransponderDataReady += Raise.EventWith(new object(),new RawTransponderDataEventArgs(list));                      

            Assert.NotNull(DecodeEventArgs);            //Assert that event args was instantiated, and the function was called
        }

        [Test]
        public void OnPlaneDecodedEventWasRaised()
        {

            var wasCalled = false;

            Uut.PlaneDecodedEvent += (o, e) => wasCalled = true; //lambda expression to subscribe on the event

            //Call Decode with a empty List only tests if the event is raised as it then doesn't use the foreach loop.
            Uut.Decode(new object(),new RawTransponderDataEventArgs(Substitute.For<List<string>>())); 

            Assert.True(wasCalled);                       
        }
        [Test]
        public void OnPlaneDecodedEventWasNotRaised()
        {

            var wasCalled = false;

            Uut.PlaneDecodedEvent += (o, e) => wasCalled = true; //lambda expression to subscribe on the event

            Assert.False(wasCalled);
        }



    }

    
}
