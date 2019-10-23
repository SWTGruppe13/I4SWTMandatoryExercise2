using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;



namespace I4SWTMandatoryExercise2
{
    public interface IAirspace
    {
        void CreateAirspace(double centerX, double centerY, double length);
        Point NWCorner { get; set; }
        Point NECorner { get; set; }
        Point SWCorner { get; set; }
        Point SECorner { get; set; }
        Point Center { get; set; }
    }
    public class Airspace : IAirspace
    {
        public Airspace(double centerX, double centerY, double length)
        {
            CreateAirspace(centerX, centerY, length);
        }
        public void CreateAirspace(double centerX, double centerY, double length)
        {
            Center = new Point(centerX,centerY);
            NWCorner = new Point(centerX- (length / 2), centerY+(length/2));
            NECorner = new Point(centerX + (length / 2), centerY + (length / 2));
            SWCorner = new Point(centerX - (length / 2), centerY - (length / 2));
            SECorner = new Point(centerX + (length / 2), centerY - (length / 2));
        }
        public Point NWCorner { get; set; }
        public Point NECorner { get; set; }
        public Point SWCorner { get; set; }
        public Point SECorner { get; set; }
        public Point Center { get; set; }

        public readonly double _minHeight = 500;
        public readonly double _maxHeight = 20000;
    }
}