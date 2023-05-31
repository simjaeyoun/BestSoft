using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 로그인화면
{
    public class BaseForm_test : Form
    {
        protected System.Windows.Forms.Timer timer;
        protected bool lock_invalidate1 = true;

        protected bool Space_Up = false, Space_Down = false, Space_Left = false, Space_Right = false;
        protected int Map;

        public static bool lock1 = true, lock2 = true, lock3 = true;
        protected bool keydown_lock = false;
        protected Move_Key key = new Move_Key { Go_Up = false, Go_Down = false, Go_Left = false, Go_Right = false };

        protected StudentData My_StudentData;
        protected StudentData StudentData;

        protected const int MoveStep = 4;

        public static Image[,] images = new Image[4, 16]; /* images[clr][direction] -> Player의 [색깔][방향] 이미지 2차원 배열 */

        public static Ch_Color clr;
        public static Player Me;
        public static Player Other_player;
        
        public Label MyLabel; 
        public Label OtherLabel;

        public static ChatForm chatForm = new ChatForm();


        public PictureBox ChatBubble; //
        public PictureBox OtherBubble; //


        public BaseForm_test()
        {
            StudentManager.StudentDic.TryGetValue("127.0.0.1", out My_StudentData);

            SetUp();

            Map = 0;
        }
        private void SetUp()
        {
            this.DoubleBuffered = true;
            timer = new System.Windows.Forms.Timer();
            timer.Interval = 14;
            timer.Enabled = false;
            timer.Tick += BaseForm_Timer_Tick;

            this.KeyDown += BaseForm_KeyDown;
            this.KeyUp += BaseForm_KeyUp;
            this.Paint += BaseForm_Paint;
            this.VisibleChanged += BaseForm_Hide;

            //

            MyLabel = new Label();
            MyLabel.BackColor = System.Drawing.Color.Transparent;
            MyLabel.Name = "MyLabel";
            MyLabel.Size = new System.Drawing.Size(60, 14);
            MyLabel.Font = new Font("Arial", 7, FontStyle.Bold);
            MyLabel.Text = "MyLabel";
            //MyLabel.TextAlign = ContentAlignment.MiddleCenter;
            MyLabel.AutoSize = true;
            MyLabel.Click += BaseForm_Label_Click;

            MyLabel.Visible = true;
            Controls.Add(MyLabel);

            // 새로 추가
            OtherLabel = new Label();
            OtherLabel.BackColor = System.Drawing.Color.Transparent;
            OtherLabel.Name = "OtherLabel";
            OtherLabel.Size = new System.Drawing.Size(60, 14);
            OtherLabel.Font = new Font("Arial", 7, FontStyle.Bold);
            OtherLabel.Text = "OtherLabel";
            // OtherLabel.TextAlign = ContentAlignment.MiddleCenter;
            OtherLabel.AutoSize = true;
            OtherLabel.Click += BaseForm_Label_Click;

            OtherLabel.Visible = false;
            Controls.Add(OtherLabel);
            // 

            ChatBubble = new PictureBox();
            ChatBubble.BackColor = System.Drawing.SystemColors.Window;
            ChatBubble.Name = "ChatBubble";
            ChatBubble.Size = new System.Drawing.Size(133, 41);
            ChatBubble.TabStop = false;
            ChatBubble.Visible = false;
            Controls.Add(ChatBubble);


            OtherBubble = new PictureBox();
            OtherBubble.BackColor = System.Drawing.SystemColors.Window;
            OtherBubble.Name = "OtherBubble";
            OtherBubble.Size = new System.Drawing.Size(133, 41);
            OtherBubble.TabStop = false;
            OtherBubble.Visible = false;
            Controls.Add(OtherBubble);

            chatForm.Hide();

            // 아래 주석치면 키자마자 없어짐 왜지?

            MyLabel.Text = My_StudentData.StudentName;
            MyLabel.Name = Me.address;
        }

        private void BaseForm_Label_Click(object sender, EventArgs e)
        {
            Label clickedLabel = sender as Label;
            if (clickedLabel != null)
            {
                string labelName = clickedLabel.Text;
                string labelNum = clickedLabel.Name; 
                MessageBox.Show("Label Name: " + labelName + "Label Num: " + labelNum);
            }
        }

        private void BaseForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up) { key.Go_Up = true; SendKeyDown(); ChatBubble.Hide(); }
            else if (e.KeyCode == Keys.Down) { key.Go_Down = true; SendKeyDown(); ChatBubble.Hide(); }
            else if (e.KeyCode == Keys.Left) { key.Go_Left = true; SendKeyDown(); ChatBubble.Hide(); }
            else if (e.KeyCode == Keys.Right) { key.Go_Right = true; SendKeyDown(); ChatBubble.Hide(); }

            else if ((e.KeyCode == Keys.Space) && (Space_Up || Space_Down || Space_Left || Space_Right))
            {
                key.Go_Up = false; key.Go_Down = false; key.Go_Left = false; key.Go_Right = false;
                Space_Event(Me.Obstacle_Name);
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
            key.Go_Up = false; key.Go_Down = false; key.Go_Left = false; key.Go_Right = false;
            SendKeyUp(); SendLocation(); this.Invalidate();
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

        }

        private void SendKeyDown()
        {
            if (keydown_lock == false)
            {
                MainClient.SendData(key, PacketType.AboutKey, "127.0.0.1");
                keydown_lock = true;
            }
        }
        private void SendKeyUp()
        {
            if (keydown_lock == true)
            {   
                MainClient.SendData(key, PacketType.AboutKey, "127.0.0.1");
                keydown_lock = false;
            }
        }
        /* KeyUp을 할 때 마다 자기 자신 Location 정보 전송 */
        public static void SendLocation()
        {
            MainClient.SendData(Me.location, PacketType.AboutLocation, "127.0.0.1");
        }

        //private void Check_Oth()
        //{
        //    if (lock1 == false && lock_invalidate1)
        //    {
        //        this.Invalidate();
        //        lock_invalidate1 = false;
        //    }
        //}

        private void Check_Other()
        {
            /* 다른 클라이언트 접속 시 */
            if (StudentManager.StudentDic.TryGetValue("127.0.0.2", out StudentData) && lock1)
            {
                if (StudentData.clr != Ch_Color.UnKnown)
                {
                    /* 클라이언트의 StudentData.Location.x/y 좌표가 -1,-1이라면 처음 접속 한 것이므로 좌표를 지정해준다.  */
                    if (StudentData.Location.X == -1 && StudentData.Location.Y == -1)
                    {
                        Other_player = new Player(StudentData.clr, "127.0.0.2");
                        lock1 = false;
                        this.Invalidate();
                    }
                    /* -1,-1이 아니라면 이미 접속한 클라이언트 이므로 해당 StudentData.Location.x/y 좌표에 맞게 초기화 */
                    else
                    {
                        Other_player = new Player(StudentData.clr, StudentData.Location.X, StudentData.Location.Y, "127.0.0.2");
                        lock1 = false;
                        this.Invalidate();
                    }
                    OtherLabel.Text = StudentData.StudentName;
                    OtherLabel.Name = Other_player.address;
                    OtherLabel.Visible = true;
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
            if (lock1 == false && Other_player.Info != null)
            {
                if (Other_player.Info.result) 
                { 
                    Space_Event(Other_player.Info.ObstacleName);
                    Other_player.Info.result = false;
                }
            }
        }

        private void My_Move()
        {
            Me.rectangle.X = Me.location.X - 20;
            Me.rectangle.Y = Me.location.Y - 20;

            if (Me.Key.Go_Up && Me.Block_Up && (Me.location.Y > 0))
            {
                Me.location.Y -= MoveStep;
                AnimatePlayer(12, 15);
                Me.Block_Down = true;
                Me.Block_Left = true;
                Me.Block_Right = true;
                this.Invalidate(Me.rectangle);
            }
            else if (Me.Key.Go_Down && Me.Block_Down && (Me.location.Y + Me.Height < this.ClientSize.Height))
            {
                Me.location.Y += MoveStep;
                AnimatePlayer(0, 3);
                Me.Block_Up = true;
                Me.Block_Left = true;
                Me.Block_Right = true;
                this.Invalidate(Me.rectangle);
            }
            else if (Me.Key.Go_Left && Me.Block_Left && (Me.location.X > 0))
            {
                Me.location.X -= MoveStep;
                AnimatePlayer(4,7);
                Me.Block_Up = true;
                Me.Block_Down = true;
                Me.Block_Right = true;
                this.Invalidate(Me.rectangle);
            }
            else if (Me.Key.Go_Right && Me.Block_Right && (Me.location.X + Me.Width < this.ClientSize.Width))
            {
                Me.location.X += MoveStep;
                AnimatePlayer(8,11);
                Me.Block_Up = true;
                Me.Block_Down = true;
                Me.Block_Left = true;
                this.Invalidate(Me.rectangle);
            }

            MyLabel.Location = new Point(Me.location.X, Me.location.Y + 50); //
        }

        private void Other_Move()
        {
            if (Other_player != null)
            {
                Other_player.rectangle.X = Other_player.location.X - 20;
                Other_player.rectangle.Y = Other_player.location.Y - 20;
            }

            if (StudentManager.StudentDic.TryGetValue("127.0.0.2", out StudentData) && lock1 == false)
            {
                if (Other_player.Key.Go_Up && Other_player.Block_Up && (Other_player.location.Y > 0))
                {
                    Other_player.location.Y -= MoveStep;
                    Other_AnimatePlayer(12, 15);
                    Other_player.Block_Down = true;
                    Other_player.Block_Left = true;
                    Other_player.Block_Right = true;
                    OtherBubble.Hide(); // 버블 숨김
                    this.Invalidate(Other_player.rectangle);
                }
                else if (Other_player.Key.Go_Down && Other_player.Block_Down && (Other_player.location.Y + Other_player.Height < this.ClientSize.Height))
                {
                    Other_player.location.Y += MoveStep;
                    Other_AnimatePlayer(0, 3);
                    Other_player.Block_Up = true;
                    Other_player.Block_Left = true;
                    Other_player.Block_Right = true;
                    OtherBubble.Hide(); // 버블 숨김
                    this.Invalidate(Other_player.rectangle);
                }
                else if (Other_player.Key.Go_Left && Other_player.Block_Left && Other_player.location.X > 0)
                {
                    Other_player.location.X -= MoveStep;
                    Other_AnimatePlayer(4, 7);
                    Other_player.Block_Down = true;
                    Other_player.Block_Up = true;
                    Other_player.Block_Right = true;
                    OtherBubble.Hide(); // 버블 숨김
                    this.Invalidate(Other_player.rectangle);
                }
                else if (Other_player.Key.Go_Right && Other_player.Block_Right && (Other_player.location.X + Other_player.Width < this.ClientSize.Width))
                {
                    Other_player.location.X += MoveStep;
                    Other_AnimatePlayer(8, 11);
                    Other_player.Block_Down = true;
                    Other_player.Block_Left = true;
                    Other_player.Block_Up = true;
                    OtherBubble.Hide(); // 버블 숨김
                    this.Invalidate(Other_player.rectangle);
                }
                OtherLabel.Location = new Point(Other_player.location.X, Other_player.location.Y + 50); //
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
                        Me.Obstacle_Name = control.Name; /* Player와 충돌된 Obstacle Name 저장 */
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
                        Me.Obstacle_Name = control.Name;
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
                        Me.Obstacle_Name = control.Name;
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
                        Me.Obstacle_Name = control.Name;
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

        private void BaseForm_Hide(object sender, EventArgs e)
        {
            if (!this.Visible)
            {
                timer.Stop();
            }
        }

        public void Create_Other_Bubble()
        {
            if (StudentManager.StudentDic.TryGetValue("127.0.0.2", out StudentData) && lock1 == false)
            {

                if (StudentData.bubblechat == null || !StudentData.bubblechat.HasBeenUpdated)
                {

                    return;
                }
                try
                {
                    // 비어져있지만 update 되지 않았을 경우에 처리가 안돼
                    string other_input_chat = StudentData.bubblechat.InputChat;
                    
                    OtherBubble.Visible = true;
                    OtherBubble.BackColor = Color.Transparent;
                    OtherBubble.Location = new Point(Other_player.location.X, Other_player.location.Y + Other_player.Height - 100);
                    OtherBubble.Image = CreateTextImage(other_input_chat);

                    // 2023.05.30 size 조절 실패
                    // 만약 글씨가 많아진다면 size가 자동으로 커졋다가 작아졋다가 해야함?
                    // OtherBubble.SizeMode = PictureBoxSizeMode.AutoSize;
                    //
                    StudentData.bubblechat.HasBeenUpdated = false;
                    chatForm.LogAppend(StudentData.bubblechat.LogChat);

                }
                catch (Exception ex) { MessageBox.Show("오류  3번 " + ex.Message); return; }
            }
        }
        public void Create_My_Bubble() //
        {
            if (StudentManager.StudentDic.TryGetValue("127.0.0.1", out StudentData))
            {
                if (StudentData.bubblechat == null || !StudentData.bubblechat.HasBeenUpdated)
                {

                    return;
                }
                try
                {

                    string My_input_chat = StudentData.bubblechat.InputChat;
                    ChatBubble.Visible = true;
                    ChatBubble.BackColor = Color.Transparent;

                    ChatBubble.Location = new Point(Me.location.X, Me.location.Y + Me.Height - 100);
                    ChatBubble.Image = CreateTextImage(My_input_chat);
                    // 만약 글씨가 많아진다면 size가 자동으로 커졋다가 작아졋다가 해야함?
                    ChatBubble.SizeMode = PictureBoxSizeMode.AutoSize;
                    //
                    StudentData.bubblechat.HasBeenUpdated = false;
                    chatForm.LogAppend(StudentData.bubblechat.LogChat);

                }
                catch (Exception ex) { MessageBox.Show("오류  1번 " + ex.Message); return; }

            }
            
        }
        private Image CreateTextImage(string text) //
        {
            // 원본 이미지를 복사합니다.
            Image bubbleImage = (Image)Properties.Resources.bubble.Clone();
            // 캔버스 생성
            Bitmap bitmap = new Bitmap(ChatBubble.Width, ChatBubble.Height);
            Graphics graphics = Graphics.FromImage(bitmap);
            graphics.Clear(Color.Transparent);

            // 원본 이미지를 캔버스에 그립니다.
            //graphics.DrawImage(bitmap, 0, 0, ChatBubble.Width, ChatBubble.Height);

            // 새로추가 
            graphics.DrawImage(bubbleImage, 0, 0, ChatBubble.Width, ChatBubble.Height);

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



        protected override void OnFormClosing(FormClosingEventArgs e)
        {

            timer.Stop();
            base.OnFormClosing(e);

            if (e.CloseReason == CloseReason.UserClosing)
            {

                /*foreach (Form form in Application.OpenForms)
                {
                    if (form != this)
                    {
                        form.Close();
                    }
                }*/

                Application.ExitThread();

                Environment.Exit(0);
            }
            else if (e.CloseReason == CloseReason.ApplicationExitCall) { }
        }

    }
}

