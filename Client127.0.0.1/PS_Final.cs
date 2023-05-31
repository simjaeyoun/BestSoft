using OpenQA.Selenium.DevTools.V110.Animation;
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
    public partial class PS_Final : BaseForm_test
    {
        private string title;
        private string content;
        private int boomX;
        private int boomY;
        private Random rand;
        private PS_hint pS_Hint;
        public PS_Final()
        {
            InitializeComponent();
            timer.Start();
            this.Map = MapType.PS_Final;
            BaseForm_test.Me.Map = MapType.PS_Final;
        }
        protected override void Space_Event(string Obstacle_Name)
        {
            if (Obstacle_Name == "box1" && (pS_Hint == null || pS_Hint.IsDisposed))
            {
                PS_hint pS_Hint = new PS_hint("B C D E I K O X ?", 1); pS_Hint.Show();
            }
            else if (Obstacle_Name == "box2" && (pS_Hint == null || pS_Hint.IsDisposed))
            {
                PS_hint pS_Hint = new PS_hint("16 + 4 = 8  8 + 1 = 9  12 + 15 = 3  7 + 9 = 4\n\n7 + 7 = ?", 1); pS_Hint.Show();
            }
            else if (Obstacle_Name == "box3" && (pS_Hint == null || pS_Hint.IsDisposed))
            {
                PS_hint pS_Hint = new PS_hint("어느 날 3명의 갑판원을 뽑기로 했다. 후보자는 7명이고 선원은 총 45명이다\n\n" +
                    "개표 도중, 후보자 중 1명이 확실하게 뽑혔다고 말할 수 있으려면\n\n몇 표가 나와야 하는가?", 1); pS_Hint.Show();
            }
            else if (Obstacle_Name == "forEscape" && (pS_Hint == null || pS_Hint.IsDisposed))
            {
                PS_hint pS_Hint = new PS_hint("최종 암호를 입력하세요", 5); pS_Hint.Show();
            }else if(Obstacle_Name == "forEscape_by_other" && BaseForm_test.Me.Map == this.Map)
            {
                this.timer.Stop();
                timer2.Start();
                MessageBox.Show("방탈출에 성공했습니다! 1");

                //Application.Exit();
            }
        }

        private void PS_Final_Load(object sender, EventArgs e)
        {
            title = "Chap3. 해적선을 탈출하라";
            content = "마지막 관문까지 도착했다.\n\n이 상자 안에 들어있는 문제들을 풀자\n\n" +
            "끝까지 무사히 탈출해야 한다!!";
            PS_paper pS_Paper = new PS_paper(title, content, 2);
            pS_Paper.Show();
            timer2.Interval = 2000;
            timer2.Stop();
            DateTime currentTime = DateTime.Now;
            rand = new Random(currentTime.Millisecond);
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            int size = rand.Next(10, 50);
            Graphics g = this.CreateGraphics();
            Color color = Color.FromArgb(rand.Next(256), rand.Next(256), rand.Next(256));
            Pen pen = new Pen(color);
            for (int i = 0; i < 100; i++)
            {
                boomX = rand.Next(1, this.Width);
                boomY = rand.Next(1, this.Height);
                g.DrawLine(pen, boomX, boomY, size, size);
            }
        }

    }
}
