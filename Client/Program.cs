using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Windows.Forms;

namespace 로그인화면
{

    internal static class Program
    {
        /// <summary>
        /// 해당 애플리케이션의 주 진입점입니다.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Login login = new Login();

            Application.Run(login);

            if (login.Login_Result == true)
            {
                //string add = "127.0.0.1";
                //BaseForm_test.My_add = add;

                //StudentManager.AddStudent(add, studentData);
                //StudentManager.StudentDic.TryGetValue(add, out BaseForm_test.My_StudentData);


                string ServerAdd;

                UdpClient udpClient = new UdpClient();
                udpClient.EnableBroadcast = true;

                // Broadcast message
                string broadcastMessage = "Hello from client";
                byte[] broadcastBytes = Encoding.ASCII.GetBytes(broadcastMessage);
                // Send the broadcast message to the network
                IPEndPoint broadcastEndPoint = new IPEndPoint(IPAddress.Broadcast, 9999);

                udpClient.Send(broadcastBytes, broadcastBytes.Length, broadcastEndPoint);

                // Wait for responses from the servers  
                while (true)
                {
                    IPEndPoint remoteEndPoint = new IPEndPoint(IPAddress.Any, 0);
                    if (remoteEndPoint == null) { continue; }
                    byte[] responseBytes = udpClient.Receive(ref remoteEndPoint);
                    string responseMessage = Encoding.ASCII.GetString(responseBytes);

                    //string[] Add = responseMessage.Split(':');
                    //ServerAdd = Add[0];
                    //MainClient.ClientAdd = Add[1];

                    ServerAdd = responseMessage;

                    IPAddress serverIPAddress = remoteEndPoint.Address;
                    if (serverIPAddress != null)
                    {
                        MainClient.serverIP = IPAddress.Parse(ServerAdd);
                        string message = string.Format("Server Add : {0}", ServerAdd);
                        MessageBox.Show(message);
                        break;
                    }
                }




                MainClient mainClient = new MainClient();

                Loader.ImageLoad();

                Select_Color Select = new Select_Color(BaseForm_test.My_add);
                Application.Run(Select);

                Lobby lobby = new Lobby();
                Application.Run(lobby);

            }


            //Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);
            //Application.ApplicationExit += Application_ApplicationExit;

        }
        //private static void Application_ApplicationExit(object sender, EventArgs e)
        //{
        //    // Terminate the application
        //    Environment.Exit(0);
        //}
    }
}
