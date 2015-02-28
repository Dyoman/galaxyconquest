namespace GalaxyConquest.SpaceObjects
{
    partial class StarSystemForm
    {
        /// <summary>
        /// Требуется переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.panel1 = new System.Windows.Forms.Panel();
            this.showAllButton = new System.Windows.Forms.Button();
            this.climateLabel = new System.Windows.Forms.Label();
            this.buildingsTextBox = new System.Windows.Forms.TextBox();
            this.ownerLabel = new System.Windows.Forms.Label();
            this.profitLabel = new System.Windows.Forms.Label();
            this.populationLabel = new System.Windows.Forms.Label();
            this.sizeLabel = new System.Windows.Forms.Label();
            this.mineralsLabel = new System.Windows.Forms.Label();
            this.captureButton = new System.Windows.Forms.Button();
            this.skillPointLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Black;
            this.pictureBox1.Location = new System.Drawing.Point(248, 1);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(460, 460);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBox1_Paint);
            this.pictureBox1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseClick);
            // 
            // timer1
            // 
            this.timer1.Interval = 10;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.skillPointLabel);
            this.panel1.Controls.Add(this.showAllButton);
            this.panel1.Controls.Add(this.climateLabel);
            this.panel1.Controls.Add(this.buildingsTextBox);
            this.panel1.Controls.Add(this.ownerLabel);
            this.panel1.Controls.Add(this.profitLabel);
            this.panel1.Controls.Add(this.populationLabel);
            this.panel1.Controls.Add(this.sizeLabel);
            this.panel1.Controls.Add(this.mineralsLabel);
            this.panel1.Location = new System.Drawing.Point(12, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(230, 391);
            this.panel1.TabIndex = 1;
            // 
            // showAllButton
            // 
            this.showAllButton.Cursor = System.Windows.Forms.Cursors.Hand;
            this.showAllButton.Location = new System.Drawing.Point(28, 3);
            this.showAllButton.Name = "showAllButton";
            this.showAllButton.Size = new System.Drawing.Size(166, 23);
            this.showAllButton.TabIndex = 41;
            this.showAllButton.Text = "Характеристики системы";
            this.showAllButton.UseVisualStyleBackColor = true;
            this.showAllButton.Click += new System.EventHandler(this.showAllButton_Click);
            // 
            // climateLabel
            // 
            this.climateLabel.AutoSize = true;
            this.climateLabel.Location = new System.Drawing.Point(3, 168);
            this.climateLabel.Name = "climateLabel";
            this.climateLabel.Size = new System.Drawing.Size(41, 13);
            this.climateLabel.TabIndex = 39;
            this.climateLabel.Text = "Climate";
            // 
            // buildingsTextBox
            // 
            this.buildingsTextBox.BackColor = System.Drawing.SystemColors.ControlLight;
            this.buildingsTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.buildingsTextBox.Location = new System.Drawing.Point(6, 262);
            this.buildingsTextBox.Multiline = true;
            this.buildingsTextBox.Name = "buildingsTextBox";
            this.buildingsTextBox.ReadOnly = true;
            this.buildingsTextBox.Size = new System.Drawing.Size(217, 122);
            this.buildingsTextBox.TabIndex = 38;
            // 
            // ownerLabel
            // 
            this.ownerLabel.AutoSize = true;
            this.ownerLabel.Location = new System.Drawing.Point(3, 143);
            this.ownerLabel.Name = "ownerLabel";
            this.ownerLabel.Size = new System.Drawing.Size(38, 13);
            this.ownerLabel.TabIndex = 32;
            this.ownerLabel.Text = "Owner";
            // 
            // profitLabel
            // 
            this.profitLabel.AutoSize = true;
            this.profitLabel.Location = new System.Drawing.Point(3, 119);
            this.profitLabel.Name = "profitLabel";
            this.profitLabel.Size = new System.Drawing.Size(31, 13);
            this.profitLabel.TabIndex = 33;
            this.profitLabel.Text = "Profit";
            // 
            // populationLabel
            // 
            this.populationLabel.AutoSize = true;
            this.populationLabel.Location = new System.Drawing.Point(3, 92);
            this.populationLabel.Name = "populationLabel";
            this.populationLabel.Size = new System.Drawing.Size(82, 13);
            this.populationLabel.TabIndex = 31;
            this.populationLabel.Text = "Population Now";
            // 
            // sizeLabel
            // 
            this.sizeLabel.AutoSize = true;
            this.sizeLabel.Location = new System.Drawing.Point(3, 39);
            this.sizeLabel.Name = "sizeLabel";
            this.sizeLabel.Size = new System.Drawing.Size(27, 13);
            this.sizeLabel.TabIndex = 26;
            this.sizeLabel.Text = "Size";
            // 
            // mineralsLabel
            // 
            this.mineralsLabel.AutoSize = true;
            this.mineralsLabel.Location = new System.Drawing.Point(3, 66);
            this.mineralsLabel.Name = "mineralsLabel";
            this.mineralsLabel.Size = new System.Drawing.Size(46, 13);
            this.mineralsLabel.TabIndex = 25;
            this.mineralsLabel.Text = "Minerals";
            // 
            // captureButton
            // 
            this.captureButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.captureButton.ForeColor = System.Drawing.SystemColors.ControlText;
            this.captureButton.Location = new System.Drawing.Point(51, 409);
            this.captureButton.Name = "captureButton";
            this.captureButton.Size = new System.Drawing.Size(123, 42);
            this.captureButton.TabIndex = 14;
            this.captureButton.Text = "Захватить систему";
            this.captureButton.UseVisualStyleBackColor = true;
            // 
            // skillPointLabel
            // 
            this.skillPointLabel.AutoSize = true;
            this.skillPointLabel.Location = new System.Drawing.Point(3, 193);
            this.skillPointLabel.Name = "skillPointLabel";
            this.skillPointLabel.Size = new System.Drawing.Size(51, 13);
            this.skillPointLabel.TabIndex = 15;
            this.skillPointLabel.Text = "skill Point";
            // 
            // StarSystemForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(710, 463);
            this.Controls.Add(this.captureButton);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.pictureBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "StarSystemForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.TopMost = true;
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label ownerLabel;
        private System.Windows.Forms.Label profitLabel;
        private System.Windows.Forms.Label populationLabel;
        private System.Windows.Forms.Label sizeLabel;
        private System.Windows.Forms.Label mineralsLabel;
        private System.Windows.Forms.TextBox buildingsTextBox;
        public System.Windows.Forms.Button captureButton;
        private System.Windows.Forms.Label climateLabel;
        private System.Windows.Forms.Button showAllButton;
        private System.Windows.Forms.Label skillPointLabel;
    }
}

