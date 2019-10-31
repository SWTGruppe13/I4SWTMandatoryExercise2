﻿using System;
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
            NWCorner = new Point(centerX - (length / 2), centerY + (length / 2));
            NECorner = new Point(centerX + (length / 2), centerY + (length / 2));
            SWCorner = new Point(centerX - (length / 2), centerY - (length / 2));
            SECorner = new Point(centerX + (length / 2), centerY - (length / 2));
        }
        public Point Center { get; set; }

        public readonly double MinHeight = 500;
        public readonly double MaxHeight = 20000;
    }
}