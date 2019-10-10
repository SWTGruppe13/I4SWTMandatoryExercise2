using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace I4SWTMandatoryExercise2
{
    class AirSpacePlaneDetector
    {
        public bool DetectAirplaneInAirspace(Point a, Airspace airspace)
        {
            if (a._x > airspace.Center._x + 40000 || a._y > airspace.Center._y + 40000 || a._x < airspace.Center._x - 40000 || a._y < airspace.Center._y - 40000)
            {
                return false;
            }

            return true; // evebt skal smides
        }
    }
}
