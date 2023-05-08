namespace 로그인화면
{
    partial class Second
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
            this.beaker4 = new System.Windows.Forms.PictureBox();
            this.desk2 = new System.Windows.Forms.PictureBox();
            this.desk = new System.Windows.Forms.PictureBox();
            this.beaker3 = new System.Windows.Forms.PictureBox();
            this.beaker2 = new System.Windows.Forms.PictureBox();
            this.beaker1 = new System.Windows.Forms.PictureBox();
            this.door = new System.Windows.Forms.PictureBox();
            this.player = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.beaker4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.desk2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.desk)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.beaker3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.beaker2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.beaker1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.door)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.player)).BeginInit();
            this.SuspendLayout();
            // 
            // beaker4
            // 
            this.beaker4.BackColor = System.Drawing.Color.Transparent;
            this.beaker4.Image = global::로그인화면.Properties.Resources.beaker4;
            this.beaker4.Location = new System.Drawing.Point(686, 536);
            this.beaker4.Name = "beaker4";
            this.beaker4.Size = new System.Drawing.Size(201, 122);
            this.beaker4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.beaker4.TabIndex = 5;
            this.beaker4.TabStop = false;
            this.beaker4.Tag = "obstacle";
            // 
            // desk2
            // 
            this.desk2.Image = global::로그인화면.Properties.Resources.desk2;
            this.desk2.Location = new System.Drawing.Point(351, 365);
            this.desk2.Name = "desk2";
            this.desk2.Size = new System.Drawing.Size(318, 293);
            this.desk2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.desk2.TabIndex = 4;
            this.desk2.TabStop = false;
            this.desk2.Tag = "obstacle";
            // 
            // desk
            // 
            this.desk.Image = global::로그인화면.Properties.Resources.desk2;
            this.desk.Location = new System.Drawing.Point(27, 365);
            this.desk.Name = "desk";
            this.desk.Size = new System.Drawing.Size(318, 293);
            this.desk.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.desk.TabIndex = 3;
            this.desk.TabStop = false;
            this.desk.Tag = "obstacle";
            // 
            // beaker3
            // 
            this.beaker3.Image = global::로그인화면.Properties.Resources.beaker3;
            this.beaker3.Location = new System.Drawing.Point(785, 271);
            this.beaker3.Name = "beaker3";
            this.beaker3.Size = new System.Drawing.Size(111, 107);
            this.beaker3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.beaker3.TabIndex = 2;
            this.beaker3.TabStop = false;
            this.beaker3.Tag = "obstacle";
            // 
            // beaker2
            // 
            this.beaker2.Image = global::로그인화면.Properties.Resources.beaker2;
            this.beaker2.Location = new System.Drawing.Point(850, 135);
            this.beaker2.Name = "beaker2";
            this.beaker2.Size = new System.Drawing.Size(111, 107);
            this.beaker2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.beaker2.TabIndex = 1;
            this.beaker2.TabStop = false;
            this.beaker2.Tag = "obstacle";
            // 
            // beaker1
            // 
            this.beaker1.Image = global::로그인화면.Properties.Resources.beaker1;
            this.beaker1.Location = new System.Drawing.Point(733, 53);
            this.beaker1.Name = "beaker1";
            this.beaker1.Size = new System.Drawing.Size(111, 107);
            this.beaker1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.beaker1.TabIndex = 0;
            this.beaker1.TabStop = false;
            this.beaker1.Tag = "obstacle";
            // 
            // door
            // 
            this.door.BackColor = System.Drawing.Color.Silver;
            this.door.Image = global::로그인화면.Properties.Resources.door1;
            this.door.Location = new System.Drawing.Point(85, 28);
            this.door.Name = "door";
            this.door.Size = new System.Drawing.Size(142, 162);
            this.door.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.door.TabIndex = 6;
            this.door.TabStop = false;
            this.door.Tag = "obstacle";
            // 
            // player
            // 
            this.player.BackColor = System.Drawing.Color.Transparent;
            this.player.Location = new System.Drawing.Point(27, 259);
            this.player.Name = "player";
            this.player.Size = new System.Drawing.Size(67, 71);
            this.player.TabIndex = 7;
            this.player.TabStop = false;
            // 
            // Second
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(1006, 721);
            this.Controls.Add(this.player);
            this.Controls.Add(this.door);
            this.Controls.Add(this.beaker4);
            this.Controls.Add(this.desk2);
            this.Controls.Add(this.desk);
            this.Controls.Add(this.beaker3);
            this.Controls.Add(this.beaker2);
            this.Controls.Add(this.beaker1);
            this.DoubleBuffered = true;
            this.Name = "Second";
            this.Text = "Second";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.In_Game_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.In_Game_KeyUp);
            ((System.ComponentModel.ISupportInitialize)(this.beaker4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.desk2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.desk)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.beaker3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.beaker2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.beaker1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.door)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.player)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox beaker1;
        private System.Windows.Forms.PictureBox beaker2;
        private System.Windows.Forms.PictureBox beaker3;
        private System.Windows.Forms.PictureBox desk;
        private System.Windows.Forms.PictureBox desk2;
        private System.Windows.Forms.PictureBox beaker4;
        private System.Windows.Forms.PictureBox door;
        private System.Windows.Forms.PictureBox player;
    }
}