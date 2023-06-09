﻿using System;
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

        public Location Location { get; set; }

    }

    class MainClient
    {
        Thread ReadMeassageThread = null;
        TcpClient client = null;
        //mainclient호출하면서 연결 및 dic에 add하기

        public MainClient() {

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
                modifierID =add, 
                packetType = PacketType.AboutConnect 
            };

            string json = JsonConvert.SerializeObject(packet);
            byte[] bytes = Encoding.UTF8.GetBytes(json);
            client.GetStream().Write(bytes, 0, bytes.Length);
            

            ReadMeassageThread.Start();
            

        }
        private void ReadMessage()
        {
            while(true)
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
                                MessageBox.Show("connect packet");

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
                                MessageBox.Show("location packet");

                                StudentManager.StudentDic[Receivedpacket.modifierID].Location = location;
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
                    }
                

            }

         }
        private void SendData(object sendData, int packetType,string modifierID)
        {
            int SendType=0;
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
            }

            Packet<object> packet = new Packet<object>
            {
                packet = sendData,
                modifierID =modifierID,
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
        
        public static void AddStudent(string Address,StudentData student)
        {
            StudentDic.TryAdd(Address, student);
            //본인이 아니라면 picturebox 추가
        }
        public static void RemoveStudent(string Address)
        {
            StudentData value=null;
            StudentDic.TryRemove(Address,out value);
            //본인이 아니라면 picturebox 삭제
        }
    }
}
