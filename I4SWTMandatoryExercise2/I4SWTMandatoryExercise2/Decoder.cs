using System;
using System.Collections.Generic;
using System.Globalization;
using TransponderReceiver;

namespace I4SWTMandatoryExercise2
{
    public interface IDecoder
    {
        event EventHandler<PlaneDecodedEventArgs> PlaneDecodedEvent;
        void Decode(object sender, RawTransponderDataEventArgs e);
        FlightData StringToClass(string str);
    }

    public class Decoder : IDecoder
    {
        private ITransponderReceiver receiver;
        public event EventHandler<PlaneDecodedEventArgs> PlaneDecodedEvent;

        public Decoder(ITransponderReceiver receiver)
        {
            this.receiver = receiver;

            // Attach to the event of the real or the fake TDR
            this.receiver.TransponderDataReady += Decode;
        }

        public void Decode(object sender, RawTransponderDataEventArgs e)
        {
            List<FlightData> planeList = new List<FlightData>(); // List to add the planes to
            // Iterates through the list of strings received from the TransponderReceiver, decodes the string into FlightData objects and adds them to a list
            foreach (var data in e.TransponderData)
            {
                FlightData fd = StringToClass(data);
                planeList.Add(fd);
            }

            OnPlaneDecodedEvent(new PlaneDecodedEventArgs { Planes = planeList }); // Invokes the PlaneDecodedEvent event
        }

        // Turns a string in the TransponderReceiver format into an appropriate class
        public FlightData StringToClass(string str)
        {
            string[] strList = str.Split(';'); // Separates the string int an array based off of semicolons

            FlightData fd = new FlightData(strList[0]); // Creates a FlightData object and sets its ID
            // Sets the remaining data from the TransponderReceiver
            fd.SetFlightData(Int32.Parse(strList[1]),
            Int32.Parse(strList[2]),
            Int32.Parse(strList[3]),
            DateTime.ParseExact(strList[4], "yyyyMMddHHmmssfff", CultureInfo.InvariantCulture));

            return fd;
        }

        private void OnPlaneDecodedEvent(PlaneDecodedEventArgs e)
        {
            PlaneDecodedEvent?.Invoke(this, e);
        }
    }
    public class PlaneDecodedEventArgs : EventArgs
    {
        public List<FlightData> Planes = new List<FlightData>();
    }
}