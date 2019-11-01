using System.Collections.Generic;

namespace I4SWTMandatoryExercise2
{
    internal interface IFlightDataController
    {
        void ListController(object sender, PlaneDetectorEventArgs e);
    }

    public class FlightDataController : IFlightDataController
    {
        private List<FlightData> flightDataListOld = new List<FlightData>();
        private IAirSpacePlaneDetector airSpacePlaneDecOBJ;
        Renderer render = new Renderer(); // interface injection some time

        public FlightDataController(IAirSpacePlaneDetector airSpacePlaneDetector)
        {
            this.airSpacePlaneDecOBJ = airSpacePlaneDetector;
            this.airSpacePlaneDecOBJ.AirplaneDetected += ListController;
        }

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