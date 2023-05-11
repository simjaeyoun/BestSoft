using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace 응소실프로젝트서버
{
    class PacketType{
        public const int AboutLocation = 1;
        public const int AboutConnect = 2;
        public const int AboutRemove = 3;
        public const int AboutServerCheck = 4;
    }
    class Packet
    {
        public object packet { get; set; }
        public int packetType { get; set; }
        public string modifierID { get; set; }
    }
    class ClientManager
    {
        public static ConcurrentDictionary<string, ClientData> clientDic = new ConcurrentDictionary<string, ClientData>();
        
        //다른 클라이언트 들에게 위치 뿌리기
        //비동기로 실행
        public event Action<Location,string> LocationParsingAction = null;
        //로그 기록 남기기
        public event Action<StudentData,string> ConstructHandler = null;
        //클라이언트 없애기
        public event Action<ClientData> RemoveHandler = null;

        public void AddClient(TcpClient newClient)
        {
            ClientData currentClient = new ClientData(newClient);
            try
            {
                currentClient.tcpClient.GetStream().BeginRead(currentClient.readBuffer, 0, currentClient.readBuffer.Length, new AsyncCallback(DataReceived), currentClient);
                clientDic.TryAdd(currentClient.clientID, currentClient);
            }

            catch (Exception e)
            {

            }
        }

        private void DataReceived(IAsyncResult ar)
        {

            ClientData client = ar.AsyncState as ClientData;

            try
            {
                int byteLength = client.tcpClient.GetStream().EndRead(ar);
                string receivedJson = Encoding.UTF8.GetString(client.readBuffer, 0, byteLength);
                Packet ReceiveData = JsonConvert.DeserializeObject<Packet>(receivedJson);
                //json

                //클라이언트가 종료됐을때
                if (ReceiveData.packetType == PacketType.AboutRemove)
                {
                    if(RemoveHandler != null)
                    {
                        RemoveHandler.BeginInvoke(client, null, null);
                        return;
                    }
                }

                //비동기 처리 종료했을때는 제외함
                client.tcpClient.GetStream().BeginRead(client.readBuffer, 0, client.readBuffer.Length, new AsyncCallback(DataReceived), client);


                //처음에는 학생 정보 가져오기 및 클라이언트에게 다른 클라이언트 보이도록
                if (ReceiveData.packetType == PacketType.AboutConnect)
                {
                    if(ConstructHandler != null)
                    {
                        //모든 클라이언트에게 모든 클라이언트의 piture보여주기
                        //모든 클라이언트에게 모든 클라리언트의 정보 알려주기
                        //접속하기
                        StudentData studentData = ReceiveData.packet as StudentData;
                        

                        if (studentData != null)
                        {
                            client.StudentData = studentData;
                        }
                        Console.WriteLine(ReceiveData.packet.GetType());
                        Console.WriteLine('\n');
                        Console.WriteLine(studentData?.GetType());
                        Console.WriteLine('\n');

                        //Console.WriteLine(ReceiveData.modifierID);

                        ConstructHandler.BeginInvoke(client.StudentData, ReceiveData.modifierID ,null, null);
                        return;

                    }

                }
                //학생의 위치 받고, 클라이언트 들에게 뿌리기
                if(ReceiveData.packetType == PacketType.AboutLocation)
                {
                    if(LocationParsingAction != null)
                    {
                        var Location = ReceiveData.packet as Location;
                        if (Location != null)
                            client.StudentData.Location = Location;
                        //첫번째 null은 이 작업이 완료 되기 전까지는 endinvoke호출 불가
                        //일을 처리하는데 정보의 상태가 필요 없다면 null
                        LocationParsingAction.BeginInvoke(client.StudentData.Location, ReceiveData.modifierID, null, null);

                    }
                }



            }
            catch (Exception e)
            {

            }
        }

    }

}
