using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

//////////////
//Syntax guide
//////////////
//var test = new Promise(() => 
//{
//    //Do some blocking operation here
//});
//test.Then(() => {
//    //Callback to operation here
//});
//Console.WriteLine("Meanwhile, this code will continue to execute.");
//////////////

namespace Asynk
{
    public class Promise
    {
        public bool Resolved { get; set; }
        public object Value { get; set; }

        private Action Callback { get; set; }
        private Action OnResolved { get; set; }
        private Promise PreviousPromise { get; set; }

        public Promise(Promise previousPromise)
        {
            PreviousPromise = previousPromise;
            Task.Run(() => 
            {
                while (PreviousPromise.Resolved == false)
                {
                    Thread.Sleep(10);
                }
                if (OnResolved != null)
                {
                    Resolve();
                }
            });
        }

        public Promise(Action callback)
        {
            Callback = callback;
            Task.Run(() =>
            {
                Callback();
                Resolve();
            });
        }

        public void Resolve()
        {
            Value = null;
            Resolved = true;
            if (OnResolved != null)
            {
                OnResolved();
            }
        }

        public Promise Then(Action callback) 
        {
            OnResolved = callback;
            return new Promise(this);
        }


        //This is not going to work right now. Have to update the Promise class to use a .Resolve() pattern istead of new Promise(previousPromise) pattern
        //public Promise When(Promise otherPromise, Action callback)
        //{
        //    var promise = new Promise(this);
        //    Task.Run(() =>
        //    {
        //        while (PreviousPromise.Resolved == false)
        //        {
        //            Thread.Sleep(100);
        //        }
        //    });
        //    return promise;
        //}
    }
}
