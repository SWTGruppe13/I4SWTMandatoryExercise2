using System.Runtime.InteropServices.ComTypes;
using NUnit.Framework;

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
            //Jens løsning
            for (int i = 0; i < e.PlanesInAirspace.Count; i++)
            {
                for (int j = i + 1; j < e.PlanesInAirspace.Count; j++)
                {
                    e.PlanesInAirspace[i].
                }
            }

            //Løsning før pull
            for (int i = 0; i < ; i++)
            {
                foreach (var fly in e.PlanesInAirspace) // mangler en list til implementation
                {
                    if (fly >)
                }
            }
        }
    }
}