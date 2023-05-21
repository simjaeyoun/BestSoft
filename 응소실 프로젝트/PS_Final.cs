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
using static 로그인화면.In_Game;

namespace 로그인화면
{
    public partial class PS_Final : Form
    {
        private int MoveStep = 8;

        private Image player;
        private int playerX = 50, playerY = 100;
        private int playerWidth = 50, playerHeight = 50;
        private int slowDownFrameRate = 0, steps = 0;
        private bool Go_Up, Go_Down, Go_Left, Go_Right;
        private bool Block_Up = true, Block_Down = true, Block_Left = true, Block_Right = true;
        private bool Space_Up = false, Space_Down = false, Space_Left = false, Space_Right = false;
        private string Ob_Name;
        private string title;
        private string content;
        private int boomX;
        private int boomY;
        private Random rand;
        public PS_Final()
        {
            InitializeComponent();
            player = In_Game.images[(int)clr, (int)Direction.Down];
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
        private void In_Game_KeyUp(object sender, KeyEventArgs e) // 키를 땠을 때 (그냥 서있는 이미지)
        {
            if (e.KeyCode == Keys.Up) { Go_Up = false; }
            else if (e.KeyCode == Keys.Down) { Go_Down = false; }
            else if (e.KeyCode == Keys.Left) { Go_Left = false; }
            else if (e.KeyCode == Keys.Right) { Go_Right = false; }
        }
        private void timer1_Tick(object sender, EventArgs e)
        {

            Collision_Detection_Up();
            Collision_Detection_Down();
            Collision_Detection_Left();
            Collision_Detection_Right();

            if (Go_Up && Block_Up && (playerY > 0))
            {
                playerY -= MoveStep;
                AnimatePlayer(0, 2);
                Block_Down = true;
                Block_Left = true;
                Block_Right = true;
            }
            else if (Go_Down && Block_Down && (playerY + playerHeight < this.ClientSize.Height))
            {
                playerY += MoveStep;
                AnimatePlayer(3, 5);
                Block_Up = true;
                Block_Left = true;
                Block_Right = true;
            }
            else if (Go_Left && Block_Left && (playerX > 0))
            {
                playerX -= MoveStep;
                AnimatePlayer(6, 8);
                Block_Up = true;
                Block_Down = true;
                Block_Right = true;
            }
            else if (Go_Right && Block_Right && (playerX + playerWidth < this.ClientSize.Width))
            {
                playerX += MoveStep;
                AnimatePlayer(9, 11);
                Block_Up = true;
                Block_Down = true;
                Block_Left = true;
            }

            this.Invalidate();
        }

        private void In_Game_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up) { Go_Up = true; }
            else if (e.KeyCode == Keys.Down) { Go_Down = true; }
            else if (e.KeyCode == Keys.Left) { Go_Left = true; }
            else if (e.KeyCode == Keys.Right) { Go_Right = true; }
            else if ((e.KeyCode == Keys.Space) && Space_Up || Space_Down || Space_Left || Space_Right)
            {
                Go_Up = false; Go_Down = false; Go_Left = false; Go_Right = false;
                if (Ob_Name == "box1")
                {
                    PS_hint pS_Hint = new PS_hint("B C D E I K O X ?", 1); pS_Hint.Show();
                }
                else if (Ob_Name == "box2")
                {
                    PS_hint pS_Hint = new PS_hint("16 + 4 = 8  8 + 1 = 9  12 + 15 = 3  7 + 9 = 4\n\n7 + 7 = ?", 1); pS_Hint.Show();
                }
                else if (Ob_Name== "box3")
                {
                    PS_hint pS_Hint = new PS_hint("어느 날 3명의 갑판원을 뽑기로 했다. 후보자는 7명이고 선원은 총 45명이다\n\n" +
                        "개표 도중, 후보자 중 1명이 확실하게 뽑혔다고 말할 수 있으려면\n\n몇 표가 나와야 하는가?", 1); pS_Hint.Show();
                }
                else if (Ob_Name == "forEscape")
                {
                    PS_hint pS_Hint = new PS_hint("최종 암호를 입력하세요", 5); pS_Hint.Show();
                }
                else { MessageBox.Show(Ob_Name); }
            }
        }

        private void Paint_Character(object sender, PaintEventArgs e)
        {
            Graphics Canvas = e.Graphics;
            Canvas.DrawImage(player, playerX, playerY, playerWidth, playerHeight);

        }
        private void Collision_Detection_Right()
        {
            foreach (Control control in this.Controls)
            {
                if (control is PictureBox && (string)control.Tag == "obstacle")
                {
                    /* Player Right 충돌검사 */
                    if (playerX < control.Location.X && playerX + playerWidth >= control.Location.X && playerY + playerHeight >= control.Location.Y + 10
                        && playerY <= control.Location.Y + control.Height - 20)
                    {
                        Block_Right = false;
                        Space_Right = true;
                        Ob_Name = control.Name; /* Player와 충돌된 Obstacle Name 저장 */
                        break;
                    }
                    else { Space_Right = false; }
                }
            }
        }
        private void Collision_Detection_Up()
        {
            foreach (Control control in this.Controls)
            {
                if (control is PictureBox && (string)control.Tag == "obstacle")
                {
                    /* Player Up 충돌검사 */
                    if (playerY > control.Location.Y && playerY <= control.Location.Y + control.Height - 10 && playerX + playerWidth >= control.Location.X + 20
                        && playerX <= control.Location.X + control.Width - 20)
                    {
                        Block_Up = false;
                        Space_Up = true;
                        Ob_Name = control.Name;
                        break;
                    }
                    else { Space_Up = false; }
                }
            }
        }

        private void Collision_Detection_Left()
        {
            foreach (Control control in this.Controls)
            {
                if (control is PictureBox && (string)control.Tag == "obstacle")
                {
                    /* Player Left 충돌검사 */
                    if (playerX + playerWidth > control.Location.X + control.Width && playerX <= control.Location.X + control.Width && playerY + playerHeight >= control.Location.Y + 10
                        && playerY <= control.Location.Y + control.Height - 20)
                    {
                        Block_Left = false;
                        Space_Left = true;
                        Ob_Name = control.Name;
                        break;
                    }
                    else { Space_Left = false; }

                }
            }
        }

        private void Collision_Detection_Down()
        {
            foreach (Control control in this.Controls)
            {
                if (control is PictureBox && (string)control.Tag == "obstacle")
                {
                    /* Player Down 충돌검사 */
                    if (playerY < control.Location.Y && playerY + playerHeight >= control.Location.Y && playerX + playerWidth >= control.Location.X + 20
                        && playerX <= control.Location.X + control.Width - 20)
                    {
                        Block_Down = false;
                        Space_Down = true;
                        Ob_Name = control.Name;
                        break;
                    }
                    else { Space_Down = false; }

                }
            }
        }

        private void AnimatePlayer(int start, int end)
        {
            slowDownFrameRate++;
            /* player animation 구현 -> 4번 호출 될 때마다 이미지 변경 */
            if (slowDownFrameRate == 4)
            {
                steps++;
                slowDownFrameRate = 0;
            }

            /* 정지 -> 왼발 -> 오른발 -> 정지 .... 순서 */
            if ((steps > end) || (steps < start))
            {
                steps = start;
            }
            player = In_Game.images[(int)clr, steps];
        }

    }
}
