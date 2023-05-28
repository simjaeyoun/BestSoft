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

    //제네릭으로 하기
    //해보고 다른 방향 모색


    class ClientManager
    {
        public static ConcurrentDictionary<string, ClientData> clientDic = new ConcurrentDictionary<string, ClientData>();

        //다른 클라이언트 들에게 위치 뿌리기
        //비동기로 실행
        public event Action<Location, string> LocationParsingAction = null;
        //로그 기록 남기기
        public event Action<StudentData, string> ConstructHandler = null;
        //클라이언트 없애기
        public event Action<ClientData> RemoveHandler = null;
        //Key 
        public event Action<Move_Key, string> Move_KeyParsingAction = null;
        //Character
        public event Action<Ch_Color, string> CharacterAction = null;
        // chat
        public event Action<BubbleChat, string> BubbleChatAction = null;

        public void AddClient(TcpClient newClient)
        {
            ClientData currentClient = new ClientData(newClient);
            try
            {
                clientDic.TryAdd(currentClient.clientID, currentClient);
                //currentClient.tcpClient.GetStream().BeginRead(currentClient.readBuffer, 0, currentClient.readBuffer.Length, new AsyncCallback(DataReceived), currentClient);
                Task.Run(() => DataReceived(currentClient));
            }

            catch (Exception e)
            {

            }
        }

        //async 메서드는 void 아니면 Task를 반환
        private async Task DataReceived(ClientData client)
        {

            //ClientData client = ar.AsyncState as ClientData;

            try
            {
                int byteLength = await client.tcpClient.GetStream().ReadAsync(client.readBuffer, 0, client.readBuffer.Length);
                string receivedJson = Encoding.UTF8.GetString(client.readBuffer, 0, byteLength);
                Packet<object> ReceiveData = JsonConvert.DeserializeObject<Packet<object>>(receivedJson);


                //클라이언트가 종료됐을때
                if (ReceiveData.packetType == PacketType.AboutRemove)
                {
                    if (RemoveHandler != null)
                    {
                        Console.WriteLine("remove packet");
                        await Task.Run(() => RemoveHandler.Invoke(client));
                        return;
                    }
                }

                //처음에는 학생 정보 가져오기 및 클라이언트에게 다른 클라이언트 보이도록
                if (ReceiveData.packetType == PacketType.AboutConnect)
                {
                    if (ConstructHandler != null)
                    {
                        //모든 클라이언트에게 모든 클라이언트의 piture보여주기
                        //모든 클라이언트에게 모든 클라리언트의 정보 알려주기
                        //접속하기

                        StudentData studentData = JsonConvert.DeserializeObject<Packet<StudentData>>(receivedJson).packet;


                        if (studentData != null)
                        {
                            client.StudentData = (StudentData)studentData;
                        }
                        //method that creates and starts a new Task to execute a specified delegate asynchronously on a thread pool thread.
                        //Task.Run(() => ConstructHandler.Invoke(client.StudentData, ReceiveData.modifierID ));
                        Console.WriteLine("connect packet");
                        await Task.Run(() => ConstructHandler.Invoke(client.StudentData, ReceiveData.modifierID));


                    }

                }
                if (ReceiveData.packetType == PacketType.AboutKey)
                {

                    Move_Key key = JsonConvert.DeserializeObject<Packet<Move_Key>>(receivedJson).packet;

                    if (key != null)
                        client.StudentData.key = key;
                    //첫번째 null은 이 작업이 완료 되기 전까지는 endinvoke호출 불가
                    //일을 처리하는데 정보의 상태가 필요 없다면 null
                    Console.WriteLine("Move_Key packet");
                    await Task.Run(() => Move_KeyParsingAction.Invoke(client.StudentData.key, ReceiveData.modifierID));

                }

                if (ReceiveData.packetType == PacketType.AboutCharacter)
                {

                    Ch_Color clr = JsonConvert.DeserializeObject<Packet<Ch_Color>>(receivedJson).packet;

                    if (Ch_Color.Black <= clr && Ch_Color.UnKnown >= clr)
                        client.StudentData.clr = clr;
                    //첫번째 null은 이 작업이 완료 되기 전까지는 endinvoke호출 불가
                    //일을 처리하는데 정보의 상태가 필요 없다면 null
                    Console.WriteLine("Character packet");
                    await Task.Run(() => CharacterAction.Invoke(client.StudentData.clr, ReceiveData.modifierID));

                }

                //학생의 위치 받고, 클라이언트 들에게 뿌리기
                if (ReceiveData.packetType == PacketType.AboutLocation)
                {
                    if (LocationParsingAction != null)
                    {
                        Location Location = JsonConvert.DeserializeObject<Packet<Location>>(receivedJson).packet;

                        if (Location != null)
                            client.StudentData.Location = Location;
                        //첫번째 null은 이 작업이 완료 되기 전까지는 endinvoke호출 불가
                        //일을 처리하는데 정보의 상태가 필요 없다면 null
                        Console.WriteLine("location packet");
                        await Task.Run(() => LocationParsingAction.Invoke(client.StudentData.Location, ReceiveData.modifierID));

                    }
                }
                //비동기 처리 종료했을때는 제외함

                //client.tcpClient.GetStream().BeginRead(client.readBuffer, 0, client.readBuffer.Length, new AsyncCallback(DataReceived), client);
                // 말풍선이랑 채팅 로그를 전부 클라이언트에 뿌려줘야함

                if (ReceiveData.packetType == PacketType.AboutChat)
                {

                    if (BubbleChatAction != null)
                    {

                        BubbleChat bubbleChat = JsonConvert.DeserializeObject<Packet<BubbleChat>>(receivedJson).packet;
                        if (bubbleChat != null)
                            client.StudentData.bubblechat = bubbleChat;


                        Console.WriteLine("메세지 전송");
                        await Task.Run(() => BubbleChatAction.Invoke(client.StudentData.bubblechat, ReceiveData.modifierID));

                    }

                    //서버 로그에 누가 언제 뭘 보냈는지 알면 좋을지도?
                }

                await DataReceived(client);
                return;

            }
            catch (Exception e)
            {

            }
        }

    }

}
