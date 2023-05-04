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
            Down,
            Left,
            Right
        }

        private Direction direction; // 방향
        private int MoveStep = 3; // 스피드
        private int x, y;
        private Image upImage;
        private Image downImage;
        private Image leftImage;
        private Image rightImage;
        private bool Go_Up = true, Go_Down = true, Go_Left = true, Go_Right = true, isMove = false;
        public int clr = 1; // black=1, orange=2, green=3

        public In_Game()
        {
            InitializeComponent();
            color();
            LoadImages(); // image file 불러오기
            player.Image = downImage;
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

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (isMove)
            {
                player.Image = Properties.Resources.a_Player_Right_3;
            }
        }

        private void In_Game_KeyUp(object sender, KeyEventArgs e) // 그냥 서있는 이미지
        {
            isMove = false;
            try
            {
                if (direction == Direction.Up)
                    player.Image = upImage;
                if (direction == Direction.Down)
                    player.Image = downImage;
                if (direction == Direction.Left)
                    player.Image = leftImage;
                if (direction == Direction.Right)
                    player.Image = rightImage;
            }
            catch (Exception q) // File이 경로에 없을 때
            {
                MessageBox.Show("error" + q.Message);
            }

        }

        private void LoadImages()
        {
            if (clr == 1) //black
            {
                try
                {
                    upImage = Properties.Resources.a_Player_Up;
                    downImage = Properties.Resources.a_Player_Down;
                    leftImage = Properties.Resources.a_Player_Left;
                    rightImage = Properties.Resources.a_Player_Right;

                }
                catch (Exception e)
                {
                    MessageBox.Show("error" + e.Message);
                }
            }
            if (clr == 2) // orange
            {
                try
                {
                    upImage = Properties.Resources.Orange_Player_Up;
                    downImage = Properties.Resources.Orange_Player_Down;
                    leftImage = Properties.Resources.Orange_Player_Left;
                    rightImage = Properties.Resources.Orange_Player_Right;

                }
                catch (Exception e)
                {
                    MessageBox.Show("error" + e.Message);
                }

            }
        }

        public void SetDirection(Direction newDirection) // Key 입력시 방향 설정 & Player Image 변경 (움직이는 이미지) 
        {
            direction = newDirection;
            try
            {
                if (newDirection == Direction.Up)
                    player.Image = Properties.Resources.a_Player_Up_2;
                if (newDirection == Direction.Down)
                    player.Image = Properties.Resources.a_Player_Down_2;
                if (newDirection == Direction.Left)
                    player.Image = Properties.Resources.a_Player_Left_2;
                if (newDirection == Direction.Right)
                    player.Image = Properties.Resources.a_Player_Right_2;
            }
            catch (Exception e) // File이 경로에 없을 때
            {
                MessageBox.Show("error" + e.Message);
            }
        }

        private void In_Game_KeyDown(object sender, KeyEventArgs e)
        {
            isMove = true;
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
            Collision_Detection(e);
        }
        private void Collision_Detection(KeyEventArgs e)
        {
            foreach (Control control in this.Controls) // obstacle 만났을 때
            {
                if (control is PictureBox && (string)control.Tag == "obstacle")
                {
                    if (player.Bounds.IntersectsWith(control.Bounds))
                    {
                        control.BackColor = Color.White;

                        if (direction == Direction.Up)
                            Go_Up = false;

                        else if (direction == Direction.Down)
                            Go_Down = false;

                        else if (direction == Direction.Left)
                            Go_Left = false;

                        else if (direction == Direction.Right)
                            Go_Right = false;
                    }
                    else
                    {
                        Go_Up = true;
                        Go_Down = true;
                        Go_Left = true;
                        Go_Right = true;
                        control.BackColor = Color.Black;
                    }

                }
            }



        }


    }
}
