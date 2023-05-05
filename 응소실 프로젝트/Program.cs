using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 로그인화면
{
    public class Student
    {
        string Stu_cat;
        string Stu_major;
        string Stu_num;
        string Stu_name;
        string Stu_state;
        /*
        public Student(string stu_cat, string stu_major, string stu_num, string stu_name, string stu_state)
        {
            Stu_cat=stu_cat;
            Stu_major=stu_major;
            Stu_num=stu_num;
            Stu_name=stu_name;
            Stu_state=stu_state;
        }*/
        public String[] Info {
            get { return new String[] { Stu_cat, Stu_major, Stu_num, Stu_name, Stu_state }; }
            set { Stu_cat = value[0]; Stu_major = value[1]; Stu_num = value[2]; Stu_name = value[3]; Stu_state = value[4]; }

        }
    }
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
