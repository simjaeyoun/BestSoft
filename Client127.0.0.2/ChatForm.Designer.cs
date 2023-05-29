namespace 로그인화면
{
    partial class ChatForm
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
            ChatLog = new System.Windows.Forms.TextBox();
            this.ChatInput = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // ChatLog
            // 
            ChatLog.Dock = System.Windows.Forms.DockStyle.Top;
            ChatLog.Location = new System.Drawing.Point(0, 0);
            ChatLog.Multiline = true;
            ChatLog.Name = "ChatLog";
            ChatLog.ReadOnly = true;
            ChatLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            ChatLog.Size = new System.Drawing.Size(798, 420);
            ChatLog.TabIndex = 0;
            ChatLog.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ChatInput_KeyDown);

            // 
            // ChatInput
            // 
            this.ChatInput.Location = new System.Drawing.Point(2, 414);
            this.ChatInput.Name = "ChatInput";
            this.ChatInput.Size = new System.Drawing.Size(796, 25);
            this.ChatInput.TabIndex = 1;
            this.ChatInput.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ChatInput_KeyDown);

            // 
            // ChatForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(798, 442);
            this.Controls.Add(this.ChatInput);
            this.Controls.Add(ChatLog);
            this.Name = "ChatForm";
            this.Text = "ChatForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox ChatInput;
        public static System.Windows.Forms.TextBox ChatLog;
    }
}