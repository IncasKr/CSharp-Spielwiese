using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RaceCondition1
{
    class Program
    {
        private static int? counter;
        private static bool t1End = false;
        private static bool t2End = false;
        private static char[] chars = { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y' };
        
        static void Main(string[] args)
        {
            /* WITH THREAD */
            Thread T1 = new Thread(PrintNumber);
            Thread T2 = new Thread(PrintLetter);
            /* Without synchronization */
            //T1.Start();
            //T2.Start();
            /* With synchronization methode 1 */
            T1.Start();
            //T1.Join();
            T2.Start();
            T2.Join();


            /* WITH TASK */
            /* Without synchronization */
            //Task.Factory.StartNew(PrintNumber);
            //Task.Factory.StartNew(PrintLetter);
            /* With synchronization methode 1 */
            //Task TK1 = Task.Factory.StartNew(PrintNumber);
            //Task TK2 = TK1.ContinueWith(antacedent => PrintLetter());
            //Task.WaitAll(new Task[] { TK1, TK2 });


            Console.WriteLine("Ending main thread");
            Console.ReadLine();
        }

        private static void Mainproc()
        {
            while (true)
            {

            }
        }

        static void PrintNumber()
        {
            for (counter = 0; counter < 10; counter++)
            {
                Console.Write(counter + "\t");
                //Thread.Sleep(13);
            }
            t1End = true;         
        }

        private static void PrintLetter()
        {
            
            for (int i = 0; i < 20; i++)
            {
                Console.Write(chars[(int)counter] + counter.ToString() + "\t");
                if (counter < 20)
                {
                    counter++;
                }
                else
                {
                    counter = null;
                }
                
                //Thread.Sleep(11);
            }
            t2End = true;
        }
    }
}
