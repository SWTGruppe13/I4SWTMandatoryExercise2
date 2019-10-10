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
        void CreateAirspace(double centerX, double centerY);
        double NWCornerX { get; set; }
        double NWCornerY { get; set; }

        double NECornerX { get; set; }
        double NECornerY { get; set; }

        double SWCornerX { get; set; }
        double SWCornerY { get; set; }

        double SECornerX { get; set; }
        double SECornerY { get; set; }
    }

    public class Airspace : IAirspace
    {
        Airspace(double centerX, double centerY)
        {
            CreateAirspace(centerX,centerY);
        }
        public void CreateAirspace(double centerX, double centerY)
        {
            NWCornerX = centerX - 40000;
            NWCornerY = centerY + 40000;

            NECornerX = centerX + 40000;
            NECornerY = centerX + 40000;

            SWCornerX = centerX - 40000;
            SWCornerY = centerY - 40000;

            SECornerX = centerX + 40000;
            SECornerY = centerY - 40000;

        }

        public double NWCornerX { get; set; }
        public double NWCornerY { get; set; }
        public double NECornerX { get; set; }
        public double NECornerY { get; set; }
        public double SWCornerX { get; set; }
        public double SWCornerY { get; set; }
        public double SECornerX { get; set; }
        public double SECornerY { get; set; }


        private readonly double _minHeight = 500;
        private readonly double _maxHeight = 500;
    }
}

