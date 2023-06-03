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
            this.components = new System.ComponentModel.Container();
            this.start = new System.Windows.Forms.PictureBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.ChatBubble)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.OtherBubble)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.start)).BeginInit();
            this.SuspendLayout();
            // 
            // start
            // 
            this.start.BackColor = System.Drawing.Color.Transparent;
            this.start.Image = global::로그인화면.Properties.Resources.start1;
            this.start.Location = new System.Drawing.Point(372, 611);
            this.start.Name = "start";
            this.start.Size = new System.Drawing.Size(275, 110);
            this.start.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.start.TabIndex = 5;
            this.start.TabStop = false;
            this.start.Tag = "obstacle";
            // 
            // Lobby
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::로그인화면.Properties.Resources.lobby;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1006, 721);
            this.Controls.Add(this.start);
            this.Name = "Lobby";
            this.Text = "Lobby";
            this.Controls.SetChildIndex(this.OtherBubble, 0);
            this.Controls.SetChildIndex(this.OtherLabel, 0);
            this.Controls.SetChildIndex(this.start, 0);
            this.Controls.SetChildIndex(this.ChatBubble, 0);
            this.Controls.SetChildIndex(this.MyLabel, 0);
            ((System.ComponentModel.ISupportInitialize)(this.ChatBubble)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.OtherBubble)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.start)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.PictureBox start;
        private System.Windows.Forms.Timer timer1;
    }
}