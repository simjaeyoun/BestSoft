namespace 로그인화면
{
    partial class PS_Final
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
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.box1 = new System.Windows.Forms.PictureBox();
            this.box2 = new System.Windows.Forms.PictureBox();
            this.box3 = new System.Windows.Forms.PictureBox();
            this.pictureBox5 = new System.Windows.Forms.PictureBox();
            this.pictureBox6 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.forEscape = new System.Windows.Forms.PictureBox();
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.DragBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ChatBubble)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.OtherBubble)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.box1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.box2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.box3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.forEscape)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.Location = new System.Drawing.Point(3, 4);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(999, 82);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Tag = "obstacle";
            // 
            // box1
            // 
            this.box1.BackColor = System.Drawing.Color.Transparent;
            this.box1.Image = global::로그인화면.Properties.Resources.box;
            this.box1.Location = new System.Drawing.Point(80, 293);
            this.box1.Name = "box1";
            this.box1.Size = new System.Drawing.Size(170, 170);
            this.box1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.box1.TabIndex = 1;
            this.box1.TabStop = false;
            this.box1.Tag = "obstacle";
            // 
            // box2
            // 
            this.box2.BackColor = System.Drawing.Color.Transparent;
            this.box2.Image = global::로그인화면.Properties.Resources.box;
            this.box2.Location = new System.Drawing.Point(392, 293);
            this.box2.Name = "box2";
            this.box2.Size = new System.Drawing.Size(170, 170);
            this.box2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.box2.TabIndex = 2;
            this.box2.TabStop = false;
            this.box2.Tag = "obstacle";
            // 
            // box3
            // 
            this.box3.BackColor = System.Drawing.Color.Transparent;
            this.box3.Image = global::로그인화면.Properties.Resources.box;
            this.box3.Location = new System.Drawing.Point(713, 293);
            this.box3.Name = "box3";
            this.box3.Size = new System.Drawing.Size(170, 170);
            this.box3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.box3.TabIndex = 3;
            this.box3.TabStop = false;
            this.box3.Tag = "obstacle";
            // 
            // pictureBox5
            // 
            this.pictureBox5.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox5.Image = global::로그인화면.Properties.Resources.item;
            this.pictureBox5.Location = new System.Drawing.Point(173, 12);
            this.pictureBox5.Name = "pictureBox5";
            this.pictureBox5.Size = new System.Drawing.Size(68, 80);
            this.pictureBox5.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox5.TabIndex = 4;
            this.pictureBox5.TabStop = false;
            // 
            // pictureBox6
            // 
            this.pictureBox6.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox6.Image = global::로그인화면.Properties.Resources.item;
            this.pictureBox6.Location = new System.Drawing.Point(247, 12);
            this.pictureBox6.Name = "pictureBox6";
            this.pictureBox6.Size = new System.Drawing.Size(68, 80);
            this.pictureBox6.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox6.TabIndex = 5;
            this.pictureBox6.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.White;
            this.label1.Font = new System.Drawing.Font("맑은 고딕", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.MidnightBlue;
            this.label1.Location = new System.Drawing.Point(12, 677);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(527, 23);
            this.label1.TabIndex = 17;
            this.label1.Text = "해적선을 탈출할 준비가 되었다면 손잡이 앞에서 space를 누르세요!";
            // 
            // forEscape
            // 
            this.forEscape.BackColor = System.Drawing.Color.Transparent;
            this.forEscape.Location = new System.Drawing.Point(419, 42);
            this.forEscape.Name = "forEscape";
            this.forEscape.Size = new System.Drawing.Size(160, 50);
            this.forEscape.TabIndex = 18;
            this.forEscape.TabStop = false;
            this.forEscape.Tag = "obstacle";
            // 
            // timer2
            // 
            this.timer2.Enabled = true;
            this.timer2.Tick += new System.EventHandler(this.timer2_Tick);
            // 
            // PS_Final
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::로그인화면.Properties.Resources.PS_Final;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1024, 768);
            this.Controls.Add(this.forEscape);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureBox6);
            this.Controls.Add(this.pictureBox5);
            this.Controls.Add(this.box3);
            this.Controls.Add(this.box2);
            this.Controls.Add(this.box1);
            this.Controls.Add(this.pictureBox1);
            this.Name = "PS_Final";
            this.Text = "PS_Final";
            this.Load += new System.EventHandler(this.PS_Final_Load);
            this.Controls.SetChildIndex(this.pictureBox1, 0);
            this.Controls.SetChildIndex(this.box1, 0);
            this.Controls.SetChildIndex(this.box2, 0);
            this.Controls.SetChildIndex(this.box3, 0);
            this.Controls.SetChildIndex(this.DragBox, 0);
            this.Controls.SetChildIndex(this.pictureBox5, 0);
            this.Controls.SetChildIndex(this.pictureBox6, 0);
            this.Controls.SetChildIndex(this.label1, 0);
            this.Controls.SetChildIndex(this.forEscape, 0);
            this.Controls.SetChildIndex(this.OtherBubble, 0);
            this.Controls.SetChildIndex(this.ChatBubble, 0);
            this.Controls.SetChildIndex(this.OtherLabel, 0);
            this.Controls.SetChildIndex(this.MyLabel, 0);
            this.Controls.SetChildIndex(this.Time_Label, 0);
            ((System.ComponentModel.ISupportInitialize)(this.DragBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ChatBubble)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.OtherBubble)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.box1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.box2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.box3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.forEscape)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox box1;
        private System.Windows.Forms.PictureBox box2;
        private System.Windows.Forms.PictureBox box3;
        private System.Windows.Forms.PictureBox pictureBox5;
        private System.Windows.Forms.PictureBox pictureBox6;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox forEscape;
        public System.Windows.Forms.Timer timer2;
    }
}