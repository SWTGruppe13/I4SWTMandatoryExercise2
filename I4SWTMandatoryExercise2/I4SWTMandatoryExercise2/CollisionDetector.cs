using System;
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
        CollisionDetector(IAirSpacePlaneDetector airSpacePlaneDetector)
        {
            this._airSpacePlaneDetector = airSpacePlaneDetector;
            this._airSpacePlaneDetector.AirplaneDetected += HandleCollision;
        }

        public void HandleCollision(object sender, PlaneDetectorEventArgs e)
        {
            for (int i = 0; i < e.PlanesInAirspace.Count; i++)
            {
                for (int j = i+1; j < e.PlanesInAirspace.Count-1; j++)
                {
                    if ((HorizontalDistance(e.PlanesInAirspace[i], e.PlanesInAirspace[j]) < 300) &&
                        (VerticalDistance(e.PlanesInAirspace[i], e.PlanesInAirspace[j]) < 5000))
                        return; // ALARM event til logger
                }
            }
        }
        public double HorizontalDistance(FlightData fd1, FlightData fd2)
        {
            return Math.Sqrt(Math.Pow((fd2.xCoordinate-fd1.xCoordinate),2)+Math.Pow((fd2.yCoordinate-fd1.yCoordinate),2));
        }
        public double VerticalDistance(FlightData fd1, FlightData fd2)
        {
            return Math.Sqrt(Math.Pow((fd2.xCoordinate-fd1.xCoordinate),2)+Math.Pow((fd2.zCoordinate-fd1.zCoordinate),2));
        }
    }
}