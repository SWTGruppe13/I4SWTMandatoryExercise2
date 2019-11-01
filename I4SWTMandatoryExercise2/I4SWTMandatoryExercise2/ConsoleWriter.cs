using System;

namespace I4SWTMandatoryExercise2
{
    public interface IConsoleWriter
    {
        void WriteLine(string lineToWrite);
    }

    // Class acts as a wrapper around the Console.WriteLine function to add testability
    public class ConsoleWriter : IConsoleWriter
    {
        public void WriteLine(string lineToWrite)
        {
            Console.WriteLine(lineToWrite);
        }
    }
}