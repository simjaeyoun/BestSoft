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
    public partial class Select_Color : Form
    {
        public Select_Color()
        {
            InitializeComponent();
        }

        private void button1_Click_1(object sender, EventArgs e) // Blakc btn 클릭 
        {
            BaseForm_test.Me = new Player(Ch_Color.Black);
            MainClient.SendData(Ch_Color.Black, PacketType.AboutCharacter, "127.0.0.2");
            this.Close();
        }

        private void button2_Click_1(object sender, EventArgs e) // Orange btn 클릭
        {
            BaseForm_test.Me = new Player(Ch_Color.Orange);
            MainClient.SendData(Ch_Color.Orange, PacketType.AboutCharacter, "127.0.0.2");
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            BaseForm_test.Me = new Player(Ch_Color.Green);
            MainClient.SendData(Ch_Color.Green, PacketType.AboutCharacter, "127.0.0.2");
            this.Close();
        }
        private void Select_Color_FormClosing(object sender, FormClosingEventArgs e)
        {
         //   if (e.CloseReason == CloseReason.UserClosing) // x버튼을 눌러서 종료했을 때 실행
         //       Application.Exit();
        }
    }
}
