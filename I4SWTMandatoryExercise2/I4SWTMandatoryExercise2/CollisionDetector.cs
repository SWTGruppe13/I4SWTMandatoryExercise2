using System.Runtime.InteropServices.ComTypes;

namespace I4SWTMandatoryExercise2
{

    public interface ICollisionDetector
    {
        void DetectCollision(IAirspacePlaneDetector a)
    }

    public class CollisionDetector : ICollisionDetector
    {
        
    }


}