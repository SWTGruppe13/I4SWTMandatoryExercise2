using System;
using System.Collections.Generic;

namespace I4SWTMandatoryExercise2
{
    public interface IAirSpacePlaneDetector
    {
        event EventHandler<PlaneDetectorEventArgs> AirplaneDetected;
        void DetectAirplaneInAirspace(object sender, PlaneDecodedEventArgs e);
    }
    
    public class AirSpacePlaneDetector : IAirSpacePlaneDetector
    {
        public event EventHandler<PlaneDetectorEventArgs> AirplaneDetected;
        readonly Airspace _airspace = new Airspace(50000, 50000);

        public AirSpacePlaneDetector(IDecoder dec)
        {
            dec.PlaneDecodedEvent += DetectAirplaneInAirspace; // Subscribes to the event from the decoder
        }

        // Checks whether the planes received from the decoder are in the defined airspace
        public void DetectAirplaneInAirspace(object sender, PlaneDecodedEventArgs e)
        {
            List<FlightData> planesInAirspace = new List<FlightData>(); // List in which to store the planes that are within the airspace

            foreach (var plane in e.Planes) // Iterates through the planes sent from the decoder
            {
                // Checks to see if the given plane is within the boundaries of the airspace, and if it is, adds it to the aforementioned list
                if ((Math.Abs(plane.xCoordinate - _airspace.Center._x) <= (_airspace.SideLength / 2)) &&
                    (Math.Abs(plane.yCoordinate - _airspace.Center._y) <= (_airspace.SideLength / 2)) &&
                    (plane.zCoordinate >= _airspace.MinHeight) &&
                    (plane.zCoordinate <= _airspace.MaxHeight))
                {
                    planesInAirspace.Add(plane);
                }
            }
            // Invokes an event if there are planes present in the airspace
            if(planesInAirspace.Count > 0) OnAirplaneDetected(new PlaneDetectorEventArgs { PlanesInAirspace = planesInAirspace});
        }

        private void OnAirplaneDetected(PlaneDetectorEventArgs args)
        {
            AirplaneDetected?.Invoke(this, args);
        }
    }

    public class PlaneDetectorEventArgs : EventArgs
    {
        public List<FlightData> PlanesInAirspace { get; set; }
    }
}
