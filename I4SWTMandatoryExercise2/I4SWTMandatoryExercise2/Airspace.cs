namespace I4SWTMandatoryExercise2
{
    public interface IAirspace
    {
        void CreateAirspace(double centerX, double centerY);
        Point Center { get; set; }
    }
    public class Airspace : IAirspace
    {
        public Airspace(double centerX, double centerY)
        {
            CreateAirspace(centerX, centerY);
        }
        public void CreateAirspace(double centerX, double centerY)
        {
            Center = new Point(centerX,centerY);
        }
        public Point Center { get; set; }

        public readonly double MinHeight = 500;
        public readonly double MaxHeight = 20000;
        public readonly double SideLength = 80000;
    }
}