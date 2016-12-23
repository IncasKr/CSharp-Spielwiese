using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MonitorSolution
{
    class Program
    {
        private static int counter = 0;
        private static char[] chars = { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T' };
        static object locker = new object();
        static void Main(string[] args)
        {
            new Thread(PrintLetter).Start();
            new Thread(PrintNumber).Start();

            Console.WriteLine("Ending main thread");
            Console.ReadLine();
        }
        
        static void PrintNumber()
        {
            Monitor.Enter(locker);
            try
            {
                for (counter = 0; counter < 20; counter++)
                {
                    Console.Write(counter + "\t");
                }
            }
            finally
            {
                Monitor.Exit(locker);
            }
        }

        private static void PrintLetter()
        {
            Monitor.Enter(locker);
            try
            {
                for (int i = 0; i < 20; i++)
                {
                    Console.Write(chars[i] + counter.ToString() + "\t");
                }
            }
            finally
            {
                Monitor.Exit(locker);
            }
        }
    }
}
