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
    public partial class bookshelf_pw : Form
    {
        public bookshelf_pw()
        {
            InitializeComponent();
        }

        private void btn_OK_Click(object sender, EventArgs e)
        {
            if (txtpasswd.Text == "8668")
            {
                DialogResult result = MessageBox.Show("정답입니다!\n 힌트 : 3", "", MessageBoxButtons.OK);
                if (result == DialogResult.OK)
                    this.Close();
            }
            else
                MessageBox.Show("비밀번호가 틀렸습니다.");
        }
    }
}
