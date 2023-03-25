using System.Net.Sockets;
using System.Net;
using System.Text;
{
    IPEndPoint iep = new IPEndPoint(IPAddress.Parse("127.0.0.1"),2008); 
    Socket client = new Socket(AddressFamily.InterNetwork,SocketType.Dgram, ProtocolType.Udp); 
    String s = "Chao server";
    byte[] data = new byte[1024]; 
    data = Encoding.ASCII.GetBytes(s); client.SendTo(data, iep); 
    EndPoint remote = (EndPoint)iep; 
    data = new byte[1024];
    int recv = client.ReceiveFrom(data, ref remote); s =Encoding.ASCII.GetString(data,0, recv);
    Console.WriteLine(" Server{ 0}",s);
    while (true)
    {
        s = Console.ReadLine();
        data=new byte[1024];
        data = Encoding.ASCII.GetBytes(s);
        client.SendTo(data, remote);
        if(s.ToUpper().Equals("QUIT"))
            break; 
        data = new byte[1024]; 
        recv = client.ReceiveFrom(data, ref remote);
        s =Encoding.ASCII.GetString(data,0, recv); 
        Console.WriteLine(s);
}
client.Close();
}