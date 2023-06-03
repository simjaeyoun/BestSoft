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
    public partial class Lobby : BaseForm_test
    {
        private string title;
        private string content;
        public Lobby()
        {
            InitializeComponent();
            timer.Start();
            this.Map = MapType.Lobby;
            BaseForm_test.Me.Map = MapType.Lobby;
        }
        
        protected override void Space_Event(string Obstacle_Name)
        {
            if (Obstacle_Name == "start")
            {
                BaseForm_test.Me.Info.ObstacleName = Obstacle_Name + "_by_other";
                BaseForm_test.Me.Info.result = true;

                MainClient.SendData(BaseForm_test.Me.Info, PacketType.AboutNext, "127.0.0.1");

                ///* 게임 시작 */
                title = "해적선에서 살아남기";
                content = "참가자들은 해적에게 납치되어 그들의 선박에 갇힌다. \n\n" +
                "해적들이 자신을 찾기 전에 항해도, 수수께끼 그리고\n\n해적들의 물건을 이용해 선박에서 탈출해야한다!!! ";
                Loading loading = new Loading(1);
                loading.Show();
                //PS_paper pS_Paper = new PS_paper(title, content, 1);
                //pS_Paper.Show();
                this.timer.Stop();
                this.Hide();
            }
            else if (Obstacle_Name == "start_by_other" && BaseForm_test.Me.Map == this.Map)
            {
                ///* 게임 시작 */
                title = "해적선에서 살아남기";
                content = "참가자들은 해적에게 납치되어 그들의 선박에 갇힌다. \n\n" +
                "해적들이 자신을 찾기 전에 항해도, 수수께끼 그리고\n\n해적들의 물건을 이용해 선박에서 탈출해야한다!!! ";
                Loading loading = new Loading(1);
                loading.Show();
                //PS_paper pS_Paper = new PS_paper(title, content, 1);
                //pS_Paper.Show();
                this.timer.Stop();
                this.Hide();

            }
            else { MessageBox.Show(Obstacle_Name); }
        }
    }
    
}
