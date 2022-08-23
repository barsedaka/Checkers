
namespace CheckersGameUI
{
    partial class CheckersGameForm
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
            this.LabelPlayerOneScore = new System.Windows.Forms.Label();
            this.LabelPlayerTwoScore = new System.Windows.Forms.Label();
            this.ComputerMoveTimer = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // LabelPlayerOneScore
            // 
            this.LabelPlayerOneScore.AutoSize = true;
            this.LabelPlayerOneScore.BackColor = System.Drawing.Color.Transparent;
            this.LabelPlayerOneScore.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.LabelPlayerOneScore.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.LabelPlayerOneScore.Location = new System.Drawing.Point(67, 25);
            this.LabelPlayerOneScore.Name = "LabelPlayerOneScore";
            this.LabelPlayerOneScore.Size = new System.Drawing.Size(97, 24);
            this.LabelPlayerOneScore.TabIndex = 0;
            this.LabelPlayerOneScore.Text = "Player 1: ";
            this.LabelPlayerOneScore.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // LabelPlayerTwoScore
            // 
            this.LabelPlayerTwoScore.AutoSize = true;
            this.LabelPlayerTwoScore.BackColor = System.Drawing.Color.Transparent;
            this.LabelPlayerTwoScore.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(177)));
            this.LabelPlayerTwoScore.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.LabelPlayerTwoScore.Location = new System.Drawing.Point(262, 25);
            this.LabelPlayerTwoScore.Name = "LabelPlayerTwoScore";
            this.LabelPlayerTwoScore.Size = new System.Drawing.Size(97, 24);
            this.LabelPlayerTwoScore.TabIndex = 1;
            this.LabelPlayerTwoScore.Text = "Player 2: ";
            // 
            // CheckersGameForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackgroundImage = global::CheckersGameUI.Properties.Resources.FormBackground;
            this.ClientSize = new System.Drawing.Size(502, 392);
            this.Controls.Add(this.LabelPlayerTwoScore);
            this.Controls.Add(this.LabelPlayerOneScore);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.Name = "CheckersGameForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "CheckersGame";
            this.Load += new System.EventHandler(this.CheckersGameForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label LabelPlayerOneScore;
        private System.Windows.Forms.Label LabelPlayerTwoScore;
        private System.Windows.Forms.Timer ComputerMoveTimer;
    }
}