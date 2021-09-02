namespace atomic_chess
{
    partial class atomicChess
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
            this.chooseLbl = new System.Windows.Forms.Label();
            this.choseWhiteBtn = new System.Windows.Forms.Button();
            this.choseRandomBtn = new System.Windows.Forms.Button();
            this.choseBlackBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // chooseLbl
            // 
            this.chooseLbl.AutoSize = true;
            this.chooseLbl.Font = new System.Drawing.Font("Georgia", 14.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chooseLbl.Location = new System.Drawing.Point(105, 30);
            this.chooseLbl.Name = "chooseLbl";
            this.chooseLbl.Size = new System.Drawing.Size(177, 23);
            this.chooseLbl.TabIndex = 0;
            this.chooseLbl.Text = "Choose your player";
            // 
            // choseWhiteBtn
            // 
            this.choseWhiteBtn.Font = new System.Drawing.Font("Georgia", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.choseWhiteBtn.Location = new System.Drawing.Point(36, 85);
            this.choseWhiteBtn.Name = "choseWhiteBtn";
            this.choseWhiteBtn.Size = new System.Drawing.Size(75, 23);
            this.choseWhiteBtn.TabIndex = 1;
            this.choseWhiteBtn.Text = "White";
            this.choseWhiteBtn.UseVisualStyleBackColor = true;
            this.choseWhiteBtn.Click += new System.EventHandler(this.choseWhiteBtn_Click);
            // 
            // choseRandomBtn
            // 
            this.choseRandomBtn.Font = new System.Drawing.Font("Georgia", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.choseRandomBtn.Location = new System.Drawing.Point(147, 85);
            this.choseRandomBtn.Name = "choseRandomBtn";
            this.choseRandomBtn.Size = new System.Drawing.Size(75, 23);
            this.choseRandomBtn.TabIndex = 2;
            this.choseRandomBtn.Text = "Random";
            this.choseRandomBtn.UseVisualStyleBackColor = true;
            this.choseRandomBtn.Click += new System.EventHandler(this.choseRandomBtn_Click);
            // 
            // choseBlackBtn
            // 
            this.choseBlackBtn.Font = new System.Drawing.Font("Georgia", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.choseBlackBtn.Location = new System.Drawing.Point(265, 85);
            this.choseBlackBtn.Name = "choseBlackBtn";
            this.choseBlackBtn.Size = new System.Drawing.Size(75, 23);
            this.choseBlackBtn.TabIndex = 3;
            this.choseBlackBtn.Text = "Black";
            this.choseBlackBtn.UseVisualStyleBackColor = true;
            this.choseBlackBtn.Click += new System.EventHandler(this.choseBlackBtn_Click);
            // 
            // atomicChess
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.ClientSize = new System.Drawing.Size(378, 143);
            this.Controls.Add(this.choseBlackBtn);
            this.Controls.Add(this.choseRandomBtn);
            this.Controls.Add(this.choseWhiteBtn);
            this.Controls.Add(this.chooseLbl);
            this.ForeColor = System.Drawing.Color.Black;
            this.Name = "atomicChess";
            this.Text = "Atomic chess";
            //this.Load += new System.EventHandler(this.atomicChessForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label chooseLbl;
        private System.Windows.Forms.Button choseWhiteBtn;
        private System.Windows.Forms.Button choseRandomBtn;
        private System.Windows.Forms.Button choseBlackBtn;
    }
}

