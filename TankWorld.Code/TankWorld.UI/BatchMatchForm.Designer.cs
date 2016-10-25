namespace TankWorld.UI
{
    partial class BatchMatchForm
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
            this.firstPlayerComboBox = new System.Windows.Forms.ComboBox();
            this.secondPlayerComboBox = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.batchStartButton = new System.Windows.Forms.Button();
            this.firstPlayerScoreBarLabel = new System.Windows.Forms.Label();
            this.secondPlayerScoreBarLabel = new System.Windows.Forms.Label();
            this.tieCountBarLabel = new System.Windows.Forms.Label();
            this.totalBarLabel = new System.Windows.Forms.Label();
            this.timesTextBox = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.player1ScoreLabel = new System.Windows.Forms.Label();
            this.totalLabel = new System.Windows.Forms.Label();
            this.tieLabel1 = new System.Windows.Forms.Label();
            this.player2ScoreLabel = new System.Windows.Forms.Label();
            this.singleStartButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // firstPlayerComboBox
            // 
            this.firstPlayerComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.firstPlayerComboBox.ForeColor = System.Drawing.Color.Red;
            this.firstPlayerComboBox.FormattingEnabled = true;
            this.firstPlayerComboBox.Items.AddRange(new object[] {
            "FlagRemover",
            "Wandering"});
            this.firstPlayerComboBox.Location = new System.Drawing.Point(12, 31);
            this.firstPlayerComboBox.Name = "firstPlayerComboBox";
            this.firstPlayerComboBox.Size = new System.Drawing.Size(222, 21);
            this.firstPlayerComboBox.TabIndex = 0;
            // 
            // secondPlayerComboBox
            // 
            this.secondPlayerComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.secondPlayerComboBox.ForeColor = System.Drawing.Color.Blue;
            this.secondPlayerComboBox.FormattingEnabled = true;
            this.secondPlayerComboBox.Items.AddRange(new object[] {
            "FlagRemover",
            "Wandering"});
            this.secondPlayerComboBox.Location = new System.Drawing.Point(240, 31);
            this.secondPlayerComboBox.Name = "secondPlayerComboBox";
            this.secondPlayerComboBox.Size = new System.Drawing.Size(222, 21);
            this.secondPlayerComboBox.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(230, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(21, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "VS";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(307, 88);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Times";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.Red;
            this.label3.Location = new System.Drawing.Point(12, 15);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(61, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "First Player:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.Blue;
            this.label4.Location = new System.Drawing.Point(383, 15);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(79, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Second Player:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(125, 88);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(37, 13);
            this.label5.TabIndex = 7;
            this.label5.Text = "Match";
            // 
            // batchStartButton
            // 
            this.batchStartButton.Location = new System.Drawing.Point(168, 110);
            this.batchStartButton.Name = "batchStartButton";
            this.batchStartButton.Size = new System.Drawing.Size(133, 23);
            this.batchStartButton.TabIndex = 8;
            this.batchStartButton.Text = "Batch Game Start";
            this.batchStartButton.UseVisualStyleBackColor = true;
            this.batchStartButton.Click += new System.EventHandler(this.startButton_Click);
            // 
            // firstPlayerScoreBarLabel
            // 
            this.firstPlayerScoreBarLabel.BackColor = System.Drawing.Color.Red;
            this.firstPlayerScoreBarLabel.ForeColor = System.Drawing.Color.White;
            this.firstPlayerScoreBarLabel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.firstPlayerScoreBarLabel.Location = new System.Drawing.Point(101, 147);
            this.firstPlayerScoreBarLabel.Name = "firstPlayerScoreBarLabel";
            this.firstPlayerScoreBarLabel.Size = new System.Drawing.Size(120, 15);
            this.firstPlayerScoreBarLabel.TabIndex = 9;
            // 
            // secondPlayerScoreBarLabel
            // 
            this.secondPlayerScoreBarLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.secondPlayerScoreBarLabel.BackColor = System.Drawing.Color.Blue;
            this.secondPlayerScoreBarLabel.ForeColor = System.Drawing.Color.White;
            this.secondPlayerScoreBarLabel.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.secondPlayerScoreBarLabel.Location = new System.Drawing.Point(101, 171);
            this.secondPlayerScoreBarLabel.Name = "secondPlayerScoreBarLabel";
            this.secondPlayerScoreBarLabel.Size = new System.Drawing.Size(120, 15);
            this.secondPlayerScoreBarLabel.TabIndex = 10;
            // 
            // tieCountBarLabel
            // 
            this.tieCountBarLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.tieCountBarLabel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.tieCountBarLabel.ForeColor = System.Drawing.Color.White;
            this.tieCountBarLabel.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.tieCountBarLabel.Location = new System.Drawing.Point(101, 198);
            this.tieCountBarLabel.Name = "tieCountBarLabel";
            this.tieCountBarLabel.Size = new System.Drawing.Size(120, 15);
            this.tieCountBarLabel.TabIndex = 11;
            // 
            // totalBarLabel
            // 
            this.totalBarLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.totalBarLabel.BackColor = System.Drawing.Color.Gray;
            this.totalBarLabel.ForeColor = System.Drawing.Color.White;
            this.totalBarLabel.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.totalBarLabel.Location = new System.Drawing.Point(101, 225);
            this.totalBarLabel.Name = "totalBarLabel";
            this.totalBarLabel.Size = new System.Drawing.Size(120, 15);
            this.totalBarLabel.TabIndex = 12;
            // 
            // timesTextBox
            // 
            this.timesTextBox.Location = new System.Drawing.Point(168, 85);
            this.timesTextBox.Name = "timesTextBox";
            this.timesTextBox.Size = new System.Drawing.Size(133, 20);
            this.timesTextBox.TabIndex = 13;
            this.timesTextBox.Text = "1000";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 147);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(49, 13);
            this.label6.TabIndex = 14;
            this.label6.Text = "#1 Won:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(12, 171);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(49, 13);
            this.label7.TabIndex = 15;
            this.label7.Text = "#2 Won:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(12, 198);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(30, 13);
            this.label8.TabIndex = 16;
            this.label8.Text = "Ties:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(12, 225);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(34, 13);
            this.label9.TabIndex = 17;
            this.label9.Text = "Total:";
            // 
            // player1ScoreLabel
            // 
            this.player1ScoreLabel.AutoSize = true;
            this.player1ScoreLabel.Location = new System.Drawing.Point(60, 147);
            this.player1ScoreLabel.Name = "player1ScoreLabel";
            this.player1ScoreLabel.Size = new System.Drawing.Size(13, 13);
            this.player1ScoreLabel.TabIndex = 18;
            this.player1ScoreLabel.Text = "0";
            // 
            // totalLabel
            // 
            this.totalLabel.AutoSize = true;
            this.totalLabel.Location = new System.Drawing.Point(60, 225);
            this.totalLabel.Name = "totalLabel";
            this.totalLabel.Size = new System.Drawing.Size(13, 13);
            this.totalLabel.TabIndex = 19;
            this.totalLabel.Text = "0";
            // 
            // tieLabel1
            // 
            this.tieLabel1.AutoSize = true;
            this.tieLabel1.Location = new System.Drawing.Point(60, 198);
            this.tieLabel1.Name = "tieLabel1";
            this.tieLabel1.Size = new System.Drawing.Size(13, 13);
            this.tieLabel1.TabIndex = 20;
            this.tieLabel1.Text = "0";
            // 
            // player2ScoreLabel
            // 
            this.player2ScoreLabel.AutoSize = true;
            this.player2ScoreLabel.Location = new System.Drawing.Point(60, 171);
            this.player2ScoreLabel.Name = "player2ScoreLabel";
            this.player2ScoreLabel.Size = new System.Drawing.Size(13, 13);
            this.player2ScoreLabel.TabIndex = 21;
            this.player2ScoreLabel.Text = "0";
            // 
            // singleStartButton
            // 
            this.singleStartButton.Location = new System.Drawing.Point(168, 58);
            this.singleStartButton.Name = "singleStartButton";
            this.singleStartButton.Size = new System.Drawing.Size(133, 23);
            this.singleStartButton.TabIndex = 22;
            this.singleStartButton.Text = "Single Game Start";
            this.singleStartButton.UseVisualStyleBackColor = true;
            this.singleStartButton.Click += new System.EventHandler(this.singleStartButton_Click);
            // 
            // BatchMatchForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(472, 296);
            this.Controls.Add(this.singleStartButton);
            this.Controls.Add(this.player2ScoreLabel);
            this.Controls.Add(this.tieLabel1);
            this.Controls.Add(this.totalLabel);
            this.Controls.Add(this.player1ScoreLabel);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.timesTextBox);
            this.Controls.Add(this.totalBarLabel);
            this.Controls.Add(this.tieCountBarLabel);
            this.Controls.Add(this.secondPlayerScoreBarLabel);
            this.Controls.Add(this.firstPlayerScoreBarLabel);
            this.Controls.Add(this.batchStartButton);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.secondPlayerComboBox);
            this.Controls.Add(this.firstPlayerComboBox);
            this.Name = "BatchMatchForm";
            this.Text = "BatchMatchForm";
            this.Load += new System.EventHandler(this.BatchMatchForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox firstPlayerComboBox;
        private System.Windows.Forms.ComboBox secondPlayerComboBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button batchStartButton;
        private System.Windows.Forms.Label firstPlayerScoreBarLabel;
        private System.Windows.Forms.Label secondPlayerScoreBarLabel;
        private System.Windows.Forms.Label tieCountBarLabel;
        private System.Windows.Forms.Label totalBarLabel;
        private System.Windows.Forms.TextBox timesTextBox;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label player1ScoreLabel;
        private System.Windows.Forms.Label totalLabel;
        private System.Windows.Forms.Label tieLabel1;
        private System.Windows.Forms.Label player2ScoreLabel;
        private System.Windows.Forms.Button singleStartButton;
    }
}