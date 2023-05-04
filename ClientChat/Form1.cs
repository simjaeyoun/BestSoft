using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClientChat
{
    public partial class Form1 : Form
    {
        TextBox chatInput;
        TextBox chatLog;
        PictureBox chatBubble;

        public Form1()
        {
            //InitializeComponent();
            chatInput = new TextBox
            {
                Location = new Point(10, 400),
                Size = new Size(300, 20),
            };
            chatInput.KeyDown += ChatInput_KeyDown;
            Controls.Add(chatInput);
            chatLog = new TextBox
            {
                Location = new Point(10, 10),
                Size = new Size(300, 380),
                Multiline = true,
                ReadOnly = true,
                ScrollBars = ScrollBars.Vertical,
            };
            Controls.Add(chatLog);
            chatBubble = new PictureBox
            {
                Location = new Point(320, 250),
                Size = new Size(150, 60),
                // 이미지를 로드하려면 아래 주석을 해제하고 이미지 파일 경로를 수정하세요.
                Image = Image.FromFile("./chatbubble.png"),
                BorderStyle = BorderStyle.FixedSingle,
            };
            Controls.Add(chatBubble);
        }


        private void ChatInput_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) // 채팅을 입력하고 전송
            {
                string message = chatInput.Text.Trim();

                if (!string.IsNullOrEmpty(message))
                {
                    // 전체 채팅 로그에 메시지 추가
                    chatLog.AppendText($"{DateTime.Now.ToShortTimeString()}: {message}\r\n");

                    // 채팅 말풍선에 메시지 표시
                    chatBubble.Image = CreateTextImage(message);

                    // 채팅 입력 지우기
                    chatInput.Clear();
                }
            }
            else if (e.KeyCode == Keys.Up || e.KeyCode == Keys.Down) // 방향키를 눌렀을 때
            {
                // 말풍선에 있는 텍스트를 지우고 원래 말풍선으로 돌아감
                chatBubble.Image = null;
            }
        }
        private Image CreateTextImage(string text)
        {
            // 캔버스 생성
            Bitmap bitmap = new Bitmap(chatBubble.Width, chatBubble.Height);
            Graphics graphics = Graphics.FromImage(bitmap);
            graphics.Clear(Color.Transparent);

            // 텍스트 출력 설정
            Font font = new Font("Arial", 12);
            SolidBrush brush = new SolidBrush(Color.Black);
            StringFormat stringFormat = new StringFormat
            {
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center
            };

            // 텍스트를 이미지로 그리기
            graphics.DrawString(text, font, brush, new RectangleF(0, 0, chatBubble.Width, chatBubble.Height), stringFormat);

            // 리소스 정리
            brush.Dispose();
            graphics.Dispose();

            return bitmap;
        }




    }
}
