using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace UDPBroadcaster
{
    class UDPWorker
    {
        private const int PORT = 23233;
        private readonly Random rnd = new Random(DateTime.Now.Millisecond);
        private const int SEK = 1;


        public UDPWorker()
        {
        }

        public void Start()
        {
            UdpClient client = new UdpClient();

            byte[] buffer;


            while (true)
            {
                // opretter en måling 
                double temp = rnd.NextDouble() * 5 + 35; // laver tal mellem 35.0-39.9999
                double press = rnd.NextDouble() * 25 + 1000;
                double hum = rnd.NextDouble() * 40 + 50;

                String str = $"temp={temp}, press={press}, hum={hum}";


                // Sender
                IPEndPoint SenderTilEP = new IPEndPoint(IPAddress.Broadcast, PORT);
                byte[] outbuffer = Encoding.UTF8.GetBytes(str);
                client.Send(outbuffer, outbuffer.Length, SenderTilEP);

                Thread.Sleep(1000*SEK);
            }
        }
    }
}