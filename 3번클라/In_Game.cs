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

    public partial class In_Game : Form
    {
        Player Me;
        private int MoveStep=4;
        private bool Space_Up = false, Space_Down = false, Space_Left = false, Space_Right = false;
        private string Ob_Name;

        /* 다른 클라이언트 접속 시 저장할 List */
        List<Player> Other_Players = new List<Player>();
        StudentData StudentData;
        private bool lock1 = true, lock2 = true, lock3 = true;
        private bool keydown_lock = false;
        private Move_Key key = new Move_Key { Go_Up = false, Go_Down = false, Go_Left = false, Go_Right = false };

        Player Other_player; 

        public static Ch_Color clr;
        public static Image[,] images = new Image[3, 12]; /* images[clr][direction] -> Player의 [색깔][방향] 이미지 2차원 배열 */
        /// <summary>
        /// cnrk
        /// </summary>
        /// 
        

        private ChatForm chatForm;
        public In_Game()
        {
            InitializeComponent();
            color();      /* Color 선택 폼 불러오기 */
            LoadImages(); /* image file 불러오기 */
            timer1.Enabled = true;
            chatForm = new ChatForm();
           
            //chatForm.ChatLog_Show();
            chatForm.Hide();

            // 자신의 Label visible true 
            //MyLabel.Location = new Point(Me.location.X, Me.location.Y + Me.Height + 50);
            //MyLabel.Visible = true;
        }

        private void color()
        {
            Select_Color select = new Select_Color();
            select.Owner = this;
            DialogResult Result = select.ShowDialog();
        }


        private void In_Game_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up) { Me.Go_Up = false; key.Go_Up = false; SendKeyUp(); SendLocation(); }
            else if (e.KeyCode == Keys.Down) { Me.Go_Down = false; key.Go_Down = false; SendKeyUp(); SendLocation(); }
            else if (e.KeyCode == Keys.Left) { Me.Go_Left = false; key.Go_Left = false; SendKeyUp(); SendLocation(); }
            else if (e.KeyCode == Keys.Right) { Me.Go_Right = false; key.Go_Right = false; SendKeyUp(); SendLocation(); }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Check_Other();
            Collision_Detection_Up();
            Collision_Detection_Down();
            Collision_Detection_Left();
            Collision_Detection_Right();

            My_Move();

            Create_My_Bubble();

            Other_Move();
            //추가
            Create_Other_Bubble();

            this.Invalidate();
        }

        private void Paint_Character(object sender, PaintEventArgs e)
        {
            Graphics Canvas = e.Graphics;
            Canvas.DrawImage(Me.image, Me.location.X, Me.location.Y, Me.Width, Me.Height);

            if (lock1==false)
            {
                if (Other_player.image != null)
                {
                    Canvas.DrawImage(Other_player.image, Other_player.location.X, Other_player.location.Y, Other_player.Width, Other_player.Height);
                }
            }
            
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
                    }
                    /* -1,-1이 아니라면 이미 접속한 클라이언트 이므로 해당 StudentData.Location.x/y 좌표에 맞게 초기화 */
                    else
                    {
                        Other_player = new Player(StudentData.clr, StudentData.Location.X, StudentData.Location.Y);
                        lock1 = false;
                    }

                    // Label 보이게 하기 
                    OtherLabel.Visible = true;
                }
            }
            if (StudentManager.StudentDic.TryGetValue("127.0.0.3", out StudentData StudentData_2) && lock2)
            {
                Other_Players.Add(new Player(StudentData_2.clr));
                lock2 = false;
            }
            if(StudentManager.StudentDic.TryGetValue("127.0.0.4", out StudentData) && lock3)
            {
                Other_Players.Add(new Player(StudentData.clr));
                lock3 = false;
            }
        }

        private void LoadImages() /* 이미지 불러오기 */
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
            Me = new Player(clr);
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
            // 추가 location
            MyLabel.Location = new Point(Me.location.X, Me.location.Y + 50);
            MyLabel.Visible = true;

        }
        private void Other_Move()
        {
            if (StudentManager.StudentDic.TryGetValue("127.0.0.1", out StudentData) && lock1 == false)
            {
                if (StudentData.Key.Go_Up && (Other_player.location.Y > 0))
                {
                    Other_player.location.Y -= MoveStep;
                    oth_AnimatePlayer(0, 2);
                    OtherBubble.Visible = false;
                }
                else if (StudentData.Key.Go_Down && (Other_player.location.Y + Other_player.Height < this.ClientSize.Height))
                {
                    Other_player.location.Y += MoveStep;
                    oth_AnimatePlayer(3, 5);
                    OtherBubble.Visible = false;

                }
                else if (StudentData.Key.Go_Left && Other_player.location.X > 0)
                {
                    Other_player.location.X -= MoveStep;
                    oth_AnimatePlayer(6, 8);
                    OtherBubble.Visible = false;

                }
                else if (StudentData.Key.Go_Right && (Other_player.location.X + Other_player.Width < this.ClientSize.Width))
                {
                    Other_player.location.X += MoveStep;
                    oth_AnimatePlayer(9, 11);
                    OtherBubble.Visible = false;

                }
                else if (StudentData.Key.Go_Up == false && StudentData.Key.Go_Down == false && StudentData.Key.Go_Left == false && StudentData.Key.Go_Right == false)
                {
                    if (Other_player != null && StudentData.Location.X != -1 && StudentData.Location.Y != -1)
                    {
                        Other_player.location.X = StudentData.Location.X;
                        Other_player.location.Y = StudentData.Location.Y;
                    }
                }
               
                // 추가 
                OtherLabel.Location = new Point(Other_player.location.X, Other_player.location.Y +50);
                OtherLabel.Visible = true;
            }
        }
        private void In_Game_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up) { Me.Go_Up = true; key.Go_Up = true; key.Go_Down = false; key.Go_Left = false; key.Go_Right = false; ChatBubble.Hide(); SendKeyDown(); }
            else if (e.KeyCode == Keys.Down) { Me.Go_Down = true; key.Go_Up = false; key.Go_Down = true; key.Go_Left = false; key.Go_Right = false; ChatBubble.Hide(); SendKeyDown(); }
            else if (e.KeyCode == Keys.Left) { Me.Go_Left = true; key.Go_Up = false; key.Go_Down = false; key.Go_Left = true; key.Go_Right = false; ChatBubble.Hide(); SendKeyDown(); }
            else if (e.KeyCode == Keys.Right) { Me.Go_Right = true; key.Go_Up = false; key.Go_Down = false; key.Go_Left = false; key.Go_Right = true; ChatBubble.Hide(); SendKeyDown(); }
            else if ((e.KeyCode == Keys.Space) && Space_Up || Space_Down || Space_Left || Space_Right)
            {
                Me.Go_Up = false; Me.Go_Down = false; Me.Go_Left = false; Me.Go_Right = false;
                if (Ob_Name == "start")
                {
                    /* 게임 시작 */
                    First first = new First();
                    first.Show();
                    this.Hide();
                }
                else { MessageBox.Show(Ob_Name); }
            }
            else if (e.KeyCode == Keys.Tab)
            {
                chatForm.Show();
            }
            else if (e.KeyCode == Keys.Escape)
            {
                chatForm.Hide();
            }
        }
        public void Create_My_Bubble()
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
                catch (Exception ex) { MessageBox.Show("오류 시발 3번 " + ex.Message); return; }

            }
            // 여기서 부터는 예전 것
                //bubbleChat.chat = inputchat;
            //ChatBubble.Visible = true;
            //ChatBubble.Location = new Point(Me.location.X, Me.location.Y + Me.Height - 100);
            //ChatBubble.Image = CreateTextImage(inputchat);
            //MainClient.SendData(bubbleChat,PacketType.AboutChat, "127.0.0.1");

        }
        public void Create_Other_Bubble()
        {
            if (StudentManager.StudentDic.TryGetValue("127.0.0.1", out StudentData) && lock1 == false)
            {

                if(StudentData.bubblechat == null || !StudentData.bubblechat.HasBeenUpdated)
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
                catch (Exception ex) { MessageBox.Show("오류 시발 3번 " + ex.Message ); return; }
            }
            //OtherBubble.Visible = true;
            //OtherBubble.Location = new Point(Other_player.location.X, Other_player.location.Y + Other_player.Height - 100);
            //ChatBubble.Image = CreateTextImage(other_input_chat);
        }
        private Image CreateTextImage(string text)
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
        private void oth_AnimatePlayer(int start, int end)
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
    }
}


