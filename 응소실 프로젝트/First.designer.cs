namespace 로그인화면
{
    partial class First
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(First));
            this.board = new System.Windows.Forms.PictureBox();
            this.door = new System.Windows.Forms.PictureBox();
            this.desk = new System.Windows.Forms.PictureBox();
            this.desk_memo = new System.Windows.Forms.PictureBox();
            this.bookshelf = new System.Windows.Forms.PictureBox();
            this.player = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.board)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.door)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.desk)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.desk_memo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bookshelf)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.player)).BeginInit();
            this.SuspendLayout();
            // 
            // board
            // 
            this.board.BackColor = System.Drawing.Color.Transparent;
            this.board.Image = ((System.Drawing.Image)(resources.GetObject("board.Image")));
            this.board.Location = new System.Drawing.Point(77, 12);
            this.board.Name = "board";
            this.board.Size = new System.Drawing.Size(505, 168);
            this.board.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.board.TabIndex = 0;
            this.board.TabStop = false;
            this.board.Tag = "obstacle";
            // 
            // door
            // 
            this.door.BackColor = System.Drawing.Color.Transparent;
            this.door.Image = ((System.Drawing.Image)(resources.GetObject("door.Image")));
            this.door.Location = new System.Drawing.Point(739, 38);
            this.door.Name = "door";
            this.door.Size = new System.Drawing.Size(142, 162);
            this.door.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.door.TabIndex = 1;
            this.door.TabStop = false;
            this.door.Tag = "obstacle";
            // 
            // desk
            // 
            this.desk.BackColor = System.Drawing.Color.Transparent;
            this.desk.Image = ((System.Drawing.Image)(resources.GetObject("desk.Image")));
            this.desk.Location = new System.Drawing.Point(77, 230);
            this.desk.Name = "desk";
            this.desk.Size = new System.Drawing.Size(201, 125);
            this.desk.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.desk.TabIndex = 2;
            this.desk.TabStop = false;
            this.desk.Tag = "obstacle";
            // 
            // desk_memo
            // 
            this.desk_memo.BackColor = System.Drawing.Color.Transparent;
            this.desk_memo.Image = ((System.Drawing.Image)(resources.GetObject("desk_memo.Image")));
            this.desk_memo.Location = new System.Drawing.Point(328, 230);
            this.desk_memo.Name = "desk_memo";
            this.desk_memo.Size = new System.Drawing.Size(196, 125);
            this.desk_memo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.desk_memo.TabIndex = 3;
            this.desk_memo.TabStop = false;
            this.desk_memo.Tag = "obstacle";
            // 
            // bookshelf
            // 
            this.bookshelf.BackColor = System.Drawing.Color.Transparent;
            this.bookshelf.Image = ((System.Drawing.Image)(resources.GetObject("bookshelf.Image")));
            this.bookshelf.Location = new System.Drawing.Point(-1, 451);
            this.bookshelf.Name = "bookshelf";
            this.bookshelf.Size = new System.Drawing.Size(759, 268);
            this.bookshelf.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.bookshelf.TabIndex = 4;
            this.bookshelf.TabStop = false;
            this.bookshelf.Tag = "obstacle";
            // 
            // player
            // 
            this.player.BackColor = System.Drawing.Color.Transparent;
            this.player.Location = new System.Drawing.Point(927, 322);
            this.player.Name = "player";
            this.player.Size = new System.Drawing.Size(67, 71);
            this.player.TabIndex = 5;
            this.player.TabStop = false;
            this.player.Tag = "player";
            // 
            // First
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1006, 721);
            this.Controls.Add(this.player);
            this.Controls.Add(this.bookshelf);
            this.Controls.Add(this.desk_memo);
            this.Controls.Add(this.desk);
            this.Controls.Add(this.door);
            this.Controls.Add(this.board);
            this.Name = "First";
            this.Text = "First";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.In_Game_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.In_Game_KeyUp);
            ((System.ComponentModel.ISupportInitialize)(this.board)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.door)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.desk)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.desk_memo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bookshelf)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.player)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox board;
        private System.Windows.Forms.PictureBox door;
        private System.Windows.Forms.PictureBox desk;
        private System.Windows.Forms.PictureBox desk_memo;
        private System.Windows.Forms.PictureBox bookshelf;
        private System.Windows.Forms.PictureBox player;
    }
}

