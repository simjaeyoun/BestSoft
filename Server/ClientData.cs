using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace 응소실프로젝트서버
{
   public enum Ch_Color
    {
        Black,
        Orange,
        Green,
        UnKnown
    }
    class PacketType
    {
        public const int AboutLocation = 1;
        public const int AboutConnect = 2;
        public const int AboutRemove = 3;
        public const int AboutServerCheck = 4;
        public const int AboutKey = 5;
        public const int AboutCharacter = 6;
        public const int AboutNext = 7;
        public const int AboutChat = 8;
    }
    class Packet<T>
    {
        public T packet { get; set; }
        public int packetType { get; set; }
        public string modifierID { get; set; }
    }

    class Location 
    {
        public int X { get; set; }
        public int Y { get; set; }
    }
    class Move_Key
    {
        public bool Go_Up { get; set; }
        public bool Go_Down { get; set; }
        public bool Go_Left { get; set; }
        public bool Go_Right { get; set; }
    }
    public class Info_Next
    {
        public string ObstacleName { get; set; }
        public bool result { get; set; }
    }
    public class BubbleChat //
    {
        public string InputChat { get; set; }
        public string LogChat { get; set; }
        public bool HasBeenUpdated { get; set; }
    }
    class StudentData 
    {
        public string StudentName { get; set; }
        public int StudentNum { get; set; }
        public string StudentState { get; set; }
        public string StudentMajor { get; set; }
        public string StudentCategory { get; set; }
        public Ch_Color clr { get; set; }
        public Location Location {get; set; }
        public Move_Key key { get; set; }
        public Info_Next Info { get; set; }
        public BubbleChat bubblechat { get; set; } //


    }
    class ClientData
    {
        public TcpClient tcpClient { get; set; }
        public Byte[] readBuffer { get; set; }
        
        public string clientID { get; set; }

        public StudentData StudentData { get; set; }

        public ClientData(TcpClient tcpClient)
        {
            readBuffer = new byte[1024];

            this.tcpClient = tcpClient;


            IPAddress clientIPAddress = ((IPEndPoint)tcpClient.Client.RemoteEndPoint).Address;
            this.clientID = clientIPAddress.ToString();

            /*
            char[] splitDivision = new char[2];
            splitDivision[0] = '.';
            splitDivision[1] = ':';

            string[] temp = null;
            //나중에 remoteendpoint로 바꾸기
            temp = tcpClient.Client.LocalEndPoint.ToString().Split(splitDivision);

            this.clientID = int.Parse(temp[3]);
            */
        }


    }
}
