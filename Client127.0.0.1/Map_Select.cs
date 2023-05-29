using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 로그인화면
{
    public partial class Map_Select : Form
    {
        private string title;
        private string content;
        public Map_Select()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
            title = "해적선에서 살아남기";
            content = "참가자들은 해적에게 납치되어 그들의 선박에 갇힌다. \n\n" +
            "해적들이 자신을 찾기 전에 항해도, 수수께끼 그리고\n\n해적들의 물건을 이용해 선박에서 탈출해야한다!!! ";
           
            PS_paper pS_Paper = new PS_paper(title,content,1);
            pS_Paper.Show();
            this.Hide();
        }

        private void Map_Select_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing) // x버튼을 눌러서 종료했을 때 실행
                Application.Exit();
        }
    }
}
