using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

public class SocketServer {
    public static void Main() {
        byte[] bytes = new byte[1024];

        //サーバ側ソケットの作成 & IP, ポートを指定
        Socket socket = new Socket(SocketType.Dgram, ProtocolType.Udp);
        IPAddress ipAddress = IPAddress.Parse("127.0.0.1");
        IPEndPoint serverEP = new IPEndPoint(ipAddress, 3000); //サーバのIPとポート
        IPEndPoint clientEP = new IPEndPoint(ipAddress, 3001); //クライアントのIPとポート
        socket.Bind(serverEP);
        Console.WriteLine("waiting on" + serverEP);

        //メッセージを待ち受け，受信したら表示してメッセージを返す
        while (true) {
            int bytesRec = socket.Receive(bytes);
            string receivedMessage = "Received: " + Encoding.UTF8.GetString(bytes, 0, bytesRec);
            Console.WriteLine(receivedMessage);

            string sendMessage = "kimochiyosugidaro...";
            byte[] byteMessage = Encoding.UTF8.GetBytes(sendMessage);
            socket.Connect(clientEP);
            socket.Send(byteMessage);
        }

        socket.Shutdown(SocketShutdown.Both);
        socket.Close();
    }
}
