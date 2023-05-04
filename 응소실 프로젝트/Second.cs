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
    public partial class Second : Form
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

        public enum ch_Color // 캐릭터 Color 열거
        {
            Black,
            Orange,
            Green
        }

        private Direction direction; // 방향
        private int MoveStep = 7; // 스피드
        private int x, y;
        private bool Go_Up = true, Go_Down = true, Go_Left = true, Go_Right = true;
        public ch_Color clr;
        private Image[,] images = new Image[3, 8];
        public Second()
        {
            InitializeComponent();
            LoadImages(); // image file 불러오기
            player.Image = images[(int)clr, (int)Direction.Down];
            x = player.Location.X;
            y = player.Location.Y;
        }

        private void LoadImages()
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
            player.Location = new Point(x, y);
            Check_Question(e);
        }

        private void Check_Question(KeyEventArgs e)
        {
            foreach (Control control in this.Controls)
            {
                if (player.Bounds.IntersectsWith(control.Bounds))
                {
                    if (e.KeyCode == Keys.Space)
                    {
                        if ((string)control.Tag == "door")
                        {
                            Second_passwd Sp = new Second_passwd();
                            Sp.ShowDialog();
                        }
                    }
                }

            }
        }
    }
}
