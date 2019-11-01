using System;
using System.Collections.Generic;
using I4SWTMandatoryExercise2;
using NSubstitute;
using NSubstitute.ReceivedExtensions;
using NUnit.Framework;

namespace ATM.test.unit
{
    public class RendererTest
    {
        public IRenderer Uut { get; set; }
        public IConsoleWriter CwFake { get; set; }

        [SetUp]
        public void SetUp()
        {
            CwFake = Substitute.For<IConsoleWriter>();
            Uut = new Renderer() { Cw = CwFake};
        }

        [Test]
        public void WriteLine_Called_Amount_Of_Times()
        {
            List<FlightData> list = new List<FlightData>();

            FlightData fd1 = new FlightData("123ABC");
            FlightData fd2 = new FlightData("ABC123");

            fd1.SetFlightData(123, 456, 789, DateTime.Now);
            fd2.SetFlightData(123, 456, 789, DateTime.Now);

            list.Add(fd1);
            list.Add(fd2);

            Uut.DisplayData(list);
            CwFake.Received(4).WriteLine("");
        }

        [Test]
        public void WriteLine_Gets_Correct_String()
        {
            Uut.DisplayAlarm();
            CwFake.Received().WriteLine(Arg.Is<string>(str => str.Contains("ALARM!!!!!")));
        }
    }
}