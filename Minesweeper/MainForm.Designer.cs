namespace Minesweeper
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pictureGameField = new System.Windows.Forms.PictureBox();
            this.labelMinesText = new System.Windows.Forms.Label();
            this.labelMinesCount = new System.Windows.Forms.Label();
            this.labelTimer = new System.Windows.Forms.Label();
            this.buttonRestart = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureGameField)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureGameField
            // 
            this.pictureGameField.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.pictureGameField.Location = new System.Drawing.Point(12, 12);
            this.pictureGameField.Name = "pictureGameField";
            this.pictureGameField.Size = new System.Drawing.Size(400, 400);
            this.pictureGameField.TabIndex = 0;
            this.pictureGameField.TabStop = false;
            this.pictureGameField.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureGameField_Paint);
            // 
            // labelMinesText
            // 
            this.labelMinesText.AutoSize = true;
            this.labelMinesText.Location = new System.Drawing.Point(418, 82);
            this.labelMinesText.Name = "labelMinesText";
            this.labelMinesText.Size = new System.Drawing.Size(54, 20);
            this.labelMinesText.TabIndex = 1;
            this.labelMinesText.Text = "Мины:";
            // 
            // labelMinesCount
            // 
            this.labelMinesCount.AutoSize = true;
            this.labelMinesCount.Location = new System.Drawing.Point(478, 82);
            this.labelMinesCount.Name = "labelMinesCount";
            this.labelMinesCount.Size = new System.Drawing.Size(25, 20);
            this.labelMinesCount.TabIndex = 2;
            this.labelMinesCount.Text = "10";
            // 
            // labelTimer
            // 
            this.labelTimer.AutoSize = true;
            this.labelTimer.Location = new System.Drawing.Point(422, 148);
            this.labelTimer.Name = "labelTimer";
            this.labelTimer.Size = new System.Drawing.Size(55, 20);
            this.labelTimer.TabIndex = 3;
            this.labelTimer.Text = "0:00:00";
            // 
            // buttonRestart
            // 
            this.buttonRestart.Location = new System.Drawing.Point(445, 383);
            this.buttonRestart.Name = "buttonRestart";
            this.buttonRestart.Size = new System.Drawing.Size(94, 29);
            this.buttonRestart.TabIndex = 4;
            this.buttonRestart.Text = "Рестарт";
            this.buttonRestart.UseVisualStyleBackColor = true;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(579, 424);
            this.Controls.Add(this.buttonRestart);
            this.Controls.Add(this.labelTimer);
            this.Controls.Add(this.labelMinesCount);
            this.Controls.Add(this.labelMinesText);
            this.Controls.Add(this.pictureGameField);
            this.Name = "MainForm";
            this.Text = "Minesweeper";
            ((System.ComponentModel.ISupportInitialize)(this.pictureGameField)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private PictureBox pictureGameField;
        private Label labelMinesText;
        private Label labelMinesCount;
        private Label labelTimer;
        private Button buttonRestart;
    }
}