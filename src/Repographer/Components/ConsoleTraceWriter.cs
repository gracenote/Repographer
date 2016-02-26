using System;

namespace Repographer.Components
{
    public class ConsoleTraceWriter : ITraceWriter
    {
        public void Output(string message)
        {
            Console.WriteLine(message);
        }
    }
}
