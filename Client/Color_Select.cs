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
    public partial class Select_Color : Form
    {
        private bool Mouse_Dragging = false;
        private Point Drag_Location;

        private string my_add;
        public Select_Color(string arg_add)
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            my_add = arg_add;
            DragBox.MouseDown += ChatForm_MouseDown;
            DragBox.MouseUp += ChatForm_MouseUp;
            DragBox.MouseMove += ChatForm_MouseMove;
        }

        private void button1_Click_1(object sender, EventArgs e) // Blakc btn 클릭 
        {

            if (Loader.Selected_Character[0])
            {
                BaseForm_test.Me = new Player(Ch_Color.Black, my_add);
                MainClient.SendData(Ch_Color.Black, PacketType.AboutCharacter, my_add);
                this.Close();
            }
            else { MessageBox.Show("이미 선택"); }
        }

        private void button2_Click_1(object sender, EventArgs e) // Orange btn 클릭
        {
            if (Loader.Selected_Character[1])
            {
                BaseForm_test.Me = new Player(Ch_Color.Orange, my_add);
                MainClient.SendData(Ch_Color.Orange, PacketType.AboutCharacter, my_add);
                this.Close();
            }
            else { MessageBox.Show("이미 선택"); }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (Loader.Selected_Character[2])
            {
                BaseForm_test.Me = new Player(Ch_Color.Green, my_add);
                MainClient.SendData(Ch_Color.Green, PacketType.AboutCharacter, my_add);
                this.Close();
            }
            else { MessageBox.Show("이미 선택"); }
        }
        private void button4_Click(object sender, EventArgs e)
        {
            if (Loader.Selected_Character[3])
            {
                BaseForm_test.Me = new Player(Ch_Color.Blue, my_add);
                MainClient.SendData(Ch_Color.Blue, PacketType.AboutCharacter, my_add);
                this.Close();
            }
            else { MessageBox.Show("이미 선택"); }
        }

        private void ChatForm_MouseDown(object sender, MouseEventArgs e)
        {
            Mouse_Dragging = true;
            Drag_Location = new Point(e.X, e.Y);
        }

        private void ChatForm_MouseMove(object sender, MouseEventArgs e)
        {
            if (Mouse_Dragging)
            {
                Point diff = new Point(e.X - Drag_Location.X, e.Y - Drag_Location.Y);
                this.Location = new Point(this.Location.X + diff.X, this.Location.Y + diff.Y);
            }
        }

        private void ChatForm_MouseUp(object sender, MouseEventArgs e)
        {
            Mouse_Dragging = false;
        }


        private void Select_Color_FormClosing(object sender, FormClosingEventArgs e)
        {
            //if (e.CloseReason == CloseReason.UserClosing) // x버튼을 눌러서 종료했을 때 실행
            //    Application.ExitThread(); Environment.Exit(0);

        }

    }
}
