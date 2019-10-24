using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace I4SWTMandatoryExercise2
{
    internal interface IFlightDataController
    {
        void ListOrganizer(object sender, PlaneDetectorEventArgs e);
    }

    class FlightDataController : IFlightDataController
    {

        private IAirSpacePlaneDetector airSpacePlaneDecOBJ;
        Renderer render = new Renderer(); // interface injection some time

        public FlightDataController(IAirSpacePlaneDetector airSpacePlaneDetector)
        {
            this.airSpacePlaneDecOBJ = airSpacePlaneDetector;
            this.airSpacePlaneDecOBJ.AirplaneDetected += ListOrganizer;
        }

        private List<FlightData> flightDataListNew = new List<FlightData>();
        private List<FlightData> flightDataListOld = new List<FlightData>();

        public void ListOrganizer(object sender, PlaneDetectorEventArgs e)
        {
            if (flightDataListNew.Count == 0)
            {
                flightDataListNew = e.PlanesInAirspace;
            }
            else if(flightDataListOld.Count == 0)
            {
                flightDataListOld = e.PlanesInAirspace;
            }
            else
            {
                flightDataListOld = flightDataListNew;
                flightDataListNew = e.PlanesInAirspace;
                flightDataListNew = Calculator.CalculateCompassCourse(flightDataListNew,flightDataListOld);
                flightDataListNew = Calculator.CalculateVelocity(flightDataListNew, flightDataListOld);
                Console.WriteLine("list count send to display: " + flightDataListNew.Count);
                render.Display(flightDataListNew);
            }
        }
    }
}
