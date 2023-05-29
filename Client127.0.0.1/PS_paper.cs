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
    public partial class PS_paper : Form
    {
        private string TitleInPaper;
        private string ContentInPaper;
        private int flag;
        private int index1 = 0;
        private int index2 = 0;

        public PS_paper(string txt1, string txt2, int paperFlag)
        {
            InitializeComponent();
            TitleInPaper = txt1;
            ContentInPaper = txt2;
            flag = paperFlag;
            if (flag == 3)
                label1.Location = new Point(90, 150);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (index1 < TitleInPaper.Length) // 방탈출 이름 출력
            {
                label1.Text = string.Concat(label1.Text, TitleInPaper[index1].ToString());
                index1++;
            }
            else if (index2 < ContentInPaper.Length) // 방탈출 소개 출력
            {
                label2.Text = string.Concat(label2.Text, ContentInPaper[index2].ToString());
                index2++;
            }
            else
            {
                if (flag == 1)
                    start.Show();
                else if (flag == 2)
                    label3.Text = "(Space)키를누르면 시작합니다.";
                else
                {
                    answerBtn.Show();
                    answerTxt.Show();
                }
            }
        }

        private void PS_Paper_Load(object sender, EventArgs e)
        {
            timer1.Interval = 30;
            timer1.Enabled = true;
            start.Hide();
            answerBtn.Hide();
            answerTxt.Hide();
        }

        private void start_Click_1(object sender, EventArgs e)
        {
            PS_First pS_First = new PS_First();
            pS_First.Show();
            timer1.Stop();
            this.Close();
        }

        private void PS_paper_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Space)
            {
                if (index2 < ContentInPaper.Length)
                {
                    index1 = TitleInPaper.Length; index2 = ContentInPaper.Length;
                    label1.Text = TitleInPaper;
                    label2.Text = ContentInPaper;
                }
                else if (flag != 1)
                {
                    timer1.Stop();
                    this.Close();
                }
            }
        }

        private void answerBtn_Click(object sender, EventArgs e)
        {
            if (answerTxt.Text == "콜린")
            {
                DialogResult result = MessageBox.Show("정답입니다!\n\n성냥에 다시 접근하면 감옥 안의 불이 켜집니다!", "", MessageBoxButtons.OK);
                if (result == DialogResult.OK)
                {
                    PS_First pS_First = (PS_First)Application.OpenForms["PS_First"];
                    //pS_First.checkBright = true;
                    pS_First.match.Name = "On_match";
                    timer1.Stop();
                    this.Close();
                }
            }
            else
                MessageBox.Show("정답이 아닙니다.");
        }
    }
}
