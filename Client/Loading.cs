using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
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
        private string resource_name;
        private int animation_count = 0;

        public Loading(int map)
        {
            this.StartPosition = FormStartPosition.CenterScreen;
            this.DoubleBuffered = true;
            BaseForm_test.Game_Timer.Stop();
            InitializeComponent();
            this.map = map;
        }

        private void Loading_Load(object sender, EventArgs e)
        {
            Timer animation_timer = new Timer();
            animation_timer.Interval = 240;
            animation_timer.Tick += animation_timer_tick;

            Timer timer = new Timer();
            timer.Interval = 2000;
            timer.Tick += (s, ev) =>
            {
                animation_timer.Stop();
                timer.Stop();
                this.Close();
            };
            timer.Start();
            animation_timer.Start();
        }

        private void animation_timer_tick(object sender, EventArgs e)
        {
            int image_index = animation_count % 4 + 1;
            animation_count++;

            resource_name = "loading" + image_index;
            this.BackgroundImage = (System.Drawing.Image)Properties.Resources.ResourceManager.GetObject(resource_name);
        }


        private void Loading_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (map == 1) // First
            {
                Loader.LocationLoad(MapType.PS_First);
                title = "해적선에서 살아남기";
                content = "참가자들은 해적에게 납치되어 그들의 선박에 갇힌다. \n\n" +
                "해적들이 자신을 찾기 전에 항해도, 수수께끼 그리고\n\n해적들의 물건을 이용해 선박에서 탈출해야한다!!! ";
                PS_paper pS_Paper = new PS_paper(title, content, 1);
                pS_Paper.Show();
                BaseForm_test.Count_Timer.Start();
            }
            else if (map == 2) // Second
            {
                Loader.LocationLoad (MapType.PS_Second);
                PS_Second pS_Second = new PS_Second();
                pS_Second.Show();
            }
            else if (map == 3) // Final
            {
                Loader.LocationLoad(MapType.PS_Final);
                PS_Final pS_Final = new PS_Final();
                pS_Final.Show();
            }
        }
    }
}
