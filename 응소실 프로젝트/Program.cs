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
            //학생정보 담는 자료구조
            //어떤 호스트를 나타내는지 확인 할수 있도록

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            //Login login = new Login();
            //Application.Run(login);

            string add = "127.0.0.1";

            StudentData studentData = new StudentData
            {
                StudentName = "심재윤",
                StudentNum = Int32.Parse("2019203051"),
                StudentState = "3학년재학",
                StudentCategory = "재학",
                StudentMajor = "소프트웨어학부",
                Location = new Location { x=0, y = 0 }
            };

            StudentManager.AddStudent(add, studentData);

            In_Game Lobby = new In_Game();
            MainClient mainClient = new MainClient();
            Application.Run(Lobby);

            /*if (login.Login_Result)
            {
                //클라이언트 스레드 실행
                MainClient mainClient = new MainClient();
                Application.Run(Lobby); // Login 폼 실행 후 Lobby 폼 실행
            }*/
        }
    }
}
