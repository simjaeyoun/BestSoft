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
    public partial class final_pw : Form
    {
        public final_pw()
        {
            InitializeComponent();
        }

        private void btn_OK_Click(object sender, EventArgs e)
        {
            if (txtpasswd.Text == "1234")
            {
                DialogResult result = MessageBox.Show("정답입니다! 방탈출에 성공하였습니다.", "", MessageBoxButtons.OK);
                if (result == DialogResult.OK)
                    Application.Exit();
            }
            else
                MessageBox.Show("비밀번호가 틀렸습니다.");
        }
    }
}
