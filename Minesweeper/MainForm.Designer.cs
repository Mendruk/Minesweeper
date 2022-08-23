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
            this.components = new System.ComponentModel.Container();
            this.pictureGameField = new System.Windows.Forms.PictureBox();
            this.labelMinesText = new System.Windows.Forms.Label();
            this.labelMinesCount = new System.Windows.Forms.Label();
            this.labelTimer = new System.Windows.Forms.Label();
            this.buttonRestart = new System.Windows.Forms.Button();
            this.timer = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.pictureGameField)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureGameField
            // 
            this.pictureGameField.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.pictureGameField.Location = new System.Drawing.Point(12, 12);
            this.pictureGameField.Name = "pictureGameField";
            this.pictureGameField.Size = new System.Drawing.Size(451, 451);
            this.pictureGameField.TabIndex = 0;
            this.pictureGameField.TabStop = false;
            this.pictureGameField.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureGameField_Paint);
            this.pictureGameField.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pictureGameField_MouseClick);
            this.pictureGameField.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureGameField_MouseMove);
            // 
            // labelMinesText
            // 
            this.labelMinesText.AutoSize = true;
            this.labelMinesText.Location = new System.Drawing.Point(12, 485);
            this.labelMinesText.Name = "labelMinesText";
            this.labelMinesText.Size = new System.Drawing.Size(54, 20);
            this.labelMinesText.TabIndex = 1;
            this.labelMinesText.Text = "Мины:";
            // 
            // labelMinesCount
            // 
            this.labelMinesCount.AutoSize = true;
            this.labelMinesCount.Location = new System.Drawing.Point(72, 485);
            this.labelMinesCount.Name = "labelMinesCount";
            this.labelMinesCount.Size = new System.Drawing.Size(25, 20);
            this.labelMinesCount.TabIndex = 2;
            this.labelMinesCount.Text = "10";
            // 
            // labelTimer
            // 
            this.labelTimer.AutoSize = true;
            this.labelTimer.Location = new System.Drawing.Point(399, 485);
            this.labelTimer.Name = "labelTimer";
            this.labelTimer.Size = new System.Drawing.Size(63, 20);
            this.labelTimer.TabIndex = 3;
            this.labelTimer.Text = "00:00:00";
            // 
            // buttonRestart
            // 
            this.buttonRestart.Location = new System.Drawing.Point(185, 481);
            this.buttonRestart.Name = "buttonRestart";
            this.buttonRestart.Size = new System.Drawing.Size(94, 29);
            this.buttonRestart.TabIndex = 4;
            this.buttonRestart.Text = "Рестарт";
            this.buttonRestart.UseVisualStyleBackColor = true;
            this.buttonRestart.Click += new System.EventHandler(this.buttonRestart_Click);
            // 
            // timer
            // 
            this.timer.Enabled = true;
            this.timer.Interval = 1000;
            this.timer.Tick += new System.EventHandler(this.timer_Tick);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(474, 524);
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
        private System.Windows.Forms.Timer timer;
    }
}