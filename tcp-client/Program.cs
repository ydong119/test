using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace tcp_client
{
    class Program
    {


        static void Main(string[] args)
        {
            SocketClient socket = new SocketClient();
            socket.Connect(6000);

        }




    }
}
