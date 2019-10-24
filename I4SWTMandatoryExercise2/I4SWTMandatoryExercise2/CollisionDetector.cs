using System;
using System.Data;
using System.Runtime.InteropServices.ComTypes;
using I4SWTMandatoryExercise2;
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
        public CollisionDetector(IAirSpacePlaneDetector airSpacePlaneDetector)
        {
            //this._airSpacePlaneDetector = airSpacePlaneDetector;
            //this._airSpacePlaneDetector.AirplaneDetected += HandleCollision;
        }

        public void HandleCollision(object sender, PlaneDetectorEventArgs e)
        {
            for (int i = 0; i < e.PlanesInAirspace.Count; i++)
            {
                for (int j = i+1; j < e.PlanesInAirspace.Count-1; j++)
                {
                    if ((Calculator.HorizontalDistance(e.PlanesInAirspace[i], e.PlanesInAirspace[j]) < 300) &&
                        (Calculator.VerticalDistance(e.PlanesInAirspace[i], e.PlanesInAirspace[j]) < 5000))
                    {
                        Console.WriteLine("ALARM");
                        return; // ALARM event til logger
                    }
                }
            }
        }
    }
}