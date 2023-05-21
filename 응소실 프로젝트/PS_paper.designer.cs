namespace 로그인화면
{
    partial class PS_paper
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PS_paper));
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.start = new System.Windows.Forms.PictureBox();
            this.answerTxt = new Guna.UI2.WinForms.Guna2TextBox();
            this.answerBtn = new Guna.UI2.WinForms.Guna2Button();
            ((System.ComponentModel.ISupportInitialize)(this.start)).BeginInit();
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("궁서", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label1.Location = new System.Drawing.Point(135, 188);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 20);
            this.label1.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("궁서", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label2.Location = new System.Drawing.Point(37, 286);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(0, 18);
            this.label2.TabIndex = 1;
            // 
            // start
            // 
            this.start.BackColor = System.Drawing.Color.Transparent;
            this.start.Image = ((System.Drawing.Image)(resources.GetObject("start.Image")));
            this.start.Location = new System.Drawing.Point(291, 558);
            this.start.Name = "start";
            this.start.Size = new System.Drawing.Size(174, 66);
            this.start.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.start.TabIndex = 2;
            this.start.TabStop = false;
            this.start.Click += new System.EventHandler(this.start_Click_1);
            // 
            // answerTxt
            // 
            this.answerTxt.BorderColor = System.Drawing.Color.White;
            this.answerTxt.BorderThickness = 0;
            this.answerTxt.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.answerTxt.DefaultText = "";
            this.answerTxt.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.answerTxt.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.answerTxt.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.answerTxt.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.answerTxt.FillColor = System.Drawing.Color.Gainsboro;
            this.answerTxt.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.answerTxt.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.answerTxt.ForeColor = System.Drawing.Color.Black;
            this.answerTxt.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.answerTxt.Location = new System.Drawing.Point(72, 534);
            this.answerTxt.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.answerTxt.Name = "answerTxt";
            this.answerTxt.PasswordChar = '\0';
            this.answerTxt.PlaceholderForeColor = System.Drawing.Color.Gray;
            this.answerTxt.PlaceholderText = "정답";
            this.answerTxt.SelectedText = "";
            this.answerTxt.Size = new System.Drawing.Size(97, 40);
            this.answerTxt.TabIndex = 3;
            // 
            // answerBtn
            // 
            this.answerBtn.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.answerBtn.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.answerBtn.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.answerBtn.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.answerBtn.FillColor = System.Drawing.Color.Gainsboro;
            this.answerBtn.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.answerBtn.ForeColor = System.Drawing.Color.Black;
            this.answerBtn.Location = new System.Drawing.Point(200, 534);
            this.answerBtn.Name = "answerBtn";
            this.answerBtn.Size = new System.Drawing.Size(66, 40);
            this.answerBtn.TabIndex = 4;
            this.answerBtn.Text = "입력";
            this.answerBtn.Click += new System.EventHandler(this.answerBtn_Click);
            // 
            // PS_paper
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.BackgroundImage = global::로그인화면.Properties.Resources.paper;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(500, 650);
            this.Controls.Add(this.answerBtn);
            this.Controls.Add(this.answerTxt);
            this.Controls.Add(this.start);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "PS_paper";
            this.Text = "PS_First";
            this.TopMost = true;
            this.TransparencyKey = System.Drawing.Color.White;
            this.Load += new System.EventHandler(this.PS_Paper_Load);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.PS_paper_KeyPress);
            ((System.ComponentModel.ISupportInitialize)(this.start)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.PictureBox start;
        private Guna.UI2.WinForms.Guna2TextBox answerTxt;
        private Guna.UI2.WinForms.Guna2Button answerBtn;
    }
}