using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 로그인화면
{
    public partial class PS_Second : BaseForm_test
    {
        private string title;
        private string content;
        private PS_hint pS_Hint;

        public PS_Second()
        {
            InitializeComponent();
            timer.Start();
            this.Map = MapType.PS_Second;
            BaseForm_test.Me.Map = MapType.PS_Second;
        }

        protected override void Space_Event(string Obstacle_Name)
        {
            if (Obstacle_Name == "book" && (pS_Hint == null || pS_Hint.IsDisposed))
            {
                pS_Hint = new PS_hint("평범한 그림책이다.. 123 P까지 있는 것으로 보인다.", 1); pS_Hint.Show();
            }
            else if (Obstacle_Name == "doc" && (pS_Hint == null || pS_Hint.IsDisposed))
            {
                pS_Hint = new PS_hint("의미를 모를 수들이 적혀있다..\n\n1 0 1 1 0 1", 1); pS_Hint.Show(this);
            }
            else if (Obstacle_Name == "picture" && (pS_Hint == null || pS_Hint.IsDisposed))
            {
                pS_Hint = new PS_hint("어린 아이가 그린 것만 같은 그림이다.. 1788년도에 그려진 듯 하다.", 1); pS_Hint.Show();
            }

            else if (Obstacle_Name == "smallMap" && (pS_Hint == null || pS_Hint.IsDisposed))
            {
                pS_Hint = new PS_hint("", 3); pS_Hint.Show();
            }
            else if (Obstacle_Name == "smallMap_by_other" && BaseForm_test.Me.Map == this.Map)
            {
                rope.Tag = "obstacle";
                rope.Show();
            }
            
            else if (Obstacle_Name == "barrel1" && (pS_Hint == null || pS_Hint.IsDisposed))
            {
                pS_Hint = new PS_hint("두 명의 아버지와 두 명의 아들이 한 명당 한 마리 씩 물고기를 낚았다. \n\n" +
                    "날이 저물어서 집으로 갈 때 잡은 물고기는 몇 마리인가?", 4);
                pS_Hint.Show();
            }
            else if (Obstacle_Name == "barrel1_by_other" && BaseForm_test.Me.Map == this.Map)
            {
                smallMap.Tag = "obstacle";
                smallMap.Show();
            }

            else if (Obstacle_Name == "rope")
            {
                BaseForm_test.Me.Info.ObstacleName = "rope_by_other";
                BaseForm_test.Me.Info.result = true;
                MainClient.SendData(BaseForm_test.Me.Info, PacketType.AboutNext, "127.0.0.1");

                Loading loading = new Loading(3);
                loading.Show();
                //PS_Final pS_Final = new PS_Final();
                //pS_Final.Show();
                this.timer.Stop();
                this.Hide();
            }
            else if (Obstacle_Name == "rope_by_other" && BaseForm_test.Me.Map == this.Map)
            {
                Loading loading = new Loading(3);
                loading.Show();
                // PS_Final pS_Final = new PS_Final();
                //pS_Final.Show();
                this.timer.Stop();
                this.Hide();
            }
        }
        private void PS_Second_Load(object sender, EventArgs e)
        {
            title = "Chap2. 선장실을 탈출하라";
            content = "감옥을 탈출하니 선장실로 오게 되었다.\n\n 선장이 오기 전에 잡동사니 사이에서 단서들을 찾자.\n\n" +
            "이 단서들을 조합하여 선장실을 탈출해야 한다.";
            PS_paper pS_Paper = new PS_paper(title, content, 2);
            pS_Paper.Show();
            smallMap.Hide();
            rope.Hide();
        }

    }
}
