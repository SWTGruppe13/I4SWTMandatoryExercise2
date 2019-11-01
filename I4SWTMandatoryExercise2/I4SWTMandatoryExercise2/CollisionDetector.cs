using System;

namespace I4SWTMandatoryExercise2
{

    public interface ICollisionDetector
    {
        void OnPlaneDetectorEvent(object sender, PlaneDetectorEventArgs e);
    }

    public class CollisionDetectedEventArgs : EventArgs
    {
        public CollisionDetectedEventArgs(FlightData x, FlightData y)
        {
            Plane1 = x;
            Plane2 = y;
        }
        public FlightData Plane1;
        public FlightData Plane2;
    }

    public class CollisionDetector : ICollisionDetector
    {
        private IAirSpacePlaneDetector _airSpacePlaneDetector;
        private Renderer renderer = new Renderer();
        public ILogger logger { get; set; } = new Logger();
        public CollisionDetector(IAirSpacePlaneDetector airSpacePlaneDetector)
        {
            this._airSpacePlaneDetector = airSpacePlaneDetector;
            this._airSpacePlaneDetector.AirplaneDetected += OnPlaneDetectorEvent;
        }

        public void OnPlaneDetectorEvent(object sender, PlaneDetectorEventArgs e)
        {
            for (int i = 0; i < e.PlanesInAirspace.Count; i++)
            {
                for (int j = i+1; j < e.PlanesInAirspace.Count; j++)
                {
                    if ((Calculator.HorizontalDistance(e.PlanesInAirspace[i], e.PlanesInAirspace[j]) < 300) &&
                        (Calculator.VerticalDistance(e.PlanesInAirspace[i], e.PlanesInAirspace[j]) < 5000))
                    {
                        renderer.DisplayAlarm();
                        logger.Log(e.PlanesInAirspace[i], e.PlanesInAirspace[j]);
                        return;
                    }
                }
            }
        }
    }
}