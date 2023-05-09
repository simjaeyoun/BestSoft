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
    public partial class First : Form
    {
        private Direction direction; // 방향
        private const int MoveStep = 7; // 스피드
        private int x, y;
        private bool Go_Up = true, Go_Down = true, Go_Left = true, Go_Right = true;
        // private int TabKeyCount = 0;
        //TextBox chatInput;
        //TextBox chatLog;
        //PictureBox chatBubble;

        public First()
        {
            InitializeComponent();
            player.Image = In_Game.images[(int)In_Game.clr, (int)Direction.Down];
            x = player.Location.X;
            y = player.Location.Y;
            //SetStyle(ControlStyles.SupportsTransparentBackColor | ControlStyles.UserPaint, true);
            //BackColor = Color.Transparent;
            //chatInput = new TextBox
            //{
            //    Location = new Point(10, 400),
            //    Size = new Size(300, 20),
            //    Visible = false
            //};
            //chatInput.KeyDown += In_Game_KeyDown;
            //Controls.Add(chatInput);
            //chatLog = new TextBox
            //{
            //    Location = new Point(10, 10),
            //    Size = new Size(300, 390),
            //    Multiline = true,
            //    ReadOnly = true,
            //    ScrollBars = ScrollBars.Vertical,
            //    Visible = false

            //};
            //Controls.Add(chatLog);
            //chatBubble = new PictureBox
            //{
            //    Location = new Point(320, 250),
            //    Size = new Size(200, 150),
            //    // 이미지를 로드하려면 아래 주석을 해제하고 이미지 파일 경로를 수정하세요.
            //    // Image = Image.FromFile("./bubble.png"),
            //    BorderStyle = BorderStyle.FixedSingle,
            //    SizeMode = PictureBoxSizeMode.StretchImage,
            //    //Image = Properties.Resources.bubble
            //    Image = null,
            //    Visible = false

            //};
            //Controls.Add(chatBubble);

        }
        //protected override void OnPaint(PaintEventArgs e)
        //{
        //    using (SolidBrush brush = new SolidBrush(ForeColor))
        //    {
        //        e.Graphics.DrawString(Text, Font, brush, new PointF(0, 0));
        //    }
        //}

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
        //private void ChatInput_TextChanged(object sender, EventArgs e)
        //{
        //    ChatInput.SelectionStart = 0;
        //}


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

        // 추석 처리된 chatBubble.Image = Properties.Resources.bubble; 나중에 이미지 추가하고 풀기

        private void In_Game_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Up:
                    if (Go_Up)
                    {
                        y -= MoveStep; SetDirection(Direction.Up);
                        ChatBubble.Image = null;

                        ChatBubble.Visible = false;

                    }
                    //chatBubble.Image = Properties.Resources.bubble;

                    break;
                case Keys.Down:
                    if (Go_Down)
                    {
                        y += MoveStep; SetDirection(Direction.Down);
                        ChatBubble.Image = null;

                        ChatBubble.Visible = false;

                    }
                    //chatBubble.Image = Properties.Resources.bubble;

                    break;
                case Keys.Left:
                    if (Go_Left)
                    {
                        x -= MoveStep; SetDirection(Direction.Left);
                        ChatBubble.Image = null;

                        ChatBubble.Visible = false;

                    }
                    //chatBubble.Image = Properties.Resources.bubble;

                    break;
                case Keys.Right:
                    if (Go_Right)
                    {
                        x += MoveStep; SetDirection(Direction.Right);
                        ChatBubble.Image = null;

                        ChatBubble.Visible = false;

                    }

                    //chatBubble.Image = Properties.Resources.bubble;
                    break;
                case Keys.Tab:

                    ChatInput.Visible = true;
                    ChatInput.BackColor = this.BackColor;
                    ChatLog.Visible = true;
                    ChatLog.BackColor = this.BackColor;
                    break;
                case Keys.Escape:
                    ChatInput.Visible = false;
                    ChatLog.Visible = false;
                    break;
                case Keys.Enter:
                    string message = ChatInput.Text.Trim();
                    if (!string.IsNullOrEmpty(message))
                    {
                        // 전체 채팅 로그에 메시지 추가
                        ChatLog.AppendText($"{DateTime.Now.ToShortTimeString() +" "+ "김동현"}: {message}\r\n");

                        // 전체 채팅 로그에 client 닉네임 추가
                       // ChatLog.AppendText($"김동현");

                        // 말풍선 보이게 하기
                        ChatBubble.Visible = true;
                        // 말풍선 위치 설정
                        ChatBubble.Location = new Point(player.Left, player.Top - 50);
                        // 채팅 말풍선에 메시지 표시

                        ChatBubble.Image = CreateTextImage(message);

                        // 채팅 입력 지우기
                        ChatInput.Clear();
                        // ChatLog.Visible = false;

                    }
                    break;
                default:
                    break;


            }
            if (x >= 0 && x + player.Width <= ClientSize.Width && // form 화면을 벗어나지 않게 함
                y >= 0 && y + player.Height <= ClientSize.Height)
            {
                player.Left = x;
                player.Top = y;
            }
            //player.Location = new Point(x, y);
            Check_Question(e);
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
                            if ((string)control.Name == "doorQ")
                            {
                                First_passwd Fp = new First_passwd();
                                Fp.ShowDialog();
                            }
                            else if ((string)control.Name == "desk_memo")
                            {
                                desk_pw dp = new desk_pw();
                                dp.ShowDialog();
                            }
                            else if ((string)control.Name == "board")
                            {
                                board_pw bp = new board_pw();
                                bp.ShowDialog();
                            }
                            else if ((string)control.Name == "bookshelfQ")
                            {
                                bookshelf_pw bookshelf = new bookshelf_pw();
                                bookshelf.ShowDialog();
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
