using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using Newtonsoft.Json;
using System.Reflection;

//await async

namespace 응소실프로젝트서버
{
    class MainServer
    {

        ClientManager? clientManager = null;
        ConcurrentBag<string>? AccessLog = null;
        Thread? ConectCheckHost = null;
           
        public MainServer()
        {
            clientManager = new ClientManager();
            AccessLog = new ConcurrentBag<string>();
            clientManager.ConstructHandler += AlarmAllClient;
            clientManager.LocationParsingAction += LocationParsing;
            clientManager.RemoveHandler += RemoveClient;

            Task serverStart = Task.Run(() =>
            {
                Serverlisten();

            });
            ConectCheckHost = new Thread(ConectCheck);
            ConectCheckHost.Start();
        }
        //client가 연결되었는지 체크 안되어 있으면 remove
        private void Serverlisten()
        {
            TcpListener listener = new TcpListener(new IPEndPoint(IPAddress.Any, 9999));
            listener.Start();

            while (true)
            {
                Task<TcpClient> acceptTask = listener.AcceptTcpClientAsync();

                acceptTask.Wait();

                TcpClient newClient = acceptTask.Result;

                clientManager.AddClient(newClient);
            }
        }
        private void ConectCheck()
        {
            while (true)
            {
                SendData("<Server Check>", PacketType.AboutServerCheck, "");
                Thread.Sleep(5000);
            }

        }

        // 접속이나, 접속해제할때 서버에 메시지 뜨게 함
        private void ClientAcess(string message)
        {
            AccessLog.Add(message);  
        }
        private async void LocationParsing(Location location,string sender)
        {
            await Task.Run(()=>SendData(location, PacketType.AboutLocation, sender));
        }
        private async void RemoveClient(ClientData clientData)
        {
            ClientData result = null;
            ClientManager.clientDic.TryRemove(clientData.clientID, out result);
            string leaveLog = string.Format("[{0}] {1} Leave Server", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), result.StudentData.StudentName);
            ClientAcess(leaveLog);

            await Task.Run(() => SendData(result.StudentData, PacketType.AboutRemove, result.clientID));

            //클라이언트 들에게 picture없애게 하기
            //클라이언트 자료구조에서 없어지는 거 제외하기

        }
        private async void AlarmAllClient(StudentData studentData,string sender)
        {

            string accessLog = string.Format("[{0}] {1} Access Server", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), studentData.StudentName);
            ClientAcess(accessLog);

            //모든 cli한테 새롭게 connect된 cli정보 보내기
            await Task.Run(() => SendData(studentData, PacketType.AboutConnect, sender));


            //connect된 cli한테 모든 cli 데이터 보내기
            foreach(var item in  ClientManager.clientDic.Keys)
            {
                if (item == sender)
                    continue;

                Packet<StudentData> packet = new Packet<StudentData> { 
                    packet = ClientManager.clientDic[item].StudentData,
                    modifierID = item,
                    packetType = PacketType.AboutConnect
                };
                string json = JsonConvert.SerializeObject(packet);
                byte[] sendBytes = Encoding.UTF8.GetBytes(json);
                ClientManager.clientDic[sender].tcpClient.GetStream().Write(sendBytes, 0, sendBytes.Length);
                //here
                
            }


        }


        private void SendData(object sendData, int packetType, string modifierID)
        {

            string json = null;
            Packet<object> Packet = null;
            object sendedData = null;


            switch (packetType)
            {
                case PacketType.AboutConnect:
                    {
                        sendedData = sendData as StudentData;
                        break;
                    }
                case PacketType.AboutLocation:
                    {
                        sendedData = sendData as Location;
                        break;
                    }
                case PacketType.AboutRemove:
                    {
                        sendedData = sendData as StudentData;
                        break;
                    }
                case PacketType.AboutServerCheck:
                    {
                        sendedData = sendData as string;
                        break;
                    }
            }

            if (sendedData != null)
            {
                Packet = new Packet<object> { packet = sendedData, packetType = packetType, modifierID = modifierID };
                json = JsonConvert.SerializeObject(Packet);
            }

            byte[] sendBytes = Encoding.UTF8.GetBytes(json);

            foreach (string i in ClientManager.clientDic.Keys)
            {
                try
                {
                    if (i == modifierID)
                    {
                        continue;
                    }
                    //즉각 반영 안되면 thread로 만들어서 돌리기
                    ClientManager.clientDic[i].tcpClient.GetStream().Write(sendBytes, 0, sendBytes.Length);
                   
                }
                catch (Exception e)
                {
                    //check하는데 cli에게 write가 안되면 없앰
                    RemoveClient(ClientManager.clientDic[i]);
                }
            }
        }
        
        public void view()
        {
            while (true)
            {
                if (ClientManager.clientDic.Count == 0)
                {
                    Console.WriteLine("접속기록이 없습니다.");
                    //Thread.Sleep(1000);
                    //return;
                }
                else
                {
                    foreach (var item in AccessLog)
                    {
                        Console.WriteLine(AccessLog.Count);
                        Console.WriteLine(item);
                    }

                    foreach ( var item in ClientManager.clientDic.Keys)
                    {
                        Console.WriteLine(item);
                    }
                }
                //Console.WriteLine("here");
                Console.ReadKey();

            }
        }


    }
}
