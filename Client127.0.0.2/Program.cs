using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
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
            //Login login = new Login();

            //Application.Run(login);

            if (/*login.Login_Result*/true)
            {
                string add = "127.0.0.2";

                StudentData studentData = new StudentData
                {
                    StudentName = "kimgyun",
                    StudentNum = 123,
                    StudentState = "test",
                    StudentCategory = "test",
                    StudentMajor = "test",
                    clr = Ch_Color.UnKnown,
                    Location = new Location { X = -1, Y = -1 },
                    Key = new Move_Key { Go_Up = false, Go_Down = false, Go_Left = false, Go_Right = false },
                    Info = new Info_Next { ObstacleName = null, Map = 0, result = false }
                };
                StudentManager.AddStudent(add, studentData);

                MainClient mainClient = new MainClient();

                Loader.ImageLoad();

                Select_Color Select = new Select_Color();
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
