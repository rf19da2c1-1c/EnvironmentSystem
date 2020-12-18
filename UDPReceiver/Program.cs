using System;

namespace UDPReceiver
{
    class Program
    {
        static void Main(string[] args)
        {
            UDPWorker worker = new UDPWorker();
            worker.Start();


            Console.ReadLine();
        }
    }
}
