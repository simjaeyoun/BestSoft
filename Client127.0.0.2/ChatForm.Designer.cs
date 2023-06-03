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
            this.ChatInput = new System.Windows.Forms.TextBox();
            this.namelbl = new System.Windows.Forms.Label();
            this.ChatLog = new System.Windows.Forms.FlowLayoutPanel();
            this.chatProfile = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.guna2ControlBox3 = new Guna.UI2.WinForms.Guna2ControlBox();
            this.guna2ControlBox1 = new Guna.UI2.WinForms.Guna2ControlBox();
            ((System.ComponentModel.ISupportInitialize)(this.chatProfile)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // ChatInput
            // 
            this.ChatInput.Location = new System.Drawing.Point(-1, 555);
            this.ChatInput.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.ChatInput.Multiline = true;
            this.ChatInput.Name = "ChatInput";
            this.ChatInput.Size = new System.Drawing.Size(543, 44);
            this.ChatInput.TabIndex = 1;
            this.ChatInput.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ChatInput_KeyDown);
            // 
            // namelbl
            // 
            this.namelbl.AllowDrop = true;
            this.namelbl.AutoSize = true;
            this.namelbl.BackColor = System.Drawing.SystemColors.ControlDark;
            this.namelbl.Font = new System.Drawing.Font("맑은 고딕", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.namelbl.Location = new System.Drawing.Point(122, 34);
            this.namelbl.Name = "namelbl";
            this.namelbl.Size = new System.Drawing.Size(0, 23);
            this.namelbl.TabIndex = 5;
            // 
            // ChatLog
            // 
            this.ChatLog.AutoScroll = true;
            this.ChatLog.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.ChatLog.Location = new System.Drawing.Point(-5, 126);
            this.ChatLog.Name = "ChatLog";
            this.ChatLog.Size = new System.Drawing.Size(551, 431);
            this.ChatLog.TabIndex = 7;
            this.ChatLog.WrapContents = false;
            // 
            // chatProfile
            // 
            this.chatProfile.BackColor = System.Drawing.SystemColors.ControlDark;
            this.chatProfile.Location = new System.Drawing.Point(18, 12);
            this.chatProfile.Name = "chatProfile";
            this.chatProfile.Size = new System.Drawing.Size(98, 94);
            this.chatProfile.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.chatProfile.TabIndex = 6;
            this.chatProfile.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.SystemColors.ControlDark;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(542, 121);
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            // 
            // guna2ControlBox3
            // 
            this.guna2ControlBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.guna2ControlBox3.BackColor = System.Drawing.SystemColors.ControlDark;
            this.guna2ControlBox3.ControlBoxType = Guna.UI2.WinForms.Enums.ControlBoxType.MinimizeBox;
            this.guna2ControlBox3.FillColor = System.Drawing.Color.Transparent;
            this.guna2ControlBox3.IconColor = System.Drawing.Color.DimGray;
            this.guna2ControlBox3.Location = new System.Drawing.Point(473, 3);
            this.guna2ControlBox3.Name = "guna2ControlBox3";
            this.guna2ControlBox3.Size = new System.Drawing.Size(34, 29);
            this.guna2ControlBox3.TabIndex = 12;
            // 
            // guna2ControlBox1
            // 
            this.guna2ControlBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.guna2ControlBox1.BackColor = System.Drawing.SystemColors.ControlDark;
            this.guna2ControlBox1.FillColor = System.Drawing.Color.Transparent;
            this.guna2ControlBox1.IconColor = System.Drawing.Color.DimGray;
            this.guna2ControlBox1.Location = new System.Drawing.Point(508, 3);
            this.guna2ControlBox1.Name = "guna2ControlBox1";
            this.guna2ControlBox1.Size = new System.Drawing.Size(34, 29);
            this.guna2ControlBox1.TabIndex = 11;
            // 
            // ChatForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(541, 600);
            this.Controls.Add(this.guna2ControlBox3);
            this.Controls.Add(this.guna2ControlBox1);
            this.Controls.Add(this.ChatLog);
            this.Controls.Add(this.chatProfile);
            this.Controls.Add(this.namelbl);
            this.Controls.Add(this.ChatInput);
            this.Controls.Add(this.pictureBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "ChatForm";
            this.Text = "ChatForm";
            ((System.ComponentModel.ISupportInitialize)(this.chatProfile)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox ChatInput;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label namelbl;
        private System.Windows.Forms.PictureBox chatProfile;
        private System.Windows.Forms.FlowLayoutPanel ChatLog;
        private Guna.UI2.WinForms.Guna2ControlBox guna2ControlBox3;
        private Guna.UI2.WinForms.Guna2ControlBox guna2ControlBox1;
    }
}