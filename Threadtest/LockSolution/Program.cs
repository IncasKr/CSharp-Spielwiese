using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LockSolution
{
    class Program
    {
        private static int counter;
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
            lock (locker)
            {
                for (counter = 0; counter < 20; counter++)
                {
                    Console.Write(counter + "\t");
                }
            }
        }

        private static void PrintLetter()
        {
            lock (locker)
            {
                for (int i = 0; i < 20; i++)
                {
                    Console.Write(chars[i] + counter.ToString() + "\t");
                }
            }
        }
    }
}
