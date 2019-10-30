using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using I4SWTMandatoryExercise2;
using NSubstitute;
using NUnit.Framework;

namespace ATM.test.unit
{
    [TestFixture]
    class CollisionDetectorTest
    {
        private CollisionDetector _uut;
        IAirSpacePlaneDetector _airSpacePlaneDetector = Substitute.For<IAirSpacePlaneDetector>();
        private PlaneDetectorEventArgs receivedArgs;
        private int NumberOfEvents;

        [SetUp]
        public void Setup()
        {
            _uut = new CollisionDetector(_airSpacePlaneDetector);
            receivedArgs = null;
            NumberOfEvents = 0;

            _uut. +=
                (s, a) =>
                {
                    receivedArgs = a;
                    NumberOfEvents++;
                };
        }
    }
}
