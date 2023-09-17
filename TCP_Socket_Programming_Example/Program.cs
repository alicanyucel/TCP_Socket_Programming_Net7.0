using System;
using System.Net;
using System.Net.Sockets;
using System.Security;
using System.Text;
const int serverPortNum =50252;
Console.WriteLine("server için S , Client için C giriniz");
string inp = Console.ReadLine();
if (inp.Equals("S"))
{
    Server();
}
else if (inp.Equals("C"))
{
    Client();
}
else
{
    Console.WriteLine("hatalı giris yaptınız");
}

void Client()
{
    // client soket olustur
    IPEndPoint clientEndPoint=new IPEndPoint(IPAddress.Loopback, 0);
    Socket clientSocket = new Socket(SocketType.Stream,ProtocolType.Tcp);
    clientSocket.Bind(clientEndPoint);
    // bağlantı olustur
    IPEndPoint serverEndPoint = new IPEndPoint(IPAddress.Loopback,serverPortNum);
    clientSocket.Connect(serverEndPoint);
    // send Message
    string messageToSend = "alicanyucel";
    byte[] byteToSend = Encoding.Default.GetBytes(messageToSend);
    clientSocket.Send(byteToSend);
    Console.WriteLine("clientden gönderlien mesaj:" + messageToSend);
    // mesajı göster
    byte[] buffer=new byte[1024];
    int numberofByteReceived = clientSocket.Receive(buffer);
    byte[] receivedBytes=new Byte[numberofByteReceived];
    Array.Copy(buffer,receivedBytes,numberofByteReceived);
    string receivedMessage = Encoding.Default.GetString(receivedBytes);
    Console.WriteLine("clientdan gelen mesaj:"+receivedMessage);
    Console.ReadKey();
}
void Server()
{

    // soket oluştur
    IPEndPoint serverEndPoints = new IPEndPoint(IPAddress.Loopback, serverPortNum);
    Socket welCommingSocket = new Socket(SocketType.Stream, ProtocolType.Tcp);
    welCommingSocket.Bind(serverEndPoints);
    // bağlantıyı dinle
    welCommingSocket.Listen();
    Socket connectionSocket = welCommingSocket.Accept();// bağlantıyı kabul et handshake()  gelen mesajı göster
    byte[] buffer = new byte[1024];
    int numberorBytesReceived = connectionSocket.Receive(buffer);
    byte[] receivedBytes = new byte[numberorBytesReceived];
    Array.Copy(buffer, receivedBytes, numberorBytesReceived);
    string receiveMessage = Encoding.Default.GetString(receivedBytes);
    Console.WriteLine("serverdan gelen mesaj:"+receiveMessage);//logladım
    connectionSocket.Send(receivedBytes);
    Console.ReadKey();
}



