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
    public partial class Form2 : Form
    {
        TextBox chatInput;
        TextBox chatLog;
        TextBox chatMessage;
        PictureBox chatBubble;

        public Form2()
        {
            // 폼의 크기 설정
            this.Size = new Size(1200, 800);

            // 폼의 최대 크기와 최소 크기에 대해서 설정가능
            //this.MinimumSize = new Size(400, 300);
            //this.MaximumSize = new Size(1024, 768); 
            chatInput = new TextBox
            {
                Location = new Point(10, 400),
                Size = new Size(300, 20),
            };
            chatInput.KeyDown += ChatInput_KeyDown;
            Controls.Add(chatInput);

            // 채팅 로그 컨트롤 설정
            chatLog = new TextBox
            {   
                Location = new Point(10, 10),
                Size = new Size(300, 390),
                Multiline = true,
                ReadOnly = true,
                ScrollBars = ScrollBars.Vertical,
            };
            Controls.Add(chatLog);

            // 채팅 말풍선 컨트롤 설정
            chatBubble = new PictureBox
            {
                Location = new Point(320, 250),
                Size = new Size(200, 150),
                // 이미지를 로드하려면 아래 주석을 해제하고 이미지 파일 경로를 수정하세요.
                // Image = Image.FromFile("./bubble.png"),
                BorderStyle = BorderStyle.FixedSingle,
                SizeMode = PictureBoxSizeMode.StretchImage,
                Image = Properties.Resources.bubble
            };
            //chatBubble.Image = Properties.Resources.bubble;
            
            Controls.Add(chatBubble);
            chatMessage = new TextBox
            {
                Location = new Point(320, 250),
                Size = new Size(150, 100),
                Multiline = true,
                ReadOnly = true
            };
            Controls.Add(chatMessage);
        }
        private void ChatInput_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Enter) // 채팅을 입력하고 전송
            {
                //chatBubble.Image = Properties.Resources.bubble;
                string message = chatInput.Text.Trim();

                if (!string.IsNullOrEmpty(message))
                {
                    // 전체 채팅 로그에 메시지 추가
                    chatLog.AppendText($"{DateTime.Now.ToShortTimeString()}: {message}\r\n");

                    // 전체 채팅 로그에 client 닉네임 추가
                    // chatLog.AppendText($"{닉네임}");

                    // 채팅 말풍선에 메시지 표시
                    chatBubble.Image = CreateTextImage(message);

                    // 채팅 입력 지우기
                    chatInput.Clear();

                }
            }
            else if (e.KeyCode == Keys.Up || e.KeyCode == Keys.Down || e.KeyCode == Keys.Left || e.KeyCode == Keys.Right) // 방향키를 눌렀을 때
            {
                // 말풍선에 있는 텍스트를 지우고 원래 말풍선으로 돌아감
                chatBubble.Image = Properties.Resources.bubble;
            }
        }
        private Image CreateTextImage(string text)
        {
            // 원본 이미지를 복사합니다.
            Image bubbleImage = (Image)Properties.Resources.bubble.Clone();
            // 캔버스 생성
            Bitmap bitmap = new Bitmap(chatBubble.Width, chatBubble.Height);
            Graphics graphics = Graphics.FromImage(bitmap);
            graphics.Clear(Color.Transparent);

            // 원본 이미지를 캔버스에 그립니다.
            graphics.DrawImage(bubbleImage, 0, 0, chatBubble.Width, chatBubble.Height);

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
            
