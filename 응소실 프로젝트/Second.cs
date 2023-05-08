using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static 로그인화면.In_Game;

namespace 로그인화면
{
    public partial class Second : Form
    {
        private Direction direction; // 방향
        private int MoveStep = 7; // 스피드
        private int x, y;
        private bool Go_Up = true, Go_Down = true, Go_Left = true, Go_Right = true;

        public Second()
        {
            InitializeComponent();
            player.Image = In_Game.images[(int)In_Game.clr, (int)Direction.Down];
            x = player.Location.X;
            y = player.Location.Y;
        }


        private void In_Game_KeyUp(object sender, KeyEventArgs e) // 키를 땠을 때 (그냥 서있는 이미지)
        {
            try
            {
                player.Image = In_Game.images[(int)In_Game.clr, (int)direction];
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
                player.Image = In_Game.images[(int)In_Game.clr, (int)direction + 4];
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
                if (control is PictureBox && (string)control.Tag == "obstacle")
                {
                    if (player.Bounds.IntersectsWith(control.Bounds))
                    {
                        control.BackColor = Color.GhostWhite;

                        if (direction == Direction.Up) { Go_Up = false; }

                        else if (direction == Direction.Down) { Go_Down = false; }

                        else if (direction == Direction.Left) { Go_Left = false; }

                        else if (direction == Direction.Right) { Go_Right = false; }

                        if (e.KeyCode == Keys.Space)
                        {
                            if ((string)control.Name == "door")
                            {
                                Second_passwd Sp = new Second_passwd();
                                Sp.ShowDialog();
                            }
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
