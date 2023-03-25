using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;
class Program
{
    static void Main(string[] args)
    {
        IPEndPoint iep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8888);
        Socket server = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
        server.Bind(iep);
        //tao ra mot Endpot tu xa de nhan du lieu ve
        IPEndPoint RemoteEp = new IPEndPoint(IPAddress.Any, 0);
        EndPoint remote = (EndPoint)RemoteEp;
        byte[] data = new byte[1024];
        int recv = server.ReceiveFrom(data, ref remote);
        string s = Encoding.ASCII.GetString(data, 0, recv);
        Console.WriteLine("Client:{0}", s);
        data = Encoding.ASCII.GetBytes("Chao client");
        server.SendTo(data, remote);
        while (true)
        {
            data = new byte[1024];
            recv = server.ReceiveFrom(data, ref remote);
            s = Encoding.ASCII.GetString(data, 0, recv);
            if (s.ToUpper().Equals("QUIT")) break;
            Console.WriteLine(s); data = new byte[1024];
            data = Encoding.ASCII.GetBytes(s);
            server.SendTo(data, 0, data.Length, SocketFlags.None, remote);
        }
        server.Close();
    }
}