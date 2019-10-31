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

            List<FlightData> planeList = new List<FlightData>();
            foreach (var data in e.TransponderData)
            {
                FlightData fd = StringToClass(data);

                //System.Console.WriteLine($"Transponderdata:\n " +
                //                         $"ID:{fd.ID}\n " +
                //                         $"x-Coordinate: {fd.xCoordinate}\n " +
                //                         $"y-Coordinate: {fd.yCoordinate}\n " +
                //                         $"z-Coordinate: {fd.zCoordinate}\n " +
                //                         $"Timestamp: {fd.timestamp}");

                planeList.Add(fd);
            }
            OnPlaneDecodedEvent(new PlaneDecodedEventArgs { Planes = planeList });
        }

        public FlightData StringToClass(string str)
        {
            string[] strList = str.Split(';');
            FlightData fd = new FlightData(strList[0]);
            fd.SetFlightData(Int32.Parse(strList[1]),
            Int32.Parse(strList[2]),
            Int32.Parse(strList[3]),
            DateTime.ParseExact(strList[4], "yyyyMMddHHmmssfff", CultureInfo.InvariantCulture));

            return fd;
        }

        protected virtual void OnPlaneDecodedEvent(PlaneDecodedEventArgs e)
        {
            PlaneDecodedEvent?.Invoke(this, e);
        }
    }
    public class PlaneDecodedEventArgs : EventArgs
    {
        public List<FlightData> Planes = new List<FlightData>();
    }
}