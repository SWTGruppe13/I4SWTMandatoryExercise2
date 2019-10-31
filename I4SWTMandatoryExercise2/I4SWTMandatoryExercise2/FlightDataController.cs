using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace I4SWTMandatoryExercise2
{
    internal interface IFlightDataController
    {
        void ListController(object sender, PlaneDetectorEventArgs e);
    }

    public class FlightDataController : IFlightDataController
    {

        //private List<FlightData> flightDataListNew = new List<FlightData>();
        private List<FlightData> flightDataListOld = new List<FlightData>();
        private IAirSpacePlaneDetector airSpacePlaneDecOBJ;
        Renderer render = new Renderer(); // interface injection some time

        public FlightDataController(IAirSpacePlaneDetector airSpacePlaneDetector)
        {
            this.airSpacePlaneDecOBJ = airSpacePlaneDetector;
            this.airSpacePlaneDecOBJ.AirplaneDetected += ListController;
        }

        //public void ListOrganizer(object sender, PlaneDetectorEventArgs e)
        //{
        //    if (flightDataListNew.Count == 0)
        //    {
        //        flightDataListNew = e.PlanesInAirspace;
        //        return;
        //    }
        //    else if(flightDataListOld.Count == 0)
        //    {
        //        flightDataListOld = e.PlanesInAirspace;
        //    }
        //    flightDataListOld = flightDataListNew;
        //    flightDataListNew = e.PlanesInAirspace;
        //    flightDataListNew = Calculator.CalculateCompassCourse(flightDataListNew,flightDataListOld);
        //    flightDataListNew = Calculator.CalculateVelocity(flightDataListNew, flightDataListOld);
        //    Console.WriteLine("list count send to display: " + flightDataListNew.Count);
        //    render.DisplayData(flightDataListNew);
        //}

        public void ListController(object sender, PlaneDetectorEventArgs e)
        {
            if (flightDataListOld.Count <= 0)
            {
                flightDataListOld = e.PlanesInAirspace;
                return;
            }

            List<FlightData> listToReturn = new List<FlightData>();
            listToReturn = Calculator.CalculateCompassCourse(flightDataListOld, e.PlanesInAirspace);
            listToReturn = Calculator.CalculateVelocity(flightDataListOld, e.PlanesInAirspace);

            render.DisplayData(listToReturn);

            flightDataListOld = e.PlanesInAirspace;
        }
    }
}