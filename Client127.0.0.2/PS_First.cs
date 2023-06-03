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
    public partial class PS_First : BaseForm_test
    {
        private Image backupImage;
        private string title;
        private string content;
        public bool checkBright = false;
        private PS_paper pS_Paper;
        private PS_hint pS_Hint;

        public PS_First()
        {
            InitializeComponent();
            backupImage = this.BackgroundImage;
            DarkerBrightness(backupImage, 0.5f);
            this.Map = MapType.PS_First;
            BaseForm_test.Me.Map = MapType.PS_First;
            Invalidate();
            timer.Start();

        }

        private void DarkerBrightness(Image image, float brightness)
        {
            Bitmap bitmap = new Bitmap(image);

            for (int x = 0; x < bitmap.Width; x++)
            {
                for (int y = 0; y < bitmap.Height; y++)
                {
                    Color pixel = bitmap.GetPixel(x, y);

                    int R = (int)(pixel.R * brightness);
                    int G = (int)(pixel.G * brightness);
                    int B = (int)(pixel.B * brightness);

                    R = Math.Max(Math.Min(R, 255), 0);
                    G = Math.Max(Math.Min(G, 255), 0);
                    B = Math.Max(Math.Min(B, 255), 0);

                    Color myPixel = Color.FromArgb(R, G, B);
                    bitmap.SetPixel(x, y, myPixel);
                }
            }
            this.BackgroundImage = bitmap;
        }

        private void Bright()
        {
            this.BackgroundImage = backupImage;
            label1.Show();
            foreach (Control control in this.Controls)
            {
                if (control is PictureBox && (string)control.Tag == "hint")
                {
                    control.Show();
                    control.Tag = "obstacle";
                }
            }
        }

        protected override void Space_Event(string Obstacle_Name)
        {
            if (Obstacle_Name == "match")
            {
                title = "\"이들 중 한 명이 거짓말을 한다.\n\n누가 선장의 럼주를 훔쳐 마셨는가?\"";
                content = "잭슨 : 콜린이 마셨어!\n\n콜린 : 조르딕이 마셨어!\n\n조르딕 : 나는 자고 있었어!\n\n" +
                    "닉슨 : 내가 안 마셨어!\n\n마치 : 나는 마시는 걸 구경만 했어!\n\n미겔 : 콜린이 거짓말 했어!";

                if (pS_Paper == null || pS_Paper.IsDisposed)
                {
                    pS_Paper = new PS_paper(title, content, 3);
                    pS_Paper.Show();
                }
            }

            else if (Obstacle_Name == "On_match")
            {
                Bright();
                BaseForm_test.Me.Info.ObstacleName = Obstacle_Name + "_by_other";
                BaseForm_test.Me.Info.result = true;
                MainClient.SendData(BaseForm_test.Me.Info, PacketType.AboutNext, "127.0.0.2");

            }
            else if (Obstacle_Name == "On_match_by_other" && BaseForm_test.Me.Map == this.Map)
            {
                Bright();
                match.Name = "On_match";
            }

            else if (Obstacle_Name == "forRope" && (pS_Hint == null || pS_Hint.IsDisposed))
            {
                pS_Hint = new PS_hint("암호를 입력하세요", 2); pS_Hint.Show();
            }
            else if (Obstacle_Name == "forRope_by_other" && BaseForm_test.Me.Map == this.Map)
            {
                forRope.Tag = "not_obstacle";
                rope.Tag = "obstacle";
                rope.Show();
            }

            else if (Obstacle_Name == "rope" && (pS_Hint == null || pS_Hint.IsDisposed))
            {
                BaseForm_test.Me.Info.ObstacleName = Obstacle_Name + "_by_other";
                BaseForm_test.Me.Info.result = true;
                MainClient.SendData(BaseForm_test.Me.Info, PacketType.AboutNext, "127.0.0.2");

                Loading loading = new Loading(2);
                loading.Show();
                //PS_Second pS_Second = new PS_Second();
                //pS_Second.Show();
                this.timer.Stop();
                this.Hide();
            }
            else if (Obstacle_Name == "rope_by_other" && BaseForm_test.Me.Map == this.Map)
            {
                Loading loading = new Loading(2);
                loading.Show();
                //PS_Second pS_Second = new PS_Second();
                //pS_Second.Show();
                this.timer.Stop();
                this.Hide();
            }
            else if (Obstacle_Name == "hint1" && (pS_Hint == null || pS_Hint.IsDisposed))
            {
                pS_Hint = new PS_hint("오후 5시 맞i한 태양을 보아라.. 그러f면 태양이 지고 달t이 나타e날 테니..", 1); pS_Hint.Show();
            }
            else if (Obstacle_Name == "hint2" && (pS_Hint == null || pS_Hint.IsDisposed))
            {
                pS_Hint = new PS_hint("오후 8시 달을 보니 적적하여 눈물을 흘렸다", 1); pS_Hint.Show();
            }
            else if (Obstacle_Name == "hint3" && (pS_Hint == null || pS_Hint.IsDisposed))
            {
                pS_Hint = new PS_hint("오전 10시 태양과 함께하는 브런치..", 1); pS_Hint.Show();
            }
            else if (Obstacle_Name == "hint4" && (pS_Hint == null || pS_Hint.IsDisposed))
            {
                pS_Hint = new PS_hint("오후 9시 창문에 달e이 보이r니..", 1); pS_Hint.Show();
            }
            else if (Obstacle_Name == "hint5" && (pS_Hint == null || pS_Hint.IsDisposed))
            {
                pS_Hint = new PS_hint("오후 7시 맞이n한 달을 보t고", 1); pS_Hint.Show();
            }
            else if (Obstacle_Name == "hint6" && (pS_Hint == null || pS_Hint.IsDisposed))
            {
                pS_Hint = new PS_hint("오후 1시 태양이 뜨는 날 우리s는 그것h을 맞이 할 것이며..", 1); pS_Hint.Show();
            }
            else if (Obstacle_Name == "hint7" && (pS_Hint == null || pS_Hint.IsDisposed))
            {
                pS_Hint = new PS_hint("오후 10시 달에는 눈물이 없다..", 1); pS_Hint.Show();
            }
            else if (Obstacle_Name == "hint8" && (pS_Hint == null || pS_Hint.IsDisposed))
            {
                pS_Hint = new PS_hint("오후 2시 창문에 비친 태양이 우리를 감싼다", 1); pS_Hint.Show();
            }
        }

        private void PS_First_Load(object sender, EventArgs e)
        {
            title = "Chap1. 감옥을 탈출하라";
            content = "이곳은 배 안의 감옥이다.\n\n 축축한 배 안의 감옥에서 의문의 이가 남긴 단서들을 찾자.\n\n" +
            "이 단서들을 조합하여 감옥을 탈출해야 한다.\n\n 어두운 감옥, 일단 불부터 켜야겠다.";
            PS_paper pS_Paper = new PS_paper(title, content, 2);
            pS_Paper.Show();
            label1.Hide();
            rope.Hide();
            foreach (Control control in this.Controls)
            {
                if (control is PictureBox && (string)control.Tag == "hint")
                    control.Hide();
            }

        }
    }
}

