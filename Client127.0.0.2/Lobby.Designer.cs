namespace 로그인화면
{
    partial class Lobby
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Lobby));
            this.start = new System.Windows.Forms.PictureBox();
            this.obstacle2 = new System.Windows.Forms.PictureBox();
            this.obstacle1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.ChatBubble)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.start)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.obstacle2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.obstacle1)).BeginInit();
            this.SuspendLayout();
            // 
            // start
            // 
            this.start.BackColor = System.Drawing.Color.Transparent;
            this.start.Image = ((System.Drawing.Image)(resources.GetObject("start.Image")));
            this.start.Location = new System.Drawing.Point(769, 419);
            this.start.Name = "start";
            this.start.Size = new System.Drawing.Size(225, 118);
            this.start.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.start.TabIndex = 5;
            this.start.TabStop = false;
            this.start.Tag = "obstacle";
            // 
            // obstacle2
            // 
            this.obstacle2.BackColor = System.Drawing.Color.Transparent;
            this.obstacle2.BackgroundImage = global::로그인화면.Properties.Resources.Obstacle2;
            this.obstacle2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.obstacle2.Location = new System.Drawing.Point(184, 419);
            this.obstacle2.Name = "obstacle2";
            this.obstacle2.Size = new System.Drawing.Size(100, 50);
            this.obstacle2.TabIndex = 4;
            this.obstacle2.TabStop = false;
            this.obstacle2.Tag = "obstacle";
            // 
            // obstacle1
            // 
            this.obstacle1.BackColor = System.Drawing.Color.Transparent;
            this.obstacle1.BackgroundImage = global::로그인화면.Properties.Resources.Obstacle1;
            this.obstacle1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.obstacle1.Location = new System.Drawing.Point(701, 264);
            this.obstacle1.Name = "obstacle1";
            this.obstacle1.Size = new System.Drawing.Size(100, 50);
            this.obstacle1.TabIndex = 3;
            this.obstacle1.TabStop = false;
            this.obstacle1.Tag = "obstacle";
            // 
            // Lobby
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::로그인화면.Properties.Resources.Map;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1006, 721);
            this.Controls.Add(this.start);
            this.Controls.Add(this.obstacle2);
            this.Controls.Add(this.obstacle1);
            this.Name = "Lobby";
            this.Text = "Lobby";
            this.Controls.SetChildIndex(this.obstacle1, 0);
            this.Controls.SetChildIndex(this.obstacle2, 0);
            this.Controls.SetChildIndex(this.start, 0);
            this.Controls.SetChildIndex(this.ChatBubble, 0);
            this.Controls.SetChildIndex(this.MyLabel, 0);
            ((System.ComponentModel.ISupportInitialize)(this.ChatBubble)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.start)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.obstacle2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.obstacle1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox obstacle1;
        private System.Windows.Forms.PictureBox obstacle2;
        private System.Windows.Forms.PictureBox start;
    }
}