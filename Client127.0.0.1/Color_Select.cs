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
            
            if (Loader.Selected_Character[0])
            {
                StudentManager.StudentDic["127.0.0.1"].profile = Properties.Resources.playerQ_forSelect;
                BaseForm_test.Me = new Player(Ch_Color.Black, "127.0.0.1");
                MainClient.SendData(Ch_Color.Black, PacketType.AboutCharacter, "127.0.0.1");
                this.Close();
            }
            else { MessageBox.Show("이미 선택"); }
        }

        private void button2_Click_1(object sender, EventArgs e) // Orange btn 클릭
        {
            if (Loader.Selected_Character[1])
            {
                StudentManager.StudentDic["127.0.0.1"].profile = Properties.Resources.playerW_forSelect;
                BaseForm_test.Me = new Player(Ch_Color.Orange, "127.0.0.1");
                MainClient.SendData(Ch_Color.Orange, PacketType.AboutCharacter, "127.0.0.1");
                this.Close();
            }
            else { MessageBox.Show("이미 선택"); }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (Loader.Selected_Character[2])
            {
                StudentManager.StudentDic["127.0.0.1"].profile = Properties.Resources.playerE_forSelect;
                BaseForm_test.Me = new Player(Ch_Color.Green, "127.0.0.1");
                MainClient.SendData(Ch_Color.Green, PacketType.AboutCharacter, "127.0.0.1");
                this.Close();
            }
            else { MessageBox.Show("이미 선택"); }
        }
        private void button4_Click(object sender, EventArgs e)
        {
            if (Loader.Selected_Character[3])
            {
                StudentManager.StudentDic["127.0.0.1"].profile = Properties.Resources.playerR_forSelect;
                BaseForm_test.Me = new Player(Ch_Color.Blue, "127.0.0.1");
                MainClient.SendData(Ch_Color.Blue, PacketType.AboutCharacter, "127.0.0.1");
                this.Close();
            }
            else { MessageBox.Show("이미 선택"); }
        }

        private void Select_Color_FormClosing(object sender, FormClosingEventArgs e)
        {
         //   if (e.CloseReason == CloseReason.UserClosing) // x버튼을 눌러서 종료했을 때 실행
         //       Application.Exit();
        }

    }
}
