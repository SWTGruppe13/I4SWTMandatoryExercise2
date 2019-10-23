using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace I4SWTMandatoryExercise2
{
    public class PlaneDetectorEventArgs : EventArgs
    {
        public Point A { get; set; }
    }

    interface IAirSpacePlaneDetector
    {
        event EventHandler<PlaneDetectorEventArgs> AirplaneDetected;
        void DetectAirplaneInAirspace(Point a, Airspace airspace);
        void OnAirplaneDetected(PlaneDetectorEventArgs args);
    }

    public class AirSpacePlaneDetector : IAirSpacePlaneDetector
    {
        public event EventHandler<PlaneDetectorEventArgs> AirplaneDetected;

        //AirSpacePlaneDetector(IDecoder de)
        //{

        //}
        public void DetectAirplaneInAirspace(Point a, Airspace airspace)
        {
            if (a._x > airspace.Center._x + 40000 || a._y > airspace.Center._y + 40000 ||
                a._x < airspace.Center._x - 40000 || a._y < airspace.Center._y - 40000)
            {
                return;
            }
            OnAirplaneDetected(new PlaneDetectorEventArgs{A = a});
        }

        public virtual void OnAirplaneDetected(PlaneDetectorEventArgs args)
        {
            AirplaneDetected?.Invoke(this, args);
        }
    }
}
