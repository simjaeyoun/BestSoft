using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Windows.Forms;

namespace 로그인화면
{
    public partial class Loading : Form
    {
        private int map;
        private string title;
        private string content;
        public Loading(int map)
        {
            InitializeComponent();
            this.map = map;
        }

        private void Loading_Load(object sender, EventArgs e)
        {
            Timer timer = new Timer();
            timer.Interval = 2000;
            timer.Tick += (s, ev) =>
            {
                timer.Stop();
                this.Close();
            };
            timer.Start();
        }

        private void Loading_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (map == 1)
            {
                title = "해적선에서 살아남기";
                content = "참가자들은 해적에게 납치되어 그들의 선박에 갇힌다. \n\n" +
                "해적들이 자신을 찾기 전에 항해도, 수수께끼 그리고\n\n해적들의 물건을 이용해 선박에서 탈출해야한다!!! ";
                PS_paper pS_Paper = new PS_paper(title, content, 1);
                pS_Paper.Show();
            }
            else if (map == 2)
            {
                PS_Second pS_Second = new PS_Second();
                pS_Second.Show();
            }
            else if (map == 3)
            {
                PS_Final pS_Final = new PS_Final();
                pS_Final.Show();
            }
        }
    }
}
