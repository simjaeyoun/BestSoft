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
            

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Login login = new Login();

            Application.Run(login);

            MainClient mainClient = new MainClient();
            
            In_Game Lobby = new In_Game();

            Application.ApplicationExit += Application_ApplicationExit;

            if (login.Login_Result)
            {
                Application.Run(Lobby); // Login 폼 실행 후 Lobby 폼 실행
            }


        }
        private static void Application_ApplicationExit(object sender, EventArgs e)
        {
            // Terminate the application
            Environment.Exit(0);
        }
    }
}
