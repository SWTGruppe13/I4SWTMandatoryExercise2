using System.Runtime.InteropServices.ComTypes;

namespace I4SWTMandatoryExercise2
{

    public interface ICollisionDetector
    {
        void HandleCollision(object sender, PlaneDetectorEventArgs e);
    }

    public class CollisionDetector : ICollisionDetector
    {

        private IAirSpacePlaneDetector _airSpacePlaneDetector;
        CollisionDetector(IAirSpacePlaneDetector airSpacePlaneDetector)
        {
            this._airSpacePlaneDetector = airSpacePlaneDetector;
            this._airSpacePlaneDetector.AirplaneDetected += HandleCollision;
        }

        public void HandleCollision(object sender, PlaneDetectorEventArgs e)
        {
            foreach (var fly in e.A) // mangler en list til implementation
            {
                if ()
            }
        }
    }
}