using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 로그인화면
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
        Green,
        UnKnown
    }
    public class Move_Key
    {
        public bool Go_Up { get; set; }
        public bool Go_Down { get; set; }
        public bool Go_Left { get; set; }
        public bool Go_Right { get; set; }
    }



    public class BaseForm : Form
    {
        protected System.Windows.Forms.Timer timer;

        protected bool Space_Up = false, Space_Down = false, Space_Left = false, Space_Right = false;
        protected string Ob_Name;

        protected bool lock1 = true, lock2 = true, lock3 = true;
        protected bool keydown_lock = false;
        protected Move_Key key = new Move_Key { Go_Up = false, Go_Down = false, Go_Left = false, Go_Right = false };
        protected bool keyup_lock = false;

        protected StudentData StudentData;
        protected Player Other_player;

        protected const int MoveStep = 4;
        public PictureBox pictureBox;

        public static Image[,] images = new Image[3, 12]; /* images[clr][direction] -> Player의 [색깔][방향] 이미지 2차원 배열 */
        public static Ch_Color clr;
        public static Player Me;

        public BaseForm()
        {
            this.DoubleBuffered = true;
            timer = new System.Windows.Forms.Timer();
            timer.Interval = 14;
            timer.Enabled = false;
            timer.Tick += BaseForm_Timer_Tick;

            this.KeyDown += BaseForm_KeyDown;
            this.KeyUp += BaseForm_KeyUp;
            this.Paint += BaseForm_Paint;
            this.FormClosed += BaseForm_Closing;

            //pictureBox = new PictureBox();
            //pictureBox.Location = new Point(Me.location.X, Me.location.Y);
            //pictureBox.Width = Me.Width + 20; pictureBox.Height = Me.Height + 20;
            //pictureBox.BackColor = Color.Transparent;
            //pictureBox.Parent = this;
            //pictureBox.Click += BaseForm_Click;

        }

        protected virtual void BaseForm_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Name : " + StudentManager.StudentDic["127.0.0.2"].StudentName);
        }

        private void BaseForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up) { Me.Go_Up = true; key.Go_Up = true; key.Go_Down = false; key.Go_Left = false; key.Go_Right = false; SendKeyDown(); }
            else if (e.KeyCode == Keys.Down) { Me.Go_Down = true; key.Go_Up = false; key.Go_Down = true; key.Go_Left = false; key.Go_Right = false; SendKeyDown(); }
            else if (e.KeyCode == Keys.Left) { Me.Go_Left = true; key.Go_Up = false; key.Go_Down = false; key.Go_Left = true; key.Go_Right = false; SendKeyDown(); }
            else if (e.KeyCode == Keys.Right) { Me.Go_Right = true; key.Go_Up = false; key.Go_Down = false; key.Go_Left = false; key.Go_Right = true; SendKeyDown(); }
            
            else if ((e.KeyCode == Keys.Space) && Space_Up || Space_Down || Space_Left || Space_Right)
            {
                Me.Go_Up = false; Me.Go_Down = false; Me.Go_Left = false; Me.Go_Right = false;
                Space_Event(Ob_Name);
            }
        }

        protected virtual void Space_Event(string Obstacle_Name)
        {
            /* Obstacle Name에 따른 이벤트 작성 */
        }

        private void BaseForm_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up) { Me.Go_Up = false; key.Go_Up = false; SendKeyUp(); SendLocation(); }
            else if (e.KeyCode == Keys.Down) { Me.Go_Down = false; key.Go_Down = false; SendKeyUp(); SendLocation(); }
            else if (e.KeyCode == Keys.Left) { Me.Go_Left = false; key.Go_Left = false; SendKeyUp(); SendLocation(); }
            else if (e.KeyCode == Keys.Right) { Me.Go_Right = false; key.Go_Right = false; SendKeyUp(); SendLocation(); }
        }
        private void BaseForm_Paint(object sender, PaintEventArgs e)
        {
            Graphics Canvas = e.Graphics;
            if (Me != null)
            {
                Canvas.DrawImage(Me.image, Me.location.X, Me.location.Y, Me.Width, Me.Height);
            }
            if (lock1 == false)
            {
                if (Other_player.image != null)
                {
                        Canvas.DrawImage(Other_player.image, Other_player.location.X, Other_player.location.Y, Other_player.Width, Other_player.Height);
                }
            }
        }
        private void BaseForm_Timer_Tick(object sender, EventArgs e)
        {
            Check_Other();

            Collision_Detection_Up();
            Collision_Detection_Down();
            Collision_Detection_Left();
            Collision_Detection_Right();

            My_Move();

            Other_Move();

            //pictureBox.Location = new Point(Me.location.X - 5, Me.location.Y - 5);
            //pictureBox.Invalidate();

            if (Other_player != null)
            {
                Other_player.rectangle.X = Other_player.location.X - 20;
                Other_player.rectangle.Y = Other_player.location.Y - 20;
                this.Invalidate(Other_player.rectangle);
            }

            Me.rectangle.X = Me.location.X - 20;
            Me.rectangle.Y = Me.location.Y - 20;
            this.Invalidate(Me.rectangle);

        }

        private void SendKeyDown()
        {
            if (keydown_lock == false)
            {
                MainClient.SendData(key, PacketType.AboutKey, "127.0.0.2");
                keydown_lock = true;
            }
        }
        private void SendKeyUp()
        {
            if (keydown_lock == true)
            {
                MainClient.SendData(key, PacketType.AboutKey, "127.0.0.2");
                keydown_lock = false;
            }
        }
        /* KeyUp을 할 때 마다 자기 자신 Location 정보 전송 */
        private void SendLocation()
        {
            MainClient.SendData(Me.location, PacketType.AboutLocation, "127.0.0.2");
        }


        private void Check_Other()
        {
            /* 다른 클라이언트 접속 시 */
            if (StudentManager.StudentDic.TryGetValue("127.0.0.1", out StudentData) && lock1)
            {
                if (StudentData.clr != Ch_Color.UnKnown)
                {
                    /* 클라이언트의 StudentData.Location.x/y 좌표가 -1,-1이라면 처음 접속 한 것이므로 좌표를 지정해준다.  */
                    if (StudentData.Location.X == -1 && StudentData.Location.Y == -1)
                    {
                        Other_player = new Player(StudentData.clr);
                        lock1 = false;
                        this.Invalidate();
                    }
                    /* -1,-1이 아니라면 이미 접속한 클라이언트 이므로 해당 StudentData.Location.x/y 좌표에 맞게 초기화 */
                    else
                    {
                        Other_player = new Player(StudentData.clr, StudentData.Location.X, StudentData.Location.Y);
                        lock1 = false;
                        this.Invalidate();
                    }
                }
            }
            if (StudentManager.StudentDic.TryGetValue("127.0.0.3", out StudentData) && lock2)
            {
                //Other_Players.Add(new Player(StudentData.clr));
                lock2 = false;
            }
            if (StudentManager.StudentDic.TryGetValue("127.0.0.4", out StudentData) && lock3)
            {
                //Other_Players.Add(new Player(StudentData.clr));
                lock3 = false;
            }
        }

        private void My_Move()
        {
            if (Me.Go_Up && Me.Block_Up && (Me.location.Y > 0))
            {
                Me.location.Y -= MoveStep;
                AnimatePlayer(0, 2);
                Me.Block_Down = true;
                Me.Block_Left = true;
                Me.Block_Right = true;
            }
            else if (Me.Go_Down && Me.Block_Down && (Me.location.Y + Me.Height < this.ClientSize.Height))
            {
                Me.location.Y += MoveStep;
                AnimatePlayer(3, 5);
                Me.Block_Up = true;
                Me.Block_Left = true;
                Me.Block_Right = true;
            }
            else if (Me.Go_Left && Me.Block_Left && (Me.location.X > 0))
            {
                Me.location.X -= MoveStep;
                AnimatePlayer(6, 8);
                Me.Block_Up = true;
                Me.Block_Down = true;
                Me.Block_Right = true;
            }
            else if (Me.Go_Right && Me.Block_Right && (Me.location.X + Me.Width < this.ClientSize.Width))
            {
                Me.location.X += MoveStep;
                AnimatePlayer(9, 11);
                Me.Block_Up = true;
                Me.Block_Down = true;
                Me.Block_Left = true;
            }
        }

        private void Other_Move()
        {
            int target_x = 0;
            int target_y = 0;
            float interpolationSpeed = 0.01f; 
            if (StudentManager.StudentDic.TryGetValue("127.0.0.1", out StudentData) && lock1 == false)
            {
                //if (StudentData.Key.Go_Up && (Other_player.location.Y > 0))
                //{
                //    target_y = Other_player.location.Y;
                //    target_y -= MoveStep;
                //    Other_player.location.Y = Interpolate(Other_player.location.Y, target_y, interpolationSpeed * 0.014f);
                //    Other_AnimatePlayer(0, 2);
                //}
                //else if (StudentData.Key.Go_Down && (Other_player.location.Y + Other_player.Height < this.ClientSize.Height))
                //{
                //    target_y = Other_player.location.Y;
                //    target_y += MoveStep;
                //    Other_player.location.Y = Interpolate(Other_player.location.Y, target_y, interpolationSpeed * 0.014f);
                //    Other_AnimatePlayer(3, 5);
                //}
                //else if (StudentData.Key.Go_Left && Other_player.location.X > 0)
                //{
                //    target_x = Other_player.location.X;
                //    target_x -= MoveStep;
                //    Other_player.location.X = Interpolate(Other_player.location.X, target_x, interpolationSpeed * 0.014f);
                //    Other_AnimatePlayer(6, 8);
                //}
                //else if (StudentData.Key.Go_Right && (Other_player.location.X + Other_player.Width < this.ClientSize.Width))
                //{
                //    target_x = Other_player.location.X;
                //    target_x += MoveStep;
                //    Other_player.location.X = Interpolate(Other_player.location.X, target_x, interpolationSpeed * 0.014f);
                //    Other_AnimatePlayer(9, 11);
                //}

                if (StudentData.Key.Go_Up && (Other_player.location.Y > 0))
                {
                    Other_player.location.Y -= MoveStep;
                    Other_AnimatePlayer(0, 2);
                }
                else if (StudentData.Key.Go_Down && (Other_player.location.Y + Other_player.Height < this.ClientSize.Height))
                {
                    Other_player.location.Y += MoveStep;
                    Other_AnimatePlayer(3, 5);
                }
                else if (StudentData.Key.Go_Left && Other_player.location.X > 0)
                {
                    Other_player.location.X -= MoveStep;
                    Other_AnimatePlayer(6, 8);
                }
                else if (StudentData.Key.Go_Right && (Other_player.location.X + Other_player.Width < this.ClientSize.Width))
                {
                    Other_player.location.X += MoveStep;
                    Other_AnimatePlayer(9, 11);
                }

                /* 해당 조건문은 Other Client가 KeyUp상태 일 때, Other Player좌표 값을 동기화 시켜준다. */
                else if (StudentData.Key.Go_Up == false && StudentData.Key.Go_Down == false && StudentData.Key.Go_Left == false && StudentData.Key.Go_Right == false && lock1 == false)
                {

                    if (Other_player != null)
                    {
                        if (StudentData.Location.X != -1 && StudentData.Location.Y != -1)
                        {
                            Other_player.location.X = StudentData.Location.X;
                            Other_player.location.Y = StudentData.Location.Y;
                        }
                    }
                }
            }
        }
        private int Interpolate(int start, int end, float t) { return (int)(start + (end - start) * t); }

        public static void LoadImages() /* 이미지 불러오기 */
        {
            for (int i = 0; i < 3; i++)
            {
                images[i, (int)Direction.Up] = Properties.Resources.a_Player_Up;
                images[i, (int)Direction.Down] = Properties.Resources.a_Player_Down;
                images[i, (int)Direction.Left] = Properties.Resources.a_Player_Left;
                images[i, (int)Direction.Right] = Properties.Resources.a_Player_Right;
                images[i, (int)Direction.Up_Walk] = Properties.Resources.a_Player_Up_Walk1;
                images[i, (int)Direction.Down_Walk] = Properties.Resources.a_Player_Down_Walk1;
                images[i, (int)Direction.Left_Walk] = Properties.Resources.a_Player_Left_Walk1;
                images[i, (int)Direction.Right_Walk] = Properties.Resources.a_Player_Right_Walk1;
                images[i, (int)Direction.Up_Walk2] = Properties.Resources.a_Player_Up_Walk2;
                images[i, (int)Direction.Down_Walk2] = Properties.Resources.a_Player_Down_Walk2;
                images[i, (int)Direction.Left_Walk2] = Properties.Resources.a_Player_Left_Walk2;
                images[i, (int)Direction.Right_Walk2] = Properties.Resources.a_Player_Right_Walk2;
            }
        }

        private void Collision_Detection_Right()
        {
            foreach (Control control in this.Controls)
            {
                if (control is PictureBox && (string)control.Tag == "obstacle")
                {
                    /* Player Right 충돌검사 */
                    if (Me.location.X < control.Location.X && Me.location.X + Me.Width >= control.Location.X && Me.location.Y + Me.Height >= control.Location.Y + 10
                        && Me.location.Y <= control.Location.Y + control.Height - 20)
                    {
                        Me.Block_Right = false;
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
                    if (Me.location.Y > control.Location.Y && Me.location.Y <= control.Location.Y + control.Height - 10 && Me.location.X + Me.Width >= control.Location.X + 20
                        && Me.location.X <= control.Location.X + control.Width - 20)
                    {
                        Me.Block_Up = false;
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
                    if (Me.location.X + Me.Width > control.Location.X + control.Width && Me.location.X <= control.Location.X + control.Width && Me.location.Y + Me.Height >= control.Location.Y + 10
                        && Me.location.Y <= control.Location.Y + control.Height - 20)
                    {
                        Me.Block_Left = false;
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
                    if (Me.location.Y < control.Location.Y && Me.location.Y + Me.Height >= control.Location.Y && Me.location.X + Me.Width >= control.Location.X + 20
                        && Me.location.X <= control.Location.X + control.Width - 20)
                    {
                        Me.Block_Down = false;
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
            Me.SlowDownFrameRate++;
            /* player animation 구현 -> 4번 호출 될 때마다 이미지 변경 */
            if (Me.SlowDownFrameRate == 4)
            {
                Me.steps++;
                Me.SlowDownFrameRate = 0;
            }

            /* 정지 -> 왼발 -> 오른발 -> 정지 .... 순서 */
            if ((Me.steps > end) || (Me.steps < start))
            {
                Me.steps = start;
            }
            Me.image = images[(int)Me.clr, Me.steps];
        }

        private void Other_AnimatePlayer(int start, int end)
        {
            Other_player.SlowDownFrameRate++;
            /* player animation 구현 -> 4번 호출 될 때마다 이미지 변경 */
            if (Other_player.SlowDownFrameRate == 4)
            {
                Other_player.steps++;
                Other_player.SlowDownFrameRate = 0;
            }

            /* 정지 -> 왼발 -> 오른발 -> 정지 .... 순서 */
            if ((Other_player.steps > end) || (Other_player.steps < start))
            {
                Other_player.steps = start;
            }
            Other_player.image = images[(int)Other_player.clr, Other_player.steps];
        }

        protected virtual void Initilalize()
        {

        }

        protected virtual void BaseForm_Closing(object sender, FormClosedEventArgs e)
        {
            timer.Stop();
        }

        //protected override void OnFormClosing(FormClosingEventArgs e)
        //{
        //    if (e.CloseReason == CloseReason.UserClosing)
        //        Application.Exit();

        //    else if (e.CloseReason == CloseReason.ApplicationExitCall) { }

        //    base.OnFormClosing(e);
        //}


    }
}
