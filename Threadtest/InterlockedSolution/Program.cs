using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace InterlockedSolution
{
    struct ThreadInfo
    {
        public string Name { get; private set; }
        public int Executed { get; private set; }

        public ThreadInfo(string name, int value = 0)
        {
            Name = name;
            Executed = value;
        }

        public void Increase()
        {
            Executed++;
        }
    }

    class Program
    {
        //0 for false, 1 for true.
        private static int usingResource = 0;

        private const int numThreadIterations = 3;
        private const int numThreads = 2;
        private static List<ThreadInfo> _executedList = new List<ThreadInfo>();

        static void Main(string[] args)
        {
            Thread myThread;
            Random rnd = new Random();

            for (int i = 0; i < numThreads; i++)
            {
                myThread = new Thread(new ThreadStart(MyThreadProc));
                myThread.Name = String.Format("Thread{0}", i + 1);
                _executedList.Add(new ThreadInfo(myThread.Name));

                //Wait a random amount of time before starting next thread.
                Thread.Sleep(rnd.Next(0, 1000));
                myThread.Start();
            }           

            Console.WriteLine("\n\nNumber of executions per thread");
            foreach (var thread in _executedList)
            {
                Console.WriteLine($"\t- {thread.Name} => {thread.Executed}");
            }

            Console.ReadLine();
        }

        private static void MyThreadProc()
        {
            for (int i = 0; i < numThreadIterations; i++)
            {
                UseResource(i + 1);

                //Wait 1 second before next attempt.
                Thread.Sleep(1000);
            }
        }

        //A simple method that denies reentrancy.
        static bool UseResource(int itt)
        {
            //0 indicates that the method is not in use.
            if (0 == Interlocked.Exchange(ref usingResource, 1))
            {
                Console.WriteLine("{0}. - {1} acquired the lock", itt, Thread.CurrentThread.Name);
                _executedList.Find(t => t.Name.Equals(Thread.CurrentThread.Name)).Increase();
                //Code to access a resource that is not thread safe would go here.

                //Simulate some work
                Thread.Sleep(500);

                Console.WriteLine("{0}. - {1} exiting lock", itt, Thread.CurrentThread.Name);

                //Release the lock
                Interlocked.Exchange(ref usingResource, 0);
                return true;
            }
            else
            {
                Console.WriteLine("   {0}. - {1} was denied the lock", itt, Thread.CurrentThread.Name);
                return false;
            }
        }

    }
}
