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
    public partial class ChatForm : Form
    {
        public ChatForm()
        {
            InitializeComponent();
            ChatInput.Focus();
            this.Load += ChatForm_Load;
            //this.KeyDown += ChatInput_KeyDown;
            //this.in_game = in_game;
            //ChatInput.Focus();  

        }

        //StudentData studentData;

        private BubbleChat sendBubbleChat = new BubbleChat { InputChat = "", LogChat = "", HasBeenUpdated = false };
        // 2023.05.29 아래에 있는 주석 삭제 해도 상관X 
        // 메세지를 hashset 형태로 저장해서 중복검사해서 log 에 추가
        //private HashSet<string> Other_existingMessages = new HashSet<string>();
        //private HashSet<string> My_existingMessages = new HashSet<string>();;
        
        
        private void ChatInput_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ChatInput.Focus();
                string message = ChatInput.Text.Trim();
                if (!string.IsNullOrEmpty(message))
                {

                    string name = "LEEgyun";
                    string timestamp = DateTime.Now.ToShortTimeString();
                    sendBubbleChat.InputChat = message;
                    sendBubbleChat.LogChat = timestamp;
                    sendBubbleChat.HasBeenUpdated = true;
                    // 전체 채팅 로그에 메시지 추가
                    // server 를 기준으로 chat 추가 해야할 듯??0

                    // in_game.Create_My_Bubble(message);
                    //if (My_existingMessages.Contains(message))
                    //{
                    //    return ;
                    //}
                    //My_existingMessages.Add(message);   
                    MainClient.SendData(sendBubbleChat, PacketType.AboutChat, "127.0.0.1");
                    // student 이름 추가

                    //ChatLog.AppendText($"{DateTime.Now.ToShortTimeString() + " " + name}: {message}\r\n");

                    // 버블 만드는 것을 잠시 위로 올려본다
                    //in_game.Create_My_Bubble(message);

                    //ChatLog_Show();


                    // 말풍선 보이게 하기
                    //ChatBubble.Visible = true;
                    // 말풍선 위치 설정s
                    //ChatBubble.Location = new Point(player.Left, player.Top - 50);
                    // 채팅 말풍선에 메시지 표시

                    //ChatBubble.Image = CreateTextImage(message);

                    // 채팅 입력 지우기
                    ChatInput.Clear();
                    // ChatLog.Visible = false
                }
            }
        }
        
        public void LogAppend(string Time, int flag)
        {
            if (flag == 1)
            {
                MyChat myChat = new MyChat();
                myChat.TopLevel = false;
                myChat.message.Text = BaseForm_test.StudentData.bubblechat.InputChat;
                myChat.time.Text = Time;
                myChat.Dock = DockStyle.Right;
                ChatLog.Controls.Add(myChat);
                myChat.Show();
            }
            else
            {
                OtherChat otherChat = new OtherChat();
                otherChat.TopLevel = false;
                otherChat.name.Text = BaseForm_test.StudentData.StudentName;
                otherChat.profile.Image = BaseForm_test.StudentData.profile;
                otherChat.message.Text = BaseForm_test.StudentData.bubblechat.InputChat;
                otherChat.time.Text = Time;
                ChatLog.Controls.Add(otherChat);
                otherChat.Show();
            }
            ChatLog.AutoScrollPosition = new Point(0, ChatLog.VerticalScroll.Maximum);
        }

        private void ChatForm_Load(object sender, EventArgs e)
        {
            chatProfile.Image = BaseForm_test.My_StudentData.profile;
            namelbl.Text = BaseForm_test.My_StudentData.StudentName;
        }

    }
}