using System;
using System.Globalization;
using TransponderReceiver;

namespace I4SWTMandatoryExercise2
{
    public class Decoder
    {
        private ITransponderReceiver receiver;
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
                System.Console.WriteLine($"Transponderdata:\n ID:{fd.ID}\n x-Coordinate: {fd.xCoordinate}\n y-Coordinate: {fd.yCoordinate}\n z-Coordinate: {fd.zCoordinate}\n Timestamp: {fd.timestamp}");
            }
        }

        public FlightData StringToClass(string str)
        {
            string[] strList = str.Split(';');
            FlightData fd = new FlightData(strList[0],
                Int32.Parse(strList[1]),
                Int32.Parse(strList[2]),
                Int32.Parse(strList[3]),
                DateTime.ParseExact(strList[4], "yyyyMMddHHmmssfff", CultureInfo.InvariantCulture));
            return fd;
        }
    }

    public class FlightData
    {
        public FlightData(string id, int x, int y, int z, DateTime time)
        {
            ID = id;
            xCoordinate = x;
            yCoordinate = y;
            zCoordinate = z;
            timestamp = time;
        }

        // Evt. tilføj de resterende data til denne klasse

        public string ID {get; private set; }
        public int xCoordinate { get; private set; }
        public int yCoordinate { get; private set; }
        public int zCoordinate { get; private set; }
        public DateTime timestamp { get; private set; }
    }
}