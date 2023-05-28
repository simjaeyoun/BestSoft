namespace 로그인화면
{
    partial class In_Game
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(In_Game));
            this.obstacle1 = new System.Windows.Forms.PictureBox();
            this.obstacle2 = new System.Windows.Forms.PictureBox();
            this.start = new System.Windows.Forms.PictureBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.ChatBubble = new System.Windows.Forms.PictureBox();
            this.OtherBubble = new System.Windows.Forms.PictureBox();
            this.MyLabel = new System.Windows.Forms.Label();
            this.OtherLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.obstacle1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.obstacle2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.start)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ChatBubble)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.OtherBubble)).BeginInit();
            this.SuspendLayout();
            // 
            // obstacle1
            // 
            this.obstacle1.BackColor = System.Drawing.Color.Transparent;
            this.obstacle1.BackgroundImage = global::로그인화면.Properties.Resources.Obstacle1;
            this.obstacle1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.obstacle1.Location = new System.Drawing.Point(656, 191);
            this.obstacle1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.obstacle1.Name = "obstacle1";
            this.obstacle1.Size = new System.Drawing.Size(88, 40);
            this.obstacle1.TabIndex = 1;
            this.obstacle1.TabStop = false;
            this.obstacle1.Tag = "obstacle";
            // 
            // obstacle2
            // 
            this.obstacle2.BackColor = System.Drawing.Color.Transparent;
            this.obstacle2.BackgroundImage = global::로그인화면.Properties.Resources.Obstacle2;
            this.obstacle2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.obstacle2.Location = new System.Drawing.Point(198, 311);
            this.obstacle2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.obstacle2.Name = "obstacle2";
            this.obstacle2.Size = new System.Drawing.Size(88, 40);
            this.obstacle2.TabIndex = 2;
            this.obstacle2.TabStop = false;
            this.obstacle2.Tag = "obstacle";
            // 
            // start
            // 
            this.start.BackColor = System.Drawing.Color.Transparent;
            this.start.Image = ((System.Drawing.Image)(resources.GetObject("start.Image")));
            this.start.Location = new System.Drawing.Point(257, 450);
            this.start.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.start.Name = "start";
            this.start.Size = new System.Drawing.Size(197, 94);
            this.start.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.start.TabIndex = 3;
            this.start.TabStop = false;
            this.start.Tag = "obstacle";
            // 
            // timer1
            // 
            this.timer1.Interval = 14;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // ChatBubble
            // 
            this.ChatBubble.BackColor = System.Drawing.SystemColors.Window;
            this.ChatBubble.Location = new System.Drawing.Point(309, 126);
            this.ChatBubble.Name = "ChatBubble";
            this.ChatBubble.Size = new System.Drawing.Size(133, 41);
            this.ChatBubble.TabIndex = 4;
            this.ChatBubble.TabStop = false;
            this.ChatBubble.Visible = false;
            // 
            // OtherBubble
            // 
            this.OtherBubble.BackColor = System.Drawing.SystemColors.Window;
            this.OtherBubble.Location = new System.Drawing.Point(527, 113);
            this.OtherBubble.Name = "OtherBubble";
            this.OtherBubble.Size = new System.Drawing.Size(133, 41);
            this.OtherBubble.TabIndex = 5;
            this.OtherBubble.TabStop = false;
            this.OtherBubble.Visible = false;
            // 
            // MyLabel
            // 
            this.MyLabel.AutoSize = true;
            this.MyLabel.BackColor = System.Drawing.Color.Transparent;
            this.MyLabel.Location = new System.Drawing.Point(196, 191);
            this.MyLabel.Name = "MyLabel";
            this.MyLabel.Size = new System.Drawing.Size(54, 12);
            this.MyLabel.TabIndex = 7;
            this.MyLabel.Text = "MyLabel";
            this.MyLabel.Visible = false;
            // 
            // OtherLabel
            // 
            this.OtherLabel.AutoSize = true;
            this.OtherLabel.BackColor = System.Drawing.Color.Transparent;
            this.OtherLabel.Location = new System.Drawing.Point(456, 253);
            this.OtherLabel.Name = "OtherLabel";
            this.OtherLabel.Size = new System.Drawing.Size(66, 12);
            this.OtherLabel.TabIndex = 8;
            this.OtherLabel.Text = "OtherLabel";
            this.OtherLabel.Visible = false;
            // 
            // In_Game
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.BackgroundImage = global::로그인화면.Properties.Resources.Map;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(880, 577);
            this.Controls.Add(this.OtherLabel);
            this.Controls.Add(this.MyLabel);
            this.Controls.Add(this.OtherBubble);
            this.Controls.Add(this.ChatBubble);
            this.Controls.Add(this.start);
            this.Controls.Add(this.obstacle2);
            this.Controls.Add(this.obstacle1);
            this.DoubleBuffered = true;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "In_Game";
            this.Text = "In Game";
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.Paint_Character);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.In_Game_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.In_Game_KeyUp);
            ((System.ComponentModel.ISupportInitialize)(this.obstacle1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.obstacle2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.start)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ChatBubble)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.OtherBubble)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.PictureBox obstacle1;
        private System.Windows.Forms.PictureBox obstacle2;
        private System.Windows.Forms.PictureBox start;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.PictureBox ChatBubble;
        private System.Windows.Forms.PictureBox OtherBubble;
        private System.Windows.Forms.Label MyLabel;
        private System.Windows.Forms.Label OtherLabel;
    }
}