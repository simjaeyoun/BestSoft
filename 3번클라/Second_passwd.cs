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
    public partial class Second_passwd : Form
    {
        public Second_passwd()
        {
            InitializeComponent();
        }

        private void btn_OK_Click(object sender, EventArgs e)
        {
            if (txtpasswd.Text == "123")
            {
                DialogResult result = MessageBox.Show("정답입니다! 다음 방으로 넘어갑니다.", "", MessageBoxButtons.OK);
                if (result == DialogResult.OK)
                {
                    Final final = new Final();
                    this.Close();
                    final.Show();
                    Second s = (Second)Application.OpenForms["Second"];
                    s.Close();
                }
            }
            else
                MessageBox.Show("비밀번호가 틀렸습니다.");
        }
    }
}
