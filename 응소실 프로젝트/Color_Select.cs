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
            this.DialogResult = DialogResult.OK;
            In_Game main = (In_Game)Owner;
            In_Game.clr = In_Game.Ch_Color.Black;
        }

        private void button2_Click_1(object sender, EventArgs e) // Orange btn 클릭
        {
            this.DialogResult = DialogResult.OK;
            In_Game main = (In_Game)Owner;
            In_Game.clr = In_Game.Ch_Color.Black;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            In_Game main = (In_Game)Owner;
            In_Game.clr = In_Game.Ch_Color.Black;
        }

        private void Select_Color_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing) // x버튼을 눌러서 종료했을 때 실행
                Application.Exit();
        }
    }
}
