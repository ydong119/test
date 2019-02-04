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
    class SocketClient
    {
        //static Thread ThreadClient = null;
        //static Socket SocketClient = null;

        //  Socket SocketClient = null;
        Socket socketCl = null;
        public void Connect(int port)
        {
            try
            {
              //  int port = 6000;
                string host = "127.0.0.1";//服务器端ip地址

                IPAddress ip = IPAddress.Parse(host);
                IPEndPoint ipe = new IPEndPoint(ip, port);

                //定义一个套接字监听  
                socketCl = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

                try
                {
                    //客户端套接字连接到网络节点上，用的是Connect  
                    socketCl.Connect(ipe);
                }
                catch (Exception)
                {
                    Console.WriteLine("连接失败！\r\n");
                    Console.ReadLine();
                    return;
                }

                Thread thread = new Thread(Recv);
                thread.IsBackground = true;
                thread.Start(socketCl);

                //Thread.Sleep(1000);
                Console.WriteLine("请输入内容<按Enter键发送>：\r\n");
                while (true)
                {
                    string sendStr = Console.ReadLine();
                    ClientSendMsg(sendStr);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadLine();
            }
        }

        //接收服务端发来信息的方法    
        private static void Recv(Object socket)
        {
            Socket socketClient = socket as Socket;
            int x = 0;
            //持续监听服务端发来的消息 
            while (true)
            {
                try
                {
                    byte[] arrRecvmsg = new byte[1024 * 1024];
                    //将客户端套接字接收到的数据存入内存缓冲区，并获取长度  
                    int length = socketClient.Receive(arrRecvmsg);

                    //将套接字获取到的字符数组转换为人可以看懂的字符串  
                    string strRevMsg = Encoding.UTF8.GetString(arrRecvmsg, 0, length);
                    Console.WriteLine("\r\n服务器：" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:fff") + "\r\n" + strRevMsg + "\r\n");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("远程服务器已经中断连接！" + ex.Message + "\r\n");
                    break;
                }
            }
        }

        //发送字符信息到服务端的方法  
        public  void ClientSendMsg(string sendMsg)
        {
            //将输入的内容字符串转换为机器可以识别的字节数组     
            byte[] arrClientSendMsg = Encoding.UTF8.GetBytes(sendMsg);
            //调用客户端套接字发送字节数组     
            socketCl.Send(arrClientSendMsg);
        }    


    }
}
