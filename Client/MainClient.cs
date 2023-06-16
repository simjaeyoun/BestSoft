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
using System.Drawing;
using System.Net;

namespace 로그인화면
{
    public class PacketType
    {
        public const int AboutLocation = 1;
        public const int AboutConnect = 2;
        public const int AboutRemove = 3;
        public const int AboutServerCheck = 4;
        public const int AboutKey = 5;
        public const int AboutCharacter = 6;
        public const int AboutNext = 7;
        public const int AboutChat = 8; //
    }

    public class MapType
    {
        public const int Lobby = 1;
        public const int PS_First = 2;
        public const int PS_Second = 3;
        public const int PS_Final = 4;
    }

    public class Packet<T>
    {
        public T packet { get; set; }
        public int packetType { get; set; }
        public string modifierID { get; set; }
    }
    public class Info_Next
    {
        public string ObstacleName { get; set; }
        public bool result { get; set; }
    }

    public class Location
    {
        public int X { get; set; }
        public int Y { get; set; }
    }
    public class BubbleChat //
    {
        public string InputChat { get; set; }
        public string LogChat { get; set; }
        public bool HasBeenUpdated { get; set; }
    }
    public class StudentData
    {
        public string StudentName { get; set; }
        public int StudentNum { get; set; }
        public string StudentState { get; set; }
        public string StudentMajor { get; set; }
        public string StudentCategory { get; set; }

        public Image profile { get; set; }
        public Ch_Color clr { get; set; }
        public Location Location { get; set; }
        public Move_Key Key { get; set; }
        public Info_Next Info { get; set; }
        public BubbleChat bubblechat { get; set; } //

    }

    public class MainClient
    {
        public static TcpClient client = null;
        public static string serverAdd;
        private string ClientAdd;
        //mainclient호출하면서 연결 및 dic에 add하기

        public MainClient()
        {

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
            client.Connect("172.20.10.2", 9999);

            MessageBox.Show(((IPEndPoint)client.Client.LocalEndPoint).Address.ToString());

            ClientAdd = ((IPEndPoint)client.Client.LocalEndPoint).Address.ToString();

            StudentManager.AddStudent(ClientAdd, BaseForm_test.My_StudentData);
            BaseForm_test.My_add = ClientAdd; //

            Packet<object> packet = new Packet<object>
            {
                packet = StudentManager.StudentDic[ClientAdd],
                modifierID = ClientAdd,
                packetType = PacketType.AboutConnect
            };

            string json = JsonConvert.SerializeObject(packet);
            byte[] bytes = Encoding.UTF8.GetBytes(json + '\n');
            client.GetStream().Write(bytes, 0, bytes.Length);


            //ReadMeassageThread.Start();


            Task.Run(() => ReadMessage());

        }

        private async void ReadMessage()
        {
            byte[] readBuffer = new byte[1024];
            await client.GetStream().ReadAsync(readBuffer, 0, readBuffer.Length);
            Task.Run(() => ReadMessage());

            string receivedJson = Encoding.UTF8.GetString(readBuffer, 0, readBuffer.Length);
            string[] jsonArray = receivedJson.Split('\n');
            string[] JsonArray = jsonArray.Take(jsonArray.Length - 1).ToArray();

            for (int i = 0; i < JsonArray.Length; i++)
            {
                Packet<object> Receivedpacket = JsonConvert.DeserializeObject<Packet<object>>(JsonArray[i]);
                int index = i;
                Task.Run(() => ProcessData(Receivedpacket, JsonArray[index]));
            }

        }

        private async void ProcessData(Packet<object> Receivedpacket, string receivedJson)
        {
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

                            if (StudentData.clr != Ch_Color.UnKnown && BaseForm_test.lock1 == true)
                            {
                                StudentManager.StudentDic.TryGetValue(Receivedpacket.modifierID, out BaseForm_test.StudentData);
                                BaseForm_test.Other_player = new Player(StudentData.clr, StudentData.Location.X, StudentData.Location.Y, Receivedpacket.modifierID);
                                BaseForm_test.lock1 = false;
                                BaseForm_test.Invalidate_Lock1 = true;
                                Loader.Check_Character(StudentData.clr);
                                BaseForm_test.Other_Ip[BaseForm_test.Other_Count++] = Receivedpacket.modifierID;
                                BaseForm_test.Other_Ip_1 = Receivedpacket.modifierID;
                            }else if (StudentData.clr != Ch_Color.UnKnown && BaseForm_test.lock2 == true)
                            {
                                StudentManager.StudentDic.TryGetValue(Receivedpacket.modifierID, out BaseForm_test.StudentData_1);
                                BaseForm_test.Other_player_1 = new Player(StudentData.clr, StudentData.Location.X, StudentData.Location.Y, Receivedpacket.modifierID);
                                BaseForm_test.lock2 = false;
                                BaseForm_test.Invalidate_Lock2 = true;
                                Loader.Check_Character(StudentData.clr);
                                BaseForm_test.Other_Ip[BaseForm_test.Other_Count++] = Receivedpacket.modifierID;
                                BaseForm_test.Other_Ip_2 = Receivedpacket.modifierID;
                            }else if (StudentData.clr != Ch_Color.UnKnown && BaseForm_test.lock3 == true)
                            {
                                StudentManager.StudentDic.TryGetValue(Receivedpacket.modifierID, out BaseForm_test.StudentData_2);
                                BaseForm_test.Other_player_2 = new Player(StudentData.clr, StudentData.Location.X, StudentData.Location.Y, Receivedpacket.modifierID);
                                BaseForm_test.lock3 = false;
                                BaseForm_test.Invalidate_Lock3 = true;
                                Loader.Check_Character(StudentData.clr);
                                BaseForm_test.Other_Ip[BaseForm_test.Other_Count++] = Receivedpacket.modifierID;
                                BaseForm_test.Other_Ip_3 = Receivedpacket.modifierID;
                            }

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

                            if (BaseForm_test.Me != null)
                            {
                                if (BaseForm_test.Me.address == Receivedpacket.modifierID)
                                {
                                    BaseForm_test.Me.location = location;
                                }
                            }

                            if (BaseForm_test.lock1 == false)
                            {
                                if (BaseForm_test.Other_player.address == Receivedpacket.modifierID)
                                {
                                    BaseForm_test.Other_player.location = location;
                                }
                            }
                            if (BaseForm_test.lock2 == false)
                            {
                                if (BaseForm_test.Other_player_1.address == Receivedpacket.modifierID)
                                {
                                    BaseForm_test.Other_player_1.location = location;
                                }
                            }
                            if (BaseForm_test.lock3 == false)
                            {
                                if (BaseForm_test.Other_player_2.address == Receivedpacket.modifierID)
                                {
                                    BaseForm_test.Other_player_2.location = location;
                                }
                            }
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
                            if (BaseForm_test.Me != null)
                            {
                                if (BaseForm_test.Me.address == Receivedpacket.modifierID)
                                {
                                    BaseForm_test.Me.Key = key;
                                }
                            }

                            if (BaseForm_test.lock1 == false)
                            {
                                if (BaseForm_test.Other_player.address == Receivedpacket.modifierID)
                                {
                                    BaseForm_test.Other_player.Key = key;

                                }
                            }
                            if (BaseForm_test.lock2 == false)
                            {
                                if (BaseForm_test.Other_player_1.address == Receivedpacket.modifierID)
                                {
                                    BaseForm_test.Other_player_1.Key = key;

                                }
                            }
                            if (BaseForm_test.lock3 == false)
                            {
                                if (BaseForm_test.Other_player_2.address == Receivedpacket.modifierID)
                                {
                                    BaseForm_test.Other_player_2.Key = key;

                                }
                            }
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
                            if (clr == Ch_Color.Black) { StudentManager.StudentDic[Receivedpacket.modifierID].profile = Properties.Resources.playerQ_forSelect; }
                            else if (clr == Ch_Color.Orange) { StudentManager.StudentDic[Receivedpacket.modifierID].profile = Properties.Resources.playerW_forSelect; }
                            else if (clr == Ch_Color.Green) { StudentManager.StudentDic[Receivedpacket.modifierID].profile = Properties.Resources.playerE_forSelect; }
                            else if (clr == Ch_Color.Blue) { StudentManager.StudentDic[Receivedpacket.modifierID].profile = Properties.Resources.playerR_forSelect; }

                            //
                            if (BaseForm_test.My_add != Receivedpacket.modifierID)
                            {
                                switch (BaseForm_test.Other_Count)
                                {
                                    case 0:
                                        {
                                            StudentManager.StudentDic.TryGetValue(Receivedpacket.modifierID, out BaseForm_test.StudentData);
                                            BaseForm_test.Other_player = new Player(clr, Receivedpacket.modifierID);
                                            BaseForm_test.lock1 = false;
                                            BaseForm_test.Invalidate_Lock1 = true;
                                            Loader.Check_Character(clr);
                                            BaseForm_test.Other_Ip[BaseForm_test.Other_Count++] = Receivedpacket.modifierID;
                                            BaseForm_test.Other_Ip_1 = Receivedpacket.modifierID;

                                            break;
                                        }
                                    case 1:
                                        {
                                            StudentManager.StudentDic.TryGetValue(Receivedpacket.modifierID, out BaseForm_test.StudentData_1);
                                            BaseForm_test.Other_player_1 = new Player(clr, Receivedpacket.modifierID);
                                            BaseForm_test.lock2 = false;
                                            BaseForm_test.Invalidate_Lock2 = true;
                                            Loader.Check_Character(clr);
                                            BaseForm_test.Other_Ip[BaseForm_test.Other_Count++] = Receivedpacket.modifierID;
                                            BaseForm_test.Other_Ip_2 = Receivedpacket.modifierID;

                                            break;
                                        }
                                    case 2:
                                        {
                                            StudentManager.StudentDic.TryGetValue(Receivedpacket.modifierID, out BaseForm_test.StudentData_2);
                                            BaseForm_test.Other_player_2 = new Player(clr, Receivedpacket.modifierID);
                                            BaseForm_test.lock3 = false;
                                            BaseForm_test.Invalidate_Lock3 = true;
                                            Loader.Check_Character(clr);
                                            BaseForm_test.Other_Ip[BaseForm_test.Other_Count++] = Receivedpacket.modifierID;
                                            BaseForm_test.Other_Ip_3 = Receivedpacket.modifierID;

                                            break;
                                        }
                                }
                            }

                        }
                        break;
                    }
                case PacketType.AboutNext:
                    {
                        //이동
                        Info_Next Info = JsonConvert.DeserializeObject<Packet<Info_Next>>(receivedJson).packet;
                        if (Info != null)
                        {
                            //MessageBox.Show("Next packet");

                            StudentManager.StudentDic[Receivedpacket.modifierID].Info = Info;
                            if (BaseForm_test.lock1 == false)
                            {
                                if (BaseForm_test.Other_player.address == Receivedpacket.modifierID)
                                {
                                    BaseForm_test.Other_player.Info = Info;

                                }
                            }
                            if (BaseForm_test.lock2 == false)
                            {
                                if (BaseForm_test.Other_player_1.address == Receivedpacket.modifierID)
                                {
                                    BaseForm_test.Other_player_1.Info = Info;

                                }
                            }
                            if (BaseForm_test.lock3 == false)
                            {
                                if (BaseForm_test.Other_player_2.address == Receivedpacket.modifierID)
                                {
                                    BaseForm_test.Other_player_2.Info = Info;

                                }
                            }
                        }
                        break;
                    }
                case PacketType.AboutChat: //
                    {
                        BubbleChat bubbleChat = JsonConvert.DeserializeObject<Packet<BubbleChat>>(receivedJson).packet;
                        if (bubbleChat != null)
                        {
                            StudentManager.StudentDic[Receivedpacket.modifierID].bubblechat = bubbleChat;
                            //ChatForm.ChatLog.AppendText(bubbleChat.chat + "\r\n");

                            //MessageBox.Show(StudentManager.StudentDic[Receivedpacket.modifierID].bubblechat.chat);
                        }

                        break;
                    }
                case PacketType.AboutRemove:
                    {
                        //MessageBox.Show("remove packet");

                        StudentManager.RemoveStudent(Receivedpacket.modifierID);
                        break;
                    }
            }

        }


            public static async void SendData(object sendData, int packetType, string modifierID)
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
                case PacketType.AboutNext:
                    SendType = PacketType.AboutNext;
                    break;
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
            byte[] bytes = Encoding.UTF8.GetBytes(json + '\n');
            await Task.Run(() => client.GetStream().Write(bytes, 0, bytes.Length));

        }


    }

    public static class StudentManager
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

            if (BaseForm_test.Other_player != null && Address == BaseForm_test.Other_player.address)
            {
                BaseForm_test.lock1 = true;
            }
            if (BaseForm_test.Other_player_1 != null && Address == BaseForm_test.Other_player_1.address)
            {
                BaseForm_test.lock2 = true;
            }
            if (BaseForm_test.Other_player_2 != null && Address == BaseForm_test.Other_player_2.address)
            {
                BaseForm_test.lock3 = true;
            }
            //본인이 아니라면 picturebox 삭제
        }
    }
}
