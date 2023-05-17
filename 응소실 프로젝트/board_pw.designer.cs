namespace 로그인화면
{
    partial class board_pw
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
            this.btn_OK = new System.Windows.Forms.Button();
            this.txtpasswd = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btn_OK
            // 
            this.btn_OK.Font = new System.Drawing.Font("돋움", 9F);
            this.btn_OK.Location = new System.Drawing.Point(242, 101);
            this.btn_OK.Name = "btn_OK";
            this.btn_OK.Size = new System.Drawing.Size(75, 35);
            this.btn_OK.TabIndex = 8;
            this.btn_OK.Text = "입력";
            this.btn_OK.UseVisualStyleBackColor = true;
            this.btn_OK.Click += new System.EventHandler(this.btn_OK_Click);
            // 
            // txtpasswd
            // 
            this.txtpasswd.Location = new System.Drawing.Point(46, 108);
            this.txtpasswd.Name = "txtpasswd";
            this.txtpasswd.Size = new System.Drawing.Size(164, 25);
            this.txtpasswd.TabIndex = 7;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("돋움", 18F);
            this.label1.Location = new System.Drawing.Point(32, 47);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(334, 30);
            this.label1.TabIndex = 6;
            this.label1.Text = "\"717 721 473 217 ???\"";
            // 
            // board_pw
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(362, 183);
            this.Controls.Add(this.btn_OK);
            this.Controls.Add(this.txtpasswd);
            this.Controls.Add(this.label1);
            this.Name = "board_pw";
            this.Text = "board_pw";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_OK;
        private System.Windows.Forms.TextBox txtpasswd;
        private System.Windows.Forms.Label label1;
    }
}