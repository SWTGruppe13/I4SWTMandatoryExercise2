using System;
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
        public IRenderer render { get; set; } = new Renderer();

        public FlightDataController(IAirSpacePlaneDetector airSpacePlaneDetector)
        {
            this.airSpacePlaneDecOBJ = airSpacePlaneDetector;
            this.airSpacePlaneDecOBJ.AirplaneDetected += ListController; // Subscribes to the vent from AirSpacePlaneDetector
        }

        public void ListController(object sender, PlaneDetectorEventArgs e)
        {
            // If no previous data is present it has no base from which to calculate the velocity and the course, so the function returns
            if (flightDataListOld.Count <= 0)
            {
                flightDataListOld = e.PlanesInAirspace;
                return;
            }

            List<FlightData> listToReturn = new List<FlightData>();
            // Calculates the course and velocity based on the new flight data that has been sent and the previously sent data
            listToReturn = Calculator.CalculateCompassCourse(flightDataListOld, e.PlanesInAirspace);
            try
            {
                listToReturn = Calculator.CalculateVelocity(flightDataListOld, e.PlanesInAirspace);

            }
            catch (ArgumentException exception)
            {
                Console.WriteLine(exception); 
                //There should be some error handling here
            }

            render.DisplayData(listToReturn); // Displays the data after it has been calculated

            flightDataListOld = e.PlanesInAirspace; // The new flight data has been calculated and displayed and now replaces the old flight data
        }
    }
}