using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.IO;

namespace CIS427Server
{
    class Program
    {
        static void Main(string[] args)
        {
            int recv;
            byte[] data = new byte[1024];

            IPEndPoint endpoint = new IPEndPoint(IPAddress.Any, 904); // what listen for connectons on port 904

            Socket newSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp); // defines socket as udp
            newSocket.Bind(endpoint);

            Console.WriteLine("Waiting for a client.....");

            IPEndPoint sender = new IPEndPoint(IPAddress.Any, 904);
            EndPoint tmpRemote = (EndPoint)sender;

            recv = newSocket.ReceiveFrom(data, ref tmpRemote);

            Console.WriteLine("Message received from {0}", tmpRemote.ToString());
            Console.WriteLine(Encoding.ASCII.GetString(data, 0, recv));

            string Message = "Welcome to my server";
            data = Encoding.ASCII.GetBytes(Message);

            if (newSocket.Connected)
            {
                newSocket.Send(data);
            }


            while (true)
            {
                if (!newSocket.Connected)
                {
                    Console.WriteLine("Client Disconnected.");
                    break;
                }

                data = new byte[1024];
                recv = newSocket.ReceiveFrom(data, ref tmpRemote);

                if (recv == 0)
                    break;
                Console.WriteLine(Encoding.ASCII.GetString(data, 0, recv));
            }

            newSocket.Close();
        }
    }
}
