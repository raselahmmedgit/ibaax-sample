using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab.scheduler
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Scheduler Start");

            BootStrapper.Run();

            Console.WriteLine("Scheduler End");
            Console.ReadLine();
        }
    }
}
