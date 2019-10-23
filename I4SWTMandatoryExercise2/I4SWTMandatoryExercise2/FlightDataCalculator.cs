using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace I4SWTMandatoryExercise2
{
    public class FlightDataCalculator
    {
        public double HorizontalDistance(Point a, Point b)
        {
            var x1 = a._x;
            var x2 = b._x;
            var y1 = a._y;
            var y2 = b._y;

            return Math.Sqrt(Math.Pow((x2-x1),2)+Math.Pow((y2-y1),2));
        }
        public double VerticalDistance(Point a, Point b)
        {
            var x1 = a._x;
            var x2 = b._x;
            var z1 = a._z;
            var z2 = b._z;

            return Math.Sqrt(Math.Pow((x2-x1),2)+Math.Pow((z2-z1),2));
        }
    }
}
