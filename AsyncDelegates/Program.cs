using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncDelegates
{
  class Program
  {
    delegate int AsyncDelegate(int x, int y);

    static void Main(string[] args)
    {
      Console.WriteLine($"Potok nomer {Thread.CurrentThread.ManagedThreadId} na4al paboty");
      var asyncDelegate = new AsyncDelegate(Calculate);

      // var result = asyncDelegate.Invoke(10, 15);

      //async nomer 1
      //var asyncResult = asyncDelegate.BeginInvoke(10, 15, null, null);
      //while (!asyncResult.IsCompleted)
      //{
      //  Console.WriteLine("Жди");
      //  Thread.Sleep(800);
      //}
      //var result = asyncDelegate.EndInvoke(asyncResult);
      //Console.WriteLine("Rezylt = " + result);
      //async nomer 2
      asyncDelegate.BeginInvoke(10, 15, new AsyncCallback(CalculateCallback), null /*<--Тип Object*/);
      Console.ReadLine();
    }
    //async nomer 2
    private static void CalculateCallback(IAsyncResult asyncResult)
    {
      var date = asyncResult.AsyncState as string;
      var asyncDelegate = (asyncResult as AsyncResult).AsyncDelegate as AsyncDelegate;
      var result = asyncDelegate.EndInvoke(asyncResult);
      Console.WriteLine("Rezylt = " + result);
    }

    static int Calculate(int firstNumber, int secondNamber)
    {
      Console.WriteLine($"Potok nomer {Thread.CurrentThread.ManagedThreadId} na4al paboty");
      Thread.Sleep(5000);
      return firstNumber + secondNamber;
    }
  }
}
