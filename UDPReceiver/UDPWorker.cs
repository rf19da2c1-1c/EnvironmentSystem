using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace UDPReceiver
{


    class UDPWorker
    {
        private const int PORT = 23233;

        public UDPWorker()
        {
        }

        public void Start()
        {
            UdpClient client = new UdpClient(PORT); // modtage pakker på 10110 port nummer

            byte[] buffer;

            while (true)
            {
                IPEndPoint remoteEP = new IPEndPoint(IPAddress.Any, 0);
                buffer = client.Receive(ref remoteEP);

                string str = Encoding.UTF8.GetString(buffer);
                Console.WriteLine("STR modtaget :\n" + str);

                //todo parsing
                string[] strs = str.Split(',');
                
                // temp
                string tempStr = strs[0].Trim();
                Console.WriteLine(tempStr);
                string[] temps = tempStr.Split('=');
                double temp = Convert.ToDouble(temps[1]);
                Console.WriteLine("Temperaturen er målt til " + temp);

                // pressure strs[1]
                string presStr = strs[1].Trim();
                Console.WriteLine(presStr);
                string[] press = presStr.Split('=');
                double pres = Convert.ToDouble(press[1]);
                Console.WriteLine("Pressure er målt til " + pres);


                // Humidity strs[2]
                string humStr = strs[2].Trim();
                Console.WriteLine(humStr);
                string[] hums = humStr.Split('=');
                double hum = Convert.ToDouble(hums[1]);
                Console.WriteLine("Humidity er målt til " + hum);
            }
        }
    }
}