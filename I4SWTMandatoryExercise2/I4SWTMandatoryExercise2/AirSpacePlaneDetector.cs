using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace I4SWTMandatoryExercise2
{
    interface IAirSpacePlaneDetector
    {
        event EventHandler<PlaneDetectorEventArgs> AirplaneDetected;
        void DetectAirplaneInAirspace(Dictionary<string, FlightData> planes, Airspace airspace);
        void OnAirplaneDetected(PlaneDetectorEventArgs args);
    }
    
    public class AirSpacePlaneDetector : IAirSpacePlaneDetector
    {
        public event EventHandler<PlaneDetectorEventArgs> AirplaneDetected;

        public void DetectAirplaneInAirspace(Dictionary<string, FlightData> planes, Airspace airspace)
        {
            List<FlightData> planesInAirspace = new List<FlightData>();
            foreach (var plane in planes)
            {
                if (!(plane.Value.xCoordinate > airspace.Center._x + 40000 || plane.Value.yCoordinate > airspace.Center._y + 40000 ||
                    plane.Value.xCoordinate < airspace.Center._x - 40000 || plane.Value.yCoordinate < airspace.Center._y - 40000 ||
                    plane.Value.zCoordinate < airspace._minHeight || plane.Value.zCoordinate > airspace._maxHeight))
                {
                    planesInAirspace.Add(plane.Value);
                }
                
            }
            OnAirplaneDetected(new PlaneDetectorEventArgs { PlanesInAirspace = planesInAirspace});

            if (Math.Abs(a._x - airspace.Center._x) < 40000 && Math.Abs(a._y - airspace.Center._y) < 40000 &&
                a._z > airspace._minHeight && a._z < airspace._maxHeight)
            {

            }
            OnAirplaneDetected(a);
        }

        public virtual void OnAirplaneDetected(PlaneDetectorEventArgs args)
        {
            AirplaneDetected?.Invoke(this, args);
        }
    }
    public class PlaneDetectorEventArgs : EventArgs
    {
        public List<FlightData> PlanesInAirspace { get; set; }
    }
}
