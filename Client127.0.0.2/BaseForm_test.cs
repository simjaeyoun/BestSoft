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
    public class BaseForm_test: Form
    {
        protected System.Windows.Forms.Timer timer;

        protected bool Space_Up = false, Space_Down = false, Space_Left = false, Space_Right = false;
        protected string Obstacle_Name;
        protected Info_Next Info;

        protected bool lock1 = true, lock2 = true, lock3 = true;
        protected bool keydown_lock = false;
        protected Move_Key key = new Move_Key { Go_Up = false, Go_Down = false, Go_Left = false, Go_Right = false };

        protected StudentData My_StudentData;
        protected StudentData StudentData;
        protected Player Other_player;

        protected const int MoveStep = 4;

        public static Image[,] images = new Image[3, 12]; /* images[clr][direction] -> Player의 [색깔][방향] 이미지 2차원 배열 */
        public static Ch_Color clr;
        public static Player Me;

        public Label MyLabel; //
        public ChatForm chatForm;

        public PictureBox ChatBubble; //
        public PictureBox OtherBubble; //

        public BaseForm_test()
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

            Info = new Info_Next();
            Info.ObstacleName = null;
            Info.result = false;

            MyLabel= new Label();  
            MyLabel.BackColor = System.Drawing.Color.Transparent;
            MyLabel.Name = "MyLabel";
            MyLabel.Size = new System.Drawing.Size(54, 12);
            MyLabel.Text = "MyLabel";
            MyLabel.Visible = true;
            Controls.Add(MyLabel);

            ChatBubble = new PictureBox();
            ChatBubble.BackColor = System.Drawing.SystemColors.Window;
            ChatBubble.Name = "ChatBubble";
            ChatBubble.Size = new System.Drawing.Size(133, 41);
            ChatBubble.TabStop = false;
            ChatBubble.Visible = false;
            Controls.Add(ChatBubble);

            chatForm = new ChatForm();
            chatForm.Hide();

            StudentManager.StudentDic.TryGetValue("127.0.0.2", out My_StudentData); // 자기자신
        }


        private void BaseForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up) { Me.Go_Up = true; key.Go_Up = true; SendKeyDown(); ChatBubble.Hide(); }
            else if (e.KeyCode == Keys.Down) { Me.Go_Down = true; key.Go_Down = true; SendKeyDown(); ChatBubble.Hide(); }
            else if (e.KeyCode == Keys.Left) { Me.Go_Left = true; key.Go_Left = true; SendKeyDown(); ChatBubble.Hide(); }
            else if (e.KeyCode == Keys.Right) { Me.Go_Right = true; key.Go_Right = true; SendKeyDown(); ChatBubble.Hide(); }

            else if ((e.KeyCode == Keys.Space) && Space_Up || Space_Down || Space_Left || Space_Right)
            {
                Me.Go_Up = false; Me.Go_Down = false; Me.Go_Left = false; Me.Go_Right = false;
                Space_Event(Obstacle_Name);
            }
            else if (e.KeyCode == Keys.Tab) //
            {
                chatForm.Show();
            }
            else if (e.KeyCode == Keys.Escape) //
            {
                chatForm.Hide();
            }
        }

        protected virtual void Space_Event(string Obstacle_Name)
        {
            /* Obstacle Name에 따른 이벤트 작성 */
        }

        private void BaseForm_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up) { Me.Go_Up = false; key.Go_Up = false; key.Go_Down = false; key.Go_Left = false; key.Go_Right = false; SendKeyUp(); SendLocation(); }
            else if (e.KeyCode == Keys.Down) { Me.Go_Down = false; key.Go_Up = false; key.Go_Down = false; key.Go_Left = false; key.Go_Right = false; SendKeyUp(); SendLocation(); }
            else if (e.KeyCode == Keys.Left) { Me.Go_Left = false; key.Go_Up = false; key.Go_Down = false; key.Go_Left = false; key.Go_Right = false; SendKeyUp(); SendLocation(); }
            else if (e.KeyCode == Keys.Right) { Me.Go_Right = false; key.Go_Up = false; key.Go_Down = false; key.Go_Left = false; key.Go_Right = false; SendKeyUp(); SendLocation(); }
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

            Check_Next();


            Create_My_Bubble();///
            Create_Other_Bubble();///

            if (Other_player != null)
            {
                Other_player.rectangle.X = Other_player.location.X - 20;
                Other_player.rectangle.Y = Other_player.location.Y - 20;
                this.Invalidate(Other_player.rectangle);
            }

            Me.rectangle.X = Me.location.X - 20;
            Me.rectangle.Y = Me.location.Y - 20;
            this.Invalidate(Me.rectangle);

            this.Invalidate(MyLabel.DisplayRectangle);
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
        private void Check_Next()
        {
            if (lock1 == false)
            {
                if (StudentData.Info.result)
                {
                    Space_Event(StudentData.Info.ObstacleName);
                }
            }
        }

        private void My_Move()
        {
            if (My_StudentData.Key.Go_Up && Me.Block_Up && (Me.location.Y > 0))
            {
                Me.location.Y -= MoveStep;
                AnimatePlayer(0, 2);
                Me.Block_Down = true;
                Me.Block_Left = true;
                Me.Block_Right = true;
            }
            else if (My_StudentData.Key.Go_Down && Me.Block_Down && (Me.location.Y + Me.Height < this.ClientSize.Height))
            {
                Me.location.Y += MoveStep;
                AnimatePlayer(3, 5);
                Me.Block_Up = true;
                Me.Block_Left = true;
                Me.Block_Right = true;
            }
            else if (My_StudentData.Key.Go_Left && Me.Block_Left && (Me.location.X > 0))
            {
                Me.location.X -= MoveStep;
                AnimatePlayer(6, 8);
                Me.Block_Up = true;
                Me.Block_Down = true;
                Me.Block_Right = true;
            }
            else if (My_StudentData.Key.Go_Right && Me.Block_Right && (Me.location.X + Me.Width < this.ClientSize.Width))
            {
                Me.location.X += MoveStep;
                AnimatePlayer(9, 11);
                Me.Block_Up = true;
                Me.Block_Down = true;
                Me.Block_Left = true;
            }

            MyLabel.Location = new Point(Me.location.X, Me.location.Y + 50); //
        }

        private void Other_Move()
        {
            if (StudentManager.StudentDic.TryGetValue("127.0.0.1", out StudentData) && lock1 == false)
            {
                if (StudentData.Key.Go_Up && Other_player.Block_Up && (Other_player.location.Y > 0))
                {
                    Other_player.location.Y -= MoveStep;
                    Other_AnimatePlayer(0, 2);
                    Other_player.Block_Down = true;
                    Other_player.Block_Left = true;
                    Other_player.Block_Right = true;
                }
                else if (StudentData.Key.Go_Down && Other_player.Block_Down && (Other_player.location.Y + Other_player.Height < this.ClientSize.Height))
                {
                    Other_player.location.Y += MoveStep;
                    Other_AnimatePlayer(3, 5);
                    Other_player.Block_Up = true;
                    Other_player.Block_Left = true;
                    Other_player.Block_Right = true;
                }
                else if (StudentData.Key.Go_Left && Other_player.Block_Left && Other_player.location.X > 0)
                {
                    Other_player.location.X -= MoveStep;
                    Other_AnimatePlayer(6, 8);
                    Other_player.Block_Down = true;
                    Other_player.Block_Up = true;
                    Other_player.Block_Right = true;
                }
                else if (StudentData.Key.Go_Right && Other_player.Block_Right && (Other_player.location.X + Other_player.Width < this.ClientSize.Width))
                {
                    Other_player.location.X += MoveStep;
                    Other_AnimatePlayer(9, 11);
                    Other_player.Block_Down = true;
                    Other_player.Block_Left = true;
                    Other_player.Block_Up = true;
                }

            }
        }
        
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
                        Obstacle_Name = control.Name; /* Player와 충돌된 Obstacle Name 저장 */
                        break;
                    }
                    else { Space_Right = false; }

                    if (lock1 == false)
                    {
                        if (Other_player.location.X < control.Location.X && Other_player.location.X + Other_player.Width >= control.Location.X && Other_player.location.Y + Other_player.Height >= control.Location.Y + 10
                            && Other_player.location.Y <= control.Location.Y + control.Height - 20)
                        {
                            Other_player.Block_Right = false;
                            break;
                        }
                    }
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
                        Obstacle_Name = control.Name;
                        break;
                    }
                    else { Space_Up = false; }

                    if (lock1 == false)
                    {
                        if (Other_player.location.Y > control.Location.Y && Other_player.location.Y <= control.Location.Y + control.Height - 10 && Other_player.location.X + Other_player.Width >= control.Location.X + 20
                            && Other_player.location.X <= control.Location.X + control.Width - 20)
                        {
                            Other_player.Block_Up = false;
                            break;
                        }
                    }

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
                        Obstacle_Name = control.Name;
                        break;
                    }
                    else { Space_Left = false; }

                    if (lock1 == false)
                    {
                        if (Other_player.location.X + Me.Width > control.Location.X + control.Width && Other_player.location.X <= control.Location.X + control.Width && Other_player.location.Y + Other_player.Height >= control.Location.Y + 10
                            && Other_player.location.Y <= control.Location.Y + control.Height - 20)
                        {
                            Other_player.Block_Left = false;
                            break;
                        }
                    }
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
                        Obstacle_Name = control.Name;
                        break;
                    }
                    else { Space_Down = false; }

                    if (lock1 == false)
                    {
                        if (Other_player.location.Y < control.Location.Y && Other_player.location.Y + Other_player.Height >= control.Location.Y && Other_player.location.X + Other_player.Width >= control.Location.X + 20
                            && Other_player.location.X <= control.Location.X + control.Width - 20)
                        {
                            Other_player.Block_Down = false;
                            break;
                        }
                    }
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

        protected virtual void BaseForm_Closing(object sender, FormClosedEventArgs e)
        {
            timer.Stop();
        }

        public void Create_Other_Bubble()
        {
            if (StudentManager.StudentDic.TryGetValue("127.0.0.1", out StudentData) && lock1 == false)
            {

                if (StudentData.bubblechat == null || !StudentData.bubblechat.HasBeenUpdated)
                {

                    return;
                }
                try
                {
                    // 비어져있지만 update 되지 않았을 경우에 처리가 안돼
                    string other_input_chat = StudentData.bubblechat.chat;
                    // 일단 안에 넣어보았다.

                    //OtherBubble.Visible = true;
                    //OtherBubble.Location = new Point(Other_player.location.X, Other_player.location.Y + Other_player.Height - 100);


                    //chatForm.ChatLog_Show(other_name, other_input_chat);
                    //if (chatForm.ChatLog_Show(other_input_chat))
                    //{
                    //    OtherBubble.Visible = true;
                    //    OtherBubble.Location = new Point(Other_player.location.X, Other_player.location.Y + Other_player.Height - 100);
                    //    OtherBubble.Image = CreateTextImage(other_input_chat);

                    //    StudentData.bubblechat.HasBeenUpdated = false;
                    //}
                    OtherBubble.Visible = true;
                    OtherBubble.Location = new Point(Other_player.location.X, Other_player.location.Y + Other_player.Height - 100);
                    OtherBubble.Image = CreateTextImage(other_input_chat);

                    StudentData.bubblechat.HasBeenUpdated = false;
                }
                catch (Exception ex) { MessageBox.Show("오류 시발 3번 " + ex.Message); return; }
            }
            //OtherBubble.Visible = true;
            //OtherBubble.Location = new Point(Other_player.location.X, Other_player.location.Y + Other_player.Height - 100);
            //ChatBubble.Image = CreateTextImage(other_input_chat);
        }
        public void Create_My_Bubble() //
        {
            if (StudentManager.StudentDic.TryGetValue("127.0.0.2", out StudentData))
            {
                if (StudentData.bubblechat == null || !StudentData.bubblechat.HasBeenUpdated)
                {

                    return;
                }
                try
                {

                    string My_input_chat = StudentData.bubblechat.chat;
                    ChatBubble.Visible = true;
                    ChatBubble.Location = new Point(Me.location.X, Me.location.Y + Me.Height - 100);
                    ChatBubble.Image = CreateTextImage(My_input_chat);

                    StudentData.bubblechat.HasBeenUpdated = false;

                }
                catch (Exception ex) { MessageBox.Show("오류 시발 1번 " + ex.Message); return; }

            }
            //bubbleChat.chat = inputchat;
            //ChatBubble.Visible = true;
            //ChatBubble.Location = new Point(Me.location.X, Me.location.Y + Me.Height - 100);
            //ChatBubble.Image = CreateTextImage(inputchat);
            //MainClient.SendData(bubbleChat,PacketType.AboutChat, "127.0.0.1");

        }
        private Image CreateTextImage(string text) //
        {
            // 원본 이미지를 복사합니다.
            //Image bubbleImage = (Image)Properties.Resources.bubble.Clone();
            // 캔버스 생성
            Bitmap bitmap = new Bitmap(ChatBubble.Width, ChatBubble.Height);
            Graphics graphics = Graphics.FromImage(bitmap);
            graphics.Clear(Color.Transparent);

            // 원본 이미지를 캔버스에 그립니다.
            graphics.DrawImage(bitmap, 0, 0, ChatBubble.Width, ChatBubble.Height);

            // 텍스트 출력 설정
            Font font = new Font("Arial", 12);
            SolidBrush brush = new SolidBrush(Color.Black);
            StringFormat stringFormat = new StringFormat
            {
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center
            };

            // 텍스트를 이미지로 그리기
            graphics.DrawString(text, font, brush, new RectangleF(0, 0, ChatBubble.Width, ChatBubble.Height), stringFormat);

            // 리소스 정리
            brush.Dispose();
            graphics.Dispose();

            return bitmap;
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

