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
        void DetectAirplaneInAirspace(object sender, PlaneDecodedEventArgs e);
        void OnAirplaneDetected(PlaneDetectorEventArgs args);
    }
    
    public class AirSpacePlaneDetector : IAirSpacePlaneDetector
    {
        public event EventHandler<PlaneDetectorEventArgs> AirplaneDetected;
        Airspace airspace = new Airspace(50000, 50000, 80000);

        public AirSpacePlaneDetector(IDecoder dec)
        {
            dec.PlaneDecodedEvent += DetectAirplaneInAirspace;
        }

        public void DetectAirplaneInAirspace(object sender, PlaneDecodedEventArgs e)
        {
            List<FlightData> planesInAirspace = new List<FlightData>();
            foreach (var plane in e.Planes)
            {
                if (Math.Abs(plane.xCoordinate - airspace.Center._x) < 40000 && Math.Abs(plane.yCoordinate - airspace.Center._y) < 40000 &&
                    plane.zCoordinate > airspace._minHeight && plane.zCoordinate < airspace._maxHeight)
                {
                    planesInAirspace.Add(plane);
                }
            }
            OnAirplaneDetected(new PlaneDetectorEventArgs { PlanesInAirspace = planesInAirspace});
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
