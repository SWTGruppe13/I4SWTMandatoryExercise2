using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace I4SWTMandatoryExercise2
{
    public interface IAirSpacePlaneDetector
    {
        event EventHandler<PlaneDetectorEventArgs> AirplaneDetected;
        void DetectAirplaneInAirspace(object sender, PlaneDecodedEventArgs e);
        void OnAirplaneDetected(PlaneDetectorEventArgs args);
    }
    
    public class AirSpacePlaneDetector : IAirSpacePlaneDetector
    {
        public event EventHandler<PlaneDetectorEventArgs> AirplaneDetected;
        readonly Airspace _airspace = new Airspace(50000, 50000, 80000);
        List<FlightData> planesInAirspace = new List<FlightData>();

        public AirSpacePlaneDetector(IDecoder dec)
        {
            dec.PlaneDecodedEvent += DetectAirplaneInAirspace;
        }

        public void DetectAirplaneInAirspace(object sender, PlaneDecodedEventArgs e)
        {
            foreach (var plane in e.Planes)
            {
                if ((Math.Abs(plane.xCoordinate - _airspace.Center._x) < (_airspace.SideLength / 2)) &&
                    (Math.Abs(plane.yCoordinate - _airspace.Center._y) < (_airspace.SideLength / 2)) &&
                    (plane.zCoordinate > _airspace.MinHeight) &&
                    (plane.zCoordinate < _airspace.MaxHeight))
                {
                    planesInAirspace.Add(plane);
                }
            }
            if(planesInAirspace.Count > 0) OnAirplaneDetected(new PlaneDetectorEventArgs { PlanesInAirspace = planesInAirspace});
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
