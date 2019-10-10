﻿using System;
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
                System.Console.WriteLine($"Transponderdata:\n " +
                                         $"ID:{fd.ID}\n " +
                                         $"x-Coordinate: {fd.xCoordinate}\n " +
                                         $"y-Coordinate: {fd.yCoordinate}\n " +
                                         $"z-Coordinate: {fd.zCoordinate}\n " +
                                         $"Timestamp: {fd.timestamp}");

            }
        }

        public FlightData StringToClass(string str)
        {
            string[] strList = str.Split(';');
            FlightData fd = new FlightData();
            fd.SetFlightData(strList[0],
            Int32.Parse(strList[1]),
            Int32.Parse(strList[2]),
            Int32.Parse(strList[3]),
            DateTime.ParseExact(strList[4], "yyyyMMddHHmmssfff", CultureInfo.InvariantCulture));

            return fd;
        }
    }
}