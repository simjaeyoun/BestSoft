namespace ClientChat
{
    partial class Form1
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.ClientChatSendBox = new System.Windows.Forms.TextBox();
            this.Speechbubble = new System.Windows.Forms.PictureBox();
            this.ClientChatBoxLog = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.Speechbubble)).BeginInit();
            this.SuspendLayout();
            // 
            // ClientChatSendBox
            // 
            this.ClientChatSendBox.Location = new System.Drawing.Point(29, 485);
            this.ClientChatSendBox.Name = "ClientChatSendBox";
            this.ClientChatSendBox.Size = new System.Drawing.Size(421, 21);
            this.ClientChatSendBox.TabIndex = 1;
            // 
            // Speechbubble
            // 
            this.Speechbubble.Location = new System.Drawing.Point(681, 59);
            this.Speechbubble.Name = "Speechbubble";
            this.Speechbubble.Size = new System.Drawing.Size(203, 186);
            this.Speechbubble.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.Speechbubble.TabIndex = 2;
            this.Speechbubble.TabStop = false;
            // 
            // ClientChatBoxLog
            // 
            this.ClientChatBoxLog.Location = new System.Drawing.Point(29, 190);
            this.ClientChatBoxLog.Multiline = true;
            this.ClientChatBoxLog.Name = "ClientChatBoxLog";
            this.ClientChatBoxLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.ClientChatBoxLog.Size = new System.Drawing.Size(421, 296);
            this.ClientChatBoxLog.TabIndex = 3;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1023, 572);
            this.Controls.Add(this.ClientChatBoxLog);
            this.Controls.Add(this.Speechbubble);
            this.Controls.Add(this.ClientChatSendBox);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.Speechbubble)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox ClientChatSendBox;
        private System.Windows.Forms.PictureBox Speechbubble;
        private System.Windows.Forms.TextBox ClientChatBoxLog;
    }
}

