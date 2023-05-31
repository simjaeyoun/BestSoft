using Guna.UI2.AnimatorNS;
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
    public partial class PS_hint : Form
    {
        private string content;
        private int flag;
        public PS_hint(string content, int flag)
        {
            InitializeComponent();
            this.content = content;
            this.flag = flag;
        }


        private void PS_hint_Load(object sender, EventArgs e)
        {
            label1.Text = content;
            if (flag != 3)
            {
                groupBox1.Hide();
                if (flag == 1)
                {
                    answerBtn.Hide();
                    answerTxt.Hide();
                }

            }
        }

        private void PS_hint_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Space)
                this.Close();
        }

        private void answerBtn_Click(object sender, EventArgs e)
        {
            if (flag == 2)
            {
                if (answerTxt.Text == "1597")
                {
                    DialogResult result = MessageBox.Show("정답입니다!\n\n밧줄을 이용하여 선장실로 이동하십시오", "", MessageBoxButtons.OK);
                    if (result == DialogResult.OK)
                    {
                        this.Close();
                        PS_First pS_First = (PS_First)Application.OpenForms["PS_First"];
                        pS_First.forRope.Tag = "not_obstacle";
                        pS_First.rope.Tag = "obstacle";
                        pS_First.rope.Show();

                        BaseForm_test.Me.Info.ObstacleName = "forRope_by_other";
                        BaseForm_test.Me.Info.result = true;
                        MainClient.SendData(BaseForm_test.Me.Info, PacketType.AboutNext, "127.0.0.2");
                    }
                }
                else
                    MessageBox.Show("정답이 아닙니다.");
            }
            else if (flag == 3)
            {
                if (answerTxt.Text == "1956")
                {
                    DialogResult result = MessageBox.Show("정답입니다!\n\n밧줄을 이용하여 갑판으로 이동하십시오", "", MessageBoxButtons.OK);
                    if (result == DialogResult.OK)
                    {
                        this.Close();
                        PS_Second pS_Second = (PS_Second)Application.OpenForms["PS_Second"];
                        pS_Second.rope.Tag = "obstacle";
                        pS_Second.rope.Show();

                        BaseForm_test.Me.Info.ObstacleName = "smallMap_by_other";
                        BaseForm_test.Me.Info.result = true;
                        MainClient.SendData(BaseForm_test.Me.Info, PacketType.AboutNext, "127.0.0.2");
                    }
                }
                else
                    MessageBox.Show("정답이 아닙니다.");
            }
            else if (flag == 4)
            {
                if (answerTxt.Text == "3")
                {
                    DialogResult result = MessageBox.Show("정답입니다!", "", MessageBoxButtons.OK);
                    if (result == DialogResult.OK)
                    {
                        this.Close();
                        PS_Second pS_Second = (PS_Second)Application.OpenForms["PS_Second"];
                        pS_Second.smallMap.Tag = "obstacle";
                        pS_Second.smallMap.Show();

                        BaseForm_test.Me.Info.ObstacleName = "barrel1_by_other";
                        BaseForm_test.Me.Info.result = true;
                        MainClient.SendData(BaseForm_test.Me.Info, PacketType.AboutNext, "127.0.0.2");
                    }
                }
                else
                    MessageBox.Show("정답이 아닙니다.");
            }
            else if (flag == 5)
            {
                if (answerTxt.Text == "H212")
                {
                    DialogResult result = MessageBox.Show("정답입니다!", "", MessageBoxButtons.OK);
                    if (result == DialogResult.OK)
                    {
                        this.Close();
                        PS_Final pS_Final = (PS_Final)Application.OpenForms["PS_Final"];
                        pS_Final.timer2.Start();
                        MessageBox.Show("방탈출에 성공했습니다!");


                        BaseForm_test.Me.Info.ObstacleName = "forEscape_by_other";
                        BaseForm_test.Me.Info.result = true;
                        MainClient.SendData(BaseForm_test.Me.Info, PacketType.AboutNext, "127.0.0.2");

                        //Application.Exit();
                    }
                }
                else
                    MessageBox.Show("정답이 아닙니다.");
            }

        }
    }
}