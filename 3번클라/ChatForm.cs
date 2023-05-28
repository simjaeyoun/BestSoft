﻿using System;
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
        // 실수로 챗 폼을 x누르면 사고나니 챗폼에 대해서는 위에 그것을 없애는 방법을 알려달라고 하자

        private In_Game in_game;
        public ChatForm(In_Game in_game)
        {
            InitializeComponent();

            this.in_game = in_game;
            //ChatInput.Focus();  

        }

        StudentData studentData;

        private BubbleChat sendBubbleChat = new BubbleChat { chat = "" };
        // 메세지를 hashset 형태로 저장해서 중복검사해서 log 에 추가
        private HashSet<string> existingMessages = new HashSet<string>();
        private void ChatInput_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
               
                ChatInput.Focus();
                string message = ChatInput.Text.Trim();
                if (!string.IsNullOrEmpty(message))
                {

                    sendBubbleChat.chat = message;

                    // 전체 채팅 로그에 메시지 추가
                    // server 를 기준으로 chat 추가 해야할 듯??0
                    in_game.Create_My_Bubble(message);
                    MainClient.SendData(sendBubbleChat, PacketType.AboutChat, "127.0.0.2");
                    // student 이름 추가
                    string name = "kimsangyun";
                    ChatLog.AppendText($"{DateTime.Now.ToShortTimeString() + " " + name}: {message}\r\n");

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
        public void ChatLog_Show(string name,string other_chat_log)
        {
            
                //string name = studentData.StudentName;
                //string Input_Chat_Log = studentData.bubblechat.chat;
                 
                ChatLog.AppendText($"{DateTime.Now.ToShortTimeString() +" "+ name}: {other_chat_log}\r\n");
                //in_game.Create_Other_Bubble(studentData.bubblechat.chat);

            
        }
        public bool ChatLog_Show(string other_chat_log)
        {

            //string name = studentData.StudentName;
            //string Input_Chat_Log = studentData.bubblechat.chat;
            string name = "LEEgyun";
            string timestamp = DateTime.Now.ToShortTimeString();
            string fullMessage = $"{timestamp + " " + name}: {other_chat_log}";

            if (existingMessages.Contains(fullMessage))
            {
                return false;
            }
            existingMessages.Add(fullMessage);

            ChatLog.AppendText(fullMessage + "\r\n");
            //ChatLog.AppendText($"{DateTime.Now.ToShortTimeString() + " " + name}: {other_chat_log}\r\n");
            //n_game.Create_Other_Bubble(studentData.bubblechat.chat);

            return true;
        }

    }
}
