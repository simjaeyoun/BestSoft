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

        }

        private BubbleChat sendBubbleChat = new BubbleChat { chat = "", HasBeenUpdated = false };

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
                    string fullMessage = $"{timestamp + " " + name}: {message}";
                    sendBubbleChat.chat = fullMessage;
                    sendBubbleChat.HasBeenUpdated = true;

                    MainClient.SendData(sendBubbleChat, PacketType.AboutChat, "127.0.0.1");

                    ChatInput.Clear();
                }
            }
        }
    }
}
