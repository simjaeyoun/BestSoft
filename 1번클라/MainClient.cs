using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net.Configuration;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Newtonsoft.Json;
using System.Runtime.Remoting.Channels;
using System.Windows.Forms;

namespace 로그인화면
{

    class PacketType
    {
        public const int AboutLocation = 1;
        public const int AboutConnect = 2;
        public const int AboutRemove = 3;
        public const int AboutServerCheck = 4;
        public const int AboutKey = 5;
        public const int AboutCharacter = 6;
        // chat 구현
        public const int AboutChat = 7;
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
    // chat 추가  
    class BubbleChat
    {
        public string chat { get; set; }

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
        public Location Location { get; set; }
        public Move_Key Key { get; set; }
        // chat
        public BubbleChat bubblechat { get; set; }
    }

    class MainClient
    {
        Thread ReadMeassageThread = null;
        public static TcpClient client = null;
        //mainclient호출하면서 연결 및 dic에 add하기

        public MainClient()
        {

            ReadMeassageThread = new Thread(ReadMessage);
            /*
            Thread a = new Thread(view);
            a.Start();
            */
            Connect();
        }

        public void view()
        {
            while (true)
            {

                foreach (var item in StudentManager.StudentDic.Keys)
                    MessageBox.Show(item);

            }
        }


        private void Connect()
        {
            client = new TcpClient();

            //현재 client주소
            string add = "127.0.0.1";
            //클라이언트 비동기로도 해보고 비교해보기
            client.Connect(add, 9999);


            Packet<object> packet = new Packet<object>
            {
                packet = StudentManager.StudentDic[add],
                modifierID = add,
                packetType = PacketType.AboutConnect
            };

            string json = JsonConvert.SerializeObject(packet);
            byte[] bytes = Encoding.UTF8.GetBytes(json);
            client.GetStream().Write(bytes, 0, bytes.Length);


            ReadMeassageThread.Start();


        }
        private void ReadMessage()
        {
            while (true)
            {
                byte[] readBuffer = new byte[1024];
                client.GetStream().Read(readBuffer, 0, readBuffer.Length);

                string receivedJson = Encoding.UTF8.GetString(readBuffer, 0, readBuffer.Length);

                Packet<object> Receivedpacket = JsonConvert.DeserializeObject<Packet<object>>(receivedJson);

                switch (Receivedpacket.packetType)
                {
                    case PacketType.AboutConnect:
                        {
                            //client추가
                            StudentData StudentData = JsonConvert.DeserializeObject<Packet<StudentData>>(receivedJson).packet;
                            if (StudentData != null)
                            {
                                //MessageBox.Show("connect packet");

                                StudentManager.AddStudent(Receivedpacket.modifierID, StudentData);
                            }


                            break;
                        }
                    case PacketType.AboutLocation:
                        {
                            //해당 학생의 위치 옮기기
                            Location location = JsonConvert.DeserializeObject<Packet<Location>>(receivedJson).packet;
                            if (location != null)
                            {
                                //MessageBox.Show("location packet");

                                StudentManager.StudentDic[Receivedpacket.modifierID].Location = location;
                            }
                            break;
                        }
                    case PacketType.AboutKey:
                        {
                            //이동
                            Move_Key key = JsonConvert.DeserializeObject<Packet<Move_Key>>(receivedJson).packet;
                            if (key != null)
                            {
                                //MessageBox.Show("Move_key packet");

                                StudentManager.StudentDic[Receivedpacket.modifierID].Key = key;
                            }
                            break;
                        }
                    case PacketType.AboutCharacter:
                        {
                            //캐릭터
                            Ch_Color clr = JsonConvert.DeserializeObject<Packet<Ch_Color>>(receivedJson).packet;
                            if (Ch_Color.Black <= clr && Ch_Color.UnKnown >= clr)
                            {
                                //MessageBox.Show("Character packet");

                                StudentManager.StudentDic[Receivedpacket.modifierID].clr = clr;
                            }
                            break;
                        }
                    case PacketType.AboutRemove:
                        {
                            MessageBox.Show("remove packet");

                            StudentManager.RemoveStudent(Receivedpacket.modifierID);
                            break;
                        }
                    case PacketType.AboutServerCheck:
                        {
                            //MessageBox.Show("server packet");
                            continue;

                        }
                    // chat packet 추가 
                    case PacketType.AboutChat:
                        {
                            BubbleChat bubbleChat = JsonConvert.DeserializeObject<Packet<BubbleChat>>(receivedJson).packet;
                            if (bubbleChat != null)
                            {
                                StudentManager.StudentDic[Receivedpacket.modifierID].bubblechat = bubbleChat;
                                ChatForm.ChatLog.AppendText(bubbleChat.chat + "\r\n");
                                //MessageBox.Show(StudentManager.StudentDic[Receivedpacket.modifierID].bubblechat.chat);
                            }

                            break;
                        }
                }


            }

        }
        public static void SendData(object sendData, int packetType, string modifierID)
        {
            int SendType = 0;
            switch (packetType)
            {
                case PacketType.AboutConnect:
                    SendType = PacketType.AboutConnect;
                    break;
                case PacketType.AboutLocation:
                    SendType = PacketType.AboutLocation;
                    break;
                case PacketType.AboutRemove:
                    SendType = PacketType.AboutRemove;
                    break;
                case PacketType.AboutKey:
                    SendType = PacketType.AboutKey;
                    break;
                case PacketType.AboutCharacter:
                    SendType = PacketType.AboutCharacter;
                    break;
                // 추가
                case PacketType.AboutChat:
                    SendType = PacketType.AboutChat;
                    break;

            }

            Packet<object> packet = new Packet<object>
            {
                packet = sendData,
                modifierID = modifierID,
                packetType = SendType
            };

            string json = JsonConvert.SerializeObject(packet);
            byte[] bytes = Encoding.UTF8.GetBytes(json);
            client.GetStream().Write(bytes, 0, bytes.Length);

        }



        //connect랑 send하는거 추가 하기

        //던전1 던전 2등르로 정의해서 거기서 처리 
        //로그아웃 기능

    }

    static class StudentManager
    {
        public static ConcurrentDictionary<string, StudentData> StudentDic = new ConcurrentDictionary<string, StudentData>();

        public static void AddStudent(string Address, StudentData student)
        {
            StudentDic.TryAdd(Address, student);
            //본인이 아니라면 picturebox 추가
        }
        public static void RemoveStudent(string Address)
        {
            StudentData value = null;
            StudentDic.TryRemove(Address, out value);
            //본인이 아니라면 picturebox 삭제
        }
    }
}
