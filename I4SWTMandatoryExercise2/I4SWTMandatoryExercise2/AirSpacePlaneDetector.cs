using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace I4SWTMandatoryExercise2
{
    public class PlaneDetectorEventArgs : EventArgs
    {
        public PlaneDetectorEventArgs(Point a)
        {
            A = a;
        }

        public Point A;
    }

    class AirSpacePlaneDetector
    {
        public event EventHandler<PlaneDetectorEventArgs> AirplaneDetected;

        public AirSpacePlaneDetector(IDecoder de)
        {

        }
        public void DetectAirplaneInAirspace(Point a, Airspace airspace)
        {
            if (a._x > airspace.Center._x + 40000 || a._y > airspace.Center._y + 40000 ||
                a._x < airspace.Center._x - 40000 || a._y < airspace.Center._y - 40000)
            {
                return;
            }
            OnAirplaneDetected(a);
        }

        protected virtual void OnAirplaneDetected(Point a)
        {
            AirplaneDetected?.Invoke(this, new PlaneDetectorEventArgs(a));
        }
    }
}
