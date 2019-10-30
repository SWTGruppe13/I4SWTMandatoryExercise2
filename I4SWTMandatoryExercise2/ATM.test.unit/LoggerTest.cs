using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using I4SWTMandatoryExercise2;
using NUnit.Framework;

namespace ATM.test.unit
{
    class LoggerTest
    {
        public FlightData plane1;
        public FlightData plane2;
        private Logger uut = new Logger();

        [SetUp]
        public void Setup()
        {
            plane1 = new FlightData("AB01234");
            plane2 = new FlightData("YZ98765");
        }

        [Test]
        public void Logfile_Exists()
        {
            uut.Log(plane1, plane2);
            Assert.IsTrue(File.Exists("log.txt"));
            // ALTERNATIVE
            // FileAssert.Exists("log.txt");
        }

        [Test]
        public void Text_Logged_Is_Correct()
        {
            uut.Log(plane1, plane2);
            StringAssert.Contains(string.Format("Alarm triggered at {0}. Involved planes: {1} and {2}",
                DateTime.Now.ToString(new CultureInfo("en-GB")), plane1.ID, plane2.ID),
                File.ReadAllText("log.txt"));
        }
    }
}
