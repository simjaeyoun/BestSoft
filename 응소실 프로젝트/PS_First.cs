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
    public partial class PS_First : Form
    {

        private int MoveStep = 8;

        private Image player;
        private int playerX = 140, playerY = 140;
        private int playerWidth = 50, playerHeight = 50;
        private int slowDownFrameRate = 0, steps = 0;
        private bool Go_Up, Go_Down, Go_Left, Go_Right;
        private bool Block_Up = true, Block_Down = true, Block_Left = true, Block_Right = true;
        private bool Space_Up = false, Space_Down = false, Space_Left = false, Space_Right = false;
        private string Ob_Name;
        private Image backupImage;
        private string title;
        private string content;
        public bool checkBright = false;

        public PS_First()
        {
            InitializeComponent();
            player = In_Game.images[(int)clr, (int)Direction.Down];
            backupImage = this.BackgroundImage;
            DarkerBrightness(backupImage, 0.5f);
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
                if (Ob_Name == "match")
                {
                    title = "\"이들 중 한 명이 거짓말을 한다.\n\n누가 선장의 럼주를 훔쳐 마셨는가?\"";
                    content = "잭슨 : 콜린이 마셨어!\n\n콜린 : 조르딕이 마셨어!\n\n조르딕 : 나는 자고 있었어!\n\n" +
                        "닉슨 : 내가 안 마셨어!\n\n마치 : 나는 마시는 걸 구경만 했어!\n\n미겔 : 콜린이 거짓말 했어!";
                    PS_paper pS_Paper = new PS_paper(title, content, 3);
                    if (checkBright)
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
                    else
                        pS_Paper.Show();
                }
                else if (Ob_Name == "forRope")
                {
                    PS_hint pS_Hint = new PS_hint("암호를 입력하세요", 2); pS_Hint.Show();
                }
                else if (Ob_Name == "rope")
                {
                    PS_Second pS_Second = new PS_Second();
                    this.Close();
                    pS_Second.Show();
                }
                else if (Ob_Name == "hint1")
                {
                    PS_hint pS_Hint = new PS_hint("오후 5시 맞i한 태양을 보아라.. 그러f면 태양이 지고 달t이 나타e날 테니..", 1); pS_Hint.Show();
                }
                else if (Ob_Name == "hint2")
                {
                    PS_hint pS_Hint = new PS_hint("오후 8시 달을 보니 적적하여 눈물을 흘렸다", 1); pS_Hint.Show();
                }
                else if (Ob_Name == "hint3")
                {
                    PS_hint pS_Hint = new PS_hint("오전 10시 태양과 함께하는 브런치..", 1); pS_Hint.Show();
                }
                else if (Ob_Name == "hint4")
                {
                    PS_hint pS_Hint = new PS_hint("오후 9시 창문에 달e이 보이r니..", 1); pS_Hint.Show();
                }
                else if (Ob_Name == "hint5")
                {
                    PS_hint pS_Hint = new PS_hint("오후 7시 맞이n한 달을 보t고", 1); pS_Hint.Show();
                }
                else if (Ob_Name == "hint6")
                {
                    PS_hint pS_Hint = new PS_hint("오후 1시 태양이 뜨는 날 우리s는 그것h을 맞이 할 것이며..", 1); pS_Hint.Show();
                }
                else if (Ob_Name == "hint7")
                {
                    PS_hint pS_Hint = new PS_hint("오후 10시 달에는 눈물이 없다..", 1); pS_Hint.Show();
                }
                else if (Ob_Name == "hint8")
                {
                    PS_hint pS_Hint = new PS_hint("오후 2시 창문에 비친 태양이 우리를 감싼다", 1); pS_Hint.Show();
                }   
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

