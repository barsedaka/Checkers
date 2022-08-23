
namespace CheckersGameUI
{
    partial class GameSettingForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.buttonDone = new System.Windows.Forms.Button();
            this.textBoxPlayer2 = new System.Windows.Forms.TextBox();
            this.checkBoxPlayer2 = new System.Windows.Forms.CheckBox();
            this.textBoxPlayer1 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.radioButtonBoardSize10 = new System.Windows.Forms.RadioButton();
            this.radioButtonBoardSize8 = new System.Windows.Forms.RadioButton();
            this.radioButtonBoardSize6 = new System.Windows.Forms.RadioButton();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(108, 20);
            this.label1.TabIndex = 1;
            this.label1.Text = "Board Size:";
            // 
            // buttonDone
            // 
            this.buttonDone.Location = new System.Drawing.Point(188, 199);
            this.buttonDone.Name = "buttonDone";
            this.buttonDone.Size = new System.Drawing.Size(82, 39);
            this.buttonDone.TabIndex = 18;
            this.buttonDone.Text = "Done";
            this.buttonDone.UseVisualStyleBackColor = true;
            this.buttonDone.Click += new System.EventHandler(this.buttonDone_Click);
            // 
            // textBoxPlayer2
            // 
            this.textBoxPlayer2.Enabled = false;
            this.textBoxPlayer2.Location = new System.Drawing.Point(140, 162);
            this.textBoxPlayer2.Name = "textBoxPlayer2";
            this.textBoxPlayer2.Size = new System.Drawing.Size(130, 22);
            this.textBoxPlayer2.TabIndex = 17;
            this.textBoxPlayer2.Text = "[Computer]";
            // 
            // checkBoxPlayer2
            // 
            this.checkBoxPlayer2.AutoSize = true;
            this.checkBoxPlayer2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.checkBoxPlayer2.Location = new System.Drawing.Point(33, 162);
            this.checkBoxPlayer2.Name = "checkBoxPlayer2";
            this.checkBoxPlayer2.Size = new System.Drawing.Size(87, 22);
            this.checkBoxPlayer2.TabIndex = 16;
            this.checkBoxPlayer2.Text = "Player 2:";
            this.checkBoxPlayer2.UseVisualStyleBackColor = true;
            this.checkBoxPlayer2.CheckedChanged += new System.EventHandler(this.checkBoxPlayer2_CheckedChanged);
            // 
            // textBoxPlayer1
            // 
            this.textBoxPlayer1.Location = new System.Drawing.Point(140, 127);
            this.textBoxPlayer1.Name = "textBoxPlayer1";
            this.textBoxPlayer1.Size = new System.Drawing.Size(130, 22);
            this.textBoxPlayer1.TabIndex = 15;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(55, 127);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 18);
            this.label3.TabIndex = 14;
            this.label3.Text = "Player 1:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(12, 94);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(78, 20);
            this.label2.TabIndex = 13;
            this.label2.Text = "Players:";
            // 
            // radioButtonBoardSize10
            // 
            this.radioButtonBoardSize10.AutoSize = true;
            this.radioButtonBoardSize10.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.radioButtonBoardSize10.Location = new System.Drawing.Point(209, 53);
            this.radioButtonBoardSize10.Name = "radioButtonBoardSize10";
            this.radioButtonBoardSize10.Size = new System.Drawing.Size(76, 22);
            this.radioButtonBoardSize10.TabIndex = 12;
            this.radioButtonBoardSize10.Tag = "10";
            this.radioButtonBoardSize10.Text = "10 x 10";
            this.radioButtonBoardSize10.UseVisualStyleBackColor = true;
            this.radioButtonBoardSize10.CheckedChanged += new System.EventHandler(this.radioButtonBoardSize_CheckedChanged);
            // 
            // radioButtonBoardSize8
            // 
            this.radioButtonBoardSize8.AutoSize = true;
            this.radioButtonBoardSize8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.radioButtonBoardSize8.Location = new System.Drawing.Point(130, 53);
            this.radioButtonBoardSize8.Name = "radioButtonBoardSize8";
            this.radioButtonBoardSize8.Size = new System.Drawing.Size(60, 22);
            this.radioButtonBoardSize8.TabIndex = 11;
            this.radioButtonBoardSize8.Tag = "8";
            this.radioButtonBoardSize8.Text = "8 x 8";
            this.radioButtonBoardSize8.UseVisualStyleBackColor = true;
            this.radioButtonBoardSize8.CheckedChanged += new System.EventHandler(this.radioButtonBoardSize_CheckedChanged);
            // 
            // radioButtonBoardSize6
            // 
            this.radioButtonBoardSize6.AutoSize = true;
            this.radioButtonBoardSize6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.radioButtonBoardSize6.Location = new System.Drawing.Point(48, 53);
            this.radioButtonBoardSize6.Name = "radioButtonBoardSize6";
            this.radioButtonBoardSize6.Size = new System.Drawing.Size(60, 22);
            this.radioButtonBoardSize6.TabIndex = 10;
            this.radioButtonBoardSize6.Tag = "6";
            this.radioButtonBoardSize6.Text = "6 x 6";
            this.radioButtonBoardSize6.UseVisualStyleBackColor = true;
            this.radioButtonBoardSize6.CheckedChanged += new System.EventHandler(this.radioButtonBoardSize_CheckedChanged);
            // 
            // GameSettingForm
            // 
            this.AcceptButton = this.buttonDone;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Silver;
            this.ClientSize = new System.Drawing.Size(301, 250);
            this.Controls.Add(this.buttonDone);
            this.Controls.Add(this.textBoxPlayer2);
            this.Controls.Add(this.checkBoxPlayer2);
            this.Controls.Add(this.textBoxPlayer1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.radioButtonBoardSize10);
            this.Controls.Add(this.radioButtonBoardSize8);
            this.Controls.Add(this.radioButtonBoardSize6);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "GameSettingForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Game Setting";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonDone;
        private System.Windows.Forms.TextBox textBoxPlayer2;
        private System.Windows.Forms.CheckBox checkBoxPlayer2;
        private System.Windows.Forms.TextBox textBoxPlayer1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RadioButton radioButtonBoardSize10;
        private System.Windows.Forms.RadioButton radioButtonBoardSize8;
        private System.Windows.Forms.RadioButton radioButtonBoardSize6;
    }
}