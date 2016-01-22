using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Net;
using System.IO;

using Asynk;


namespace app
{
    public class Program
    {
        static void Main(string[] args)
        {
            var promise = new Promise(() =>
            {
                //This is syncronous, blocking code
                var request = WebRequest.Create("https://spiderserver.herokuapp.com");
                var response = request.GetResponse().GetResponseStream();
                var reader = new StreamReader(response);
                var html = reader.ReadToEnd();
                Console.WriteLine(html);
            });

            promise.Then(() =>
            {
                Console.WriteLine("Promise has resolved");
            });

            //Down here we'll count up for a bit

            var countTo = 40;

            for (var i = 0; i < countTo; i++)
            {
                Console.WriteLine(i.ToString());
                Thread.Sleep(100);
            }
        }
    }
}
