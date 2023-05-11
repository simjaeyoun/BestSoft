using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;

namespace 로그인화면
{
    public partial class In_Game : Form
    {
        public enum Direction 
        {
            Up,
            Up_Walk,
            Up_Walk2,
            Down,
            Down_Walk,
            Down_Walk2,
            Left,
            Left_Walk,
            Left_Walk2,
            Right,
            Right_Walk,
            Right_Walk2
        }
        public enum Ch_Color 
        {
            Black,
            Orange,
            Green
        }

        private Direction direction;
        private int MoveStep=4;

        private Image player;
        private int playerX=0, playerY=0;
        private int playerWidth = 50, playerHeight = 50;
        private int slowDownFrameRate = 0, steps = 0;
        private bool Go_Up, Go_Down, Go_Left, Go_Right;
        private bool Block_Up=true, Block_Down=true, Block_Left=true, Block_Right=true;
        private bool Space_Up = false, Space_Down = false, Space_Left = false, Space_Right = false;
        private string Ob_Name;

        public static Ch_Color clr;
        public static Image[,] images = new Image[3,12]; /* images[clr][direction] -> Player의 [색깔][방향] 이미지 2차원 배열 */


        public In_Game()
        {
            InitializeComponent();
            color();      /* Color 선택 폼 불러오기 */
            LoadImages(); /* image file 불러오기 */
            player = images[(int)clr, (int)Direction.Down];
        }

        private void color()
        {
            Select_Color select = new Select_Color();
            select.Owner = this;
            DialogResult Result = select.ShowDialog();
        }


        private void In_Game_KeyUp(object sender, KeyEventArgs e) 
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

        private void Paint_Character(object sender, PaintEventArgs e)
        {
            Graphics Canvas = e.Graphics;
            Canvas.DrawImage(player, playerX, playerY, playerWidth, playerHeight);
            
        }

        
        private void LoadImages() /* 이미지 불러오기 */
        {
            if (clr == Ch_Color.Black) //black
            {
                try
                {
                    images[(int)clr,(int)Direction.Up] = Properties.Resources.a_Player_Up;
                    images[(int)clr, (int)Direction.Down] = Properties.Resources.a_Player_Down;
                    images[(int)clr, (int)Direction.Left] = Properties.Resources.a_Player_Left;
                    images[(int)clr, (int)Direction.Right] = Properties.Resources.a_Player_Right;
                    images[(int)clr, (int)Direction.Up_Walk] = Properties.Resources.a_Player_Up_Walk1;
                    images[(int)clr, (int)Direction.Down_Walk] = Properties.Resources.a_Player_Down_Walk1;
                    images[(int)clr, (int)Direction.Left_Walk] = Properties.Resources.a_Player_Left_Walk1;
                    images[(int)clr, (int)Direction.Right_Walk] = Properties.Resources.a_Player_Right_Walk1;
                    images[(int)clr, (int)Direction.Up_Walk2] = Properties.Resources.a_Player_Up_Walk2;
                    images[(int)clr, (int)Direction.Down_Walk2] = Properties.Resources.a_Player_Down_Walk2;
                    images[(int)clr, (int)Direction.Left_Walk2] = Properties.Resources.a_Player_Left_Walk2;
                    images[(int)clr, (int)Direction.Right_Walk2] = Properties.Resources.a_Player_Right_Walk2;

                }
                catch (Exception e)
                {
                    MessageBox.Show("error" + e.Message);
                }
            }
            if (clr == Ch_Color.Orange) // orange
            {
                try
                {
                    images[(int)clr, (int)Direction.Up] = Properties.Resources.a_Player_Up;
                    images[(int)clr, (int)Direction.Down] = Properties.Resources.a_Player_Down;
                    images[(int)clr, (int)Direction.Left] = Properties.Resources.a_Player_Left;
                    images[(int)clr, (int)Direction.Right] = Properties.Resources.a_Player_Right;
                    images[(int)clr, (int)Direction.Up_Walk] = Properties.Resources.a_Player_Up_Walk1;
                    images[(int)clr, (int)Direction.Down_Walk] = Properties.Resources.a_Player_Down_Walk1;
                    images[(int)clr, (int)Direction.Left_Walk] = Properties.Resources.a_Player_Left_Walk1;
                    images[(int)clr, (int)Direction.Right_Walk] = Properties.Resources.a_Player_Right_Walk1;
                    images[(int)clr, (int)Direction.Up_Walk2] = Properties.Resources.a_Player_Up_Walk2;
                    images[(int)clr, (int)Direction.Down_Walk2] = Properties.Resources.a_Player_Down_Walk2;
                    images[(int)clr, (int)Direction.Left_Walk2] = Properties.Resources.a_Player_Left_Walk2;
                    images[(int)clr, (int)Direction.Right_Walk2] = Properties.Resources.a_Player_Right_Walk2;

                }
                catch (Exception e)
                {
                    MessageBox.Show("error" + e.Message);
                }

            }
            if (clr == Ch_Color.Green) // green
            {
                try
                {
                    images[(int)clr, (int)Direction.Up] = Properties.Resources.a_Player_Up;
                    images[(int)clr, (int)Direction.Down] = Properties.Resources.a_Player_Down;
                    images[(int)clr, (int)Direction.Left] = Properties.Resources.a_Player_Left;
                    images[(int)clr, (int)Direction.Right] = Properties.Resources.a_Player_Right;
                    images[(int)clr, (int)Direction.Up_Walk] = Properties.Resources.a_Player_Up_Walk1;
                    images[(int)clr, (int)Direction.Down_Walk] = Properties.Resources.a_Player_Down_Walk1;
                    images[(int)clr, (int)Direction.Left_Walk] = Properties.Resources.a_Player_Left_Walk1;
                    images[(int)clr, (int)Direction.Right_Walk] = Properties.Resources.a_Player_Right_Walk1;
                    images[(int)clr, (int)Direction.Up_Walk2] = Properties.Resources.a_Player_Up_Walk2;
                    images[(int)clr, (int)Direction.Down_Walk2] = Properties.Resources.a_Player_Down_Walk2;
                    images[(int)clr, (int)Direction.Left_Walk2] = Properties.Resources.a_Player_Left_Walk2;
                    images[(int)clr, (int)Direction.Right_Walk2] = Properties.Resources.a_Player_Right_Walk2;

                }
                catch (Exception e)
                {
                    MessageBox.Show("error" + e.Message);
                }

            }
        }

        
        private void In_Game_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up) { Go_Up = true; }
            else if (e.KeyCode == Keys.Down) { Go_Down = true; }
            else if (e.KeyCode == Keys.Left) { Go_Left = true; }
            else if (e.KeyCode == Keys.Right) { Go_Right = true; }
            else if((e.KeyCode == Keys.Space) && Space_Up || Space_Down || Space_Left || Space_Right) 
            {
                Go_Up = false; Go_Down = false; Go_Left = false; Go_Right = false;
                if (Ob_Name == "start")
                {
                    /* 게임 시작 */
                    First first = new First();
                    first.Show();
                    this.Hide();
                }
                else { MessageBox.Show(Ob_Name); }
            }
        }


        private void Collision_Detection_Right() 
        {
            foreach(Control control in this.Controls)
            {
                if(control is PictureBox && (string)control.Tag == "obstacle")
                {
                    /* Player Right 충돌검사 */
                    if (playerX < control.Location.X && playerX+playerWidth >= control.Location.X && playerY + playerHeight >= control.Location.Y + 10
                        && playerY <= control.Location.Y + control.Height - 20)
                    {
                        Block_Right = false;
                        Space_Right = true;
                        Ob_Name = control.Name; /* Player와 충돌된 Obstacle Name 저장 */
                        break;
                    }else { Space_Right = false; }
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
                    if (playerY < control.Location.Y && playerY + playerHeight >= control.Location.Y  && playerX + playerWidth >= control.Location.X + 20
                        && playerX <= control.Location.X + control.Width - 20 )
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
            player = images[(int)clr, steps];
        }
    }
}


