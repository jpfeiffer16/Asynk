using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

using Asynk;


namespace app
{
    public class Program
    {
        static void Main(string[] args)
        {
            var utils = new Utils();
            utils.DoStuff()
                .Then(() =>
                {
                    Console.WriteLine("Done doing stuff");
                });
            
            //Keep the main ui thread awake:
            while (true)
            {
                Thread.Sleep(100);
            }
        }
    }

    public class Utils
    {
        public Promise DoStuff()
        {
            return new Promise(() =>
            {
                for (var i = 0; i < 10; i++)
                {
                    Console.WriteLine(i.ToString());
                    Thread.Sleep(1000);
                }
            });
        }
    }
}
