using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace I4SWTMandatoryExercise2
{
    class FlightDataCalculator
    {

        private IAirSpacePlaneDetector airSpacePlaneDecOBJ;
        Renderer render = new Renderer(); // interface injection some time

        FlightDataCalculator(IAirSpacePlaneDetector airSpacePlaneDetector)
        {
            this.airSpacePlaneDecOBJ = airSpacePlaneDetector;
            this.airSpacePlaneDecOBJ.AirplaneDetected += test;
        }

        private List<FlightData> flightDataListOne = new List<FlightData>();
        private List<FlightData> flightDataListTwo = new List<FlightData>();


        public void test(object sender, PlaneDetectorEventArgs e)
        {
            if (flightDataListOne.Count == 0)
            {
                flightDataListOne = e.PlanesInAirspace;
            }
            else if(flightDataListTwo.Count == 0)
            {
                flightDataListTwo = e.PlanesInAirspace;
            }
            else
            {
                flightDataListTwo = flightDataListOne;
                flightDataListOne = e.PlanesInAirspace;
                CalculateCompassCourse(flightDataListOne,flightDataListTwo);
            }

            
        }


        public void CalculateCompassCourse(List<FlightData> PlainDataList1, List<FlightData> PlainDataList2)
        {
            var ListOneAndTwo = PlainDataList1.Zip(PlainDataList2, (l1, l2) => new { List1 = l1, List2 = l2 });

            foreach (var flight in ListOneAndTwo)
            {
                double width = flight.List2.xCoordinate - flight.List1.xCoordinate;
                double height = flight.List2.yCoordinate - flight.List1.yCoordinate;

                double atan = Math.Atan(height / width) / Math.PI * 180;
                if (width < 0 || height < 0)
                    atan += 180;
                if (width > 0 && height < 0)
                    atan -= 180;
                if (atan < 0)
                    atan += 360;

                double degrees = atan % 360;

                flight.List1.CompassCourse = degrees;
                render.Display(flight.List1);

            }
            

            

            

            
            

        }
    }
}
