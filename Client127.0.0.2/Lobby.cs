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
        public Lobby()
        {
            InitializeComponent();
            timer.Start();
        }
        
        protected override void Space_Event(string Obstacle_Name)
        {
            if (Obstacle_Name == "start")
            {
                if (this.Obstacle_Name == "start")
                {
                    this.Info.ObstacleName = Obstacle_Name;
                    this.Info.result = true;
                    MainClient.SendData(this.Info, PacketType.AboutNext, "127.0.0.2");
                }

                /* 게임 시작 */
                Map_Select map_Select = new Map_Select();
                map_Select.Show();
                this.timer.Stop();
                this.Hide();
            }
            else { MessageBox.Show(Obstacle_Name); }
        }
    }
}
