using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace 응소실프로젝트서버
{
    class PacketType
    {
        public const int AboutLocation = 1;
        public const int AboutConnect = 2;
        public const int AboutRemove = 3;
        public const int AboutServerCheck = 4;
    }
    class Packet<T>
    {
        public T packet { get; set; }
        public int packetType { get; set; }
        public string modifierID { get; set; }
    }

    class Location 
    {
        public int x { get; set; }
        public int y { get; set; }
    }
    class StudentData 
    {
        public string StudentName { get; set; }
        public int StudentNum { get; set; }
        public string StudentState { get; set; }
        public string StudentMajor { get; set; }
        public string StudentCategory { get; set; }
        
        public Location Location {get; set; }

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

           
            this.clientID = tcpClient.Client.LocalEndPoint.ToString().Split(":")[0];

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
