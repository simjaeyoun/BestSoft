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
        public enum Direction // 방향 열거
        {
            Up,
            Down,
            Left,
            Right,
            Up_Walk,
            Down_Walk,
            Left_Walk,
            Right_Walk
        }
        public enum Ch_Color // 캐릭터 Color 열거
        {
            Black,
            Orange,
            Green
        }

        private Direction direction; // 방향
        private const int MoveStep = 7; // 스피드
        private int x, y;
        private bool Go_Up = true, Go_Down = true, Go_Left = true, Go_Right = true;
        public static Ch_Color clr;
        public static Image[,] images = new Image[3,8]; // images[clr][direction] -> Player의 [색깔][방향] 이미지 2차원 배열

        public In_Game()
        {
            InitializeComponent();
            color();      // Color 선택 폼 불러오기
            LoadImages(); // image file 불러오기
            player.Image = images[(int)clr,(int)Direction.Down];
            x = player.Location.X;
            y = player.Location.Y;
        }

        private void color()
        {
            Select_Color select = new Select_Color();
            select.Owner = this;
            DialogResult Result = select.ShowDialog();
            if (Result == DialogResult.OK)
            {
                MessageBox.Show("Game Start");
            }
        }

        private void timer1_Tick(object sender, EventArgs e) // Player 움직임 애니메이션 구현 
        {
        }

        private void In_Game_KeyUp(object sender, KeyEventArgs e) // 키를 땠을 때 (그냥 서있는 이미지)
        {
            try
            {
                player.Image = images[(int)clr, (int)direction];
            }
            catch (Exception q) 
            {
                MessageBox.Show("error" + q.Message);
            }

        }

        private void LoadImages() // 기본 이미지 불러오기
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

                }
                catch (Exception e)
                {
                    MessageBox.Show("error" + e.Message);
                }

            }
        }

        public void SetDirection(Direction newDirection) // Key 입력 시 방향 설정 & Player Image 변경 (움직이는 이미지) 
        {
            direction = newDirection;
            try
            {
                    player.Image = images[(int)clr, (int)direction + 4];
            }
            catch (Exception e)
            {
                MessageBox.Show("error" + e.Message);
            }
        }

        private void In_Game_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Up:
                    if (Go_Up)
                        y -= MoveStep; SetDirection(Direction.Up);
                    break;
                case Keys.Down:
                    if (Go_Down)
                        y += MoveStep; SetDirection(Direction.Down);
                    break;
                case Keys.Left:
                    if (Go_Left)
                        x -= MoveStep; SetDirection(Direction.Left);
                    break;
                case Keys.Right:
                    if (Go_Right)
                        x += MoveStep; SetDirection(Direction.Right);
                    break;
            }
            if (x >= 0 && x + player.Width <= ClientSize.Width && // form 화면을 벗어나지 않게 함
                y >= 0 && y + player.Height <= ClientSize.Height)
            {
                player.Left = x;
                player.Top = y;
            }
            //player.Location = new Point(x, y);
            Collision_Detection(e);
        }
        private void Collision_Detection(KeyEventArgs e) // 충돌검사
        {
            foreach (Control control in this.Controls)
            {
                if (control is PictureBox && (string)control.Tag == "obstacle")
                {
                    if (player.Bounds.IntersectsWith(control.Bounds))
                    {
                        control.BackColor = Color.White;

                        if (direction == Direction.Up) { Go_Up = false; }

                        else if (direction == Direction.Down) { Go_Down = false; }

                        else if (direction == Direction.Left) { Go_Left = false; }

                        else if (direction == Direction.Right) { Go_Right = false; }

                        if (e.KeyCode == Keys.Space) //Space 입력 시
                        {
                            if ((string)control.Name == "start")
                            {
                                First f = new First();
                                f.Show();
                                this.Hide();
                            }
                            else
                                MessageBox.Show("Space");
                        }
                        break;
                    }
                    else
                    {
                        Go_Up = true;
                        Go_Down = true;
                        Go_Left = true;
                        Go_Right = true;
                        control.BackColor = Color.Transparent;
                    }
                }
            }

        }
    }
}


