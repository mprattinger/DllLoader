using System;

namespace DllLoader.Utils
{
    public class Reporter
    {
        private static object _lock = new object();

        public static Reporter Output { get; private set; }
        public static Reporter Error { get; private set; }

        static Reporter()
        {
            Init();
        }

        public static void Init()
        {
            lock (_lock)
            {
                Output = new Reporter();
                Error = new Reporter();
            }
        }

        public Reporter()
        {

        }

        public void WriteLine(string message)
        {
            lock (_lock)
            {
                Console.WriteLine(message);
            }
        }

        public void WriteLine()
        {
            lock (_lock)
            {
                Console.WriteLine();
            }
        }

        public void Write(string message)
        {
            lock (_lock)
            {
                Console.Write(message);
            }
        }
    }
}
