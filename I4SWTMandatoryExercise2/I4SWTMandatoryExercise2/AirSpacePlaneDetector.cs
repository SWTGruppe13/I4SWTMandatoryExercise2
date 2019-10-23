using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework.Internal;

namespace I4SWTMandatoryExercise2
{
    class AirSpacePlaneDetector
    {
        public AirSpacePlaneDetector(IDecoder dec)
        {
            dec.PlaneDecodedEvent += TestFunc;
        }
        public bool DetectAirplaneInAirspace(Point a, Airspace airspace)
        {
            if (a._x > airspace.Center._x + 40000 || a._y > airspace.Center._y + 40000 || a._x < airspace.Center._x - 40000 || a._y < airspace.Center._y - 40000)
            {
                return false;
            }

            return true; // event skal smides
        }

        public void TestFunc(object sender, PlaneDecodedEventArgs e)
        {
            Console.WriteLine("Event triggered from Decoder");
            Console.WriteLine($"Plane object {e.Planes}");
        }
    }
}
