using System;
using System.Collections.Generic;
using System.Linq;
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

            In_Game Lobby = new In_Game();
            if (login.Login_Result)
            {
                Application.Run(Lobby); // Login 폼 실행 후 Lobby 폼 실행
            }
        }
    }
}
