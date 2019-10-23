using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace I4SWTMandatoryExercise2
{
    class FlightDataController
    {

        private IAirSpacePlaneDetector airSpacePlaneDecOBJ;
        Renderer render = new Renderer(); // interface injection some time

        public FlightDataController(IAirSpacePlaneDetector airSpacePlaneDetector)
        {
            this.airSpacePlaneDecOBJ = airSpacePlaneDetector;
            this.airSpacePlaneDecOBJ.AirplaneDetected += ListOrganizer;
        }

        private List<FlightData> flightDataListOne = new List<FlightData>();
        private List<FlightData> flightDataListTwo = new List<FlightData>();


        public void ListOrganizer(object sender, PlaneDetectorEventArgs e)
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
                Calculator.CalculateCompassCourse(flightDataListOne,flightDataListTwo);
            }
        }
    }
}
