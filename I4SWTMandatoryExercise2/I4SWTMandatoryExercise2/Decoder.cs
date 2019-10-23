using System;
using System.Collections.Generic;
using System.Globalization;
using TransponderReceiver;

namespace I4SWTMandatoryExercise2
{
    public class PlaneDecodedEventArgs : EventArgs
    {
        public Dictionary<string, FlightData> Planes = new Dictionary<string, FlightData>();
    }
    public interface IDecoder
    {
        event EventHandler<PlaneDecodedEventArgs> PlaneDecodedEvent;
        void Decode(object sender, RawTransponderDataEventArgs e);
    }
    public class Decoder : IDecoder
    {
        private ITransponderReceiver receiver;
        public event EventHandler<PlaneDecodedEventArgs> PlaneDecodedEvent;
        Dictionary<string, FlightData> planeList = new Dictionary<string, FlightData>();

        public Decoder(ITransponderReceiver receiver)
        {
            this.receiver = receiver;

            // Attach to the event of the real or the fake TDR
            this.receiver.TransponderDataReady += Decode;
        }

        public void Decode(object sender, RawTransponderDataEventArgs e)
        {
            foreach (var data in e.TransponderData)
            {
                FlightData fd = StringToClass(data);

                System.Console.WriteLine($"Transponderdata:\n " +
                                         $"ID:{fd.ID}\n " +
                                         $"x-Coordinate: {fd.xCoordinate}\n " +
                                         $"y-Coordinate: {fd.yCoordinate}\n " +
                                         $"z-Coordinate: {fd.zCoordinate}\n " +
                                         $"Timestamp: {fd.timestamp}");

                if (!planeList.ContainsKey(fd.ID))
                { planeList.Add(fd.ID, fd); }
                else
                {
                    planeList[fd.ID].xCoordinate = fd.xCoordinate;
                    planeList[fd.ID].yCoordinate = fd.yCoordinate;
                    planeList[fd.ID].zCoordinate = fd.zCoordinate;
                    planeList[fd.ID].timestamp = fd.timestamp;
                }
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
}