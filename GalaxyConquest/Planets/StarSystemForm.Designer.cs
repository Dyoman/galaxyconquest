namespace GalaxyConquest.StarSystems
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
            this.labelPlanetPopulation = new System.Windows.Forms.Label();
            this.labelPlanetMinerals = new System.Windows.Forms.Label();
            this.labelPlanetSize = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.labelPlanetName = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Black;
            this.pictureBox1.Location = new System.Drawing.Point(194, 2);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(383, 371);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
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
            this.panel1.Controls.Add(this.labelPlanetPopulation);
            this.panel1.Controls.Add(this.labelPlanetMinerals);
            this.panel1.Controls.Add(this.labelPlanetSize);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.labelPlanetName);
            this.panel1.Location = new System.Drawing.Point(12, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(162, 210);
            this.panel1.TabIndex = 1;
            // 
            // label7
            // 
            this.labelPlanetPopulation.AutoSize = true;
            this.labelPlanetPopulation.Location = new System.Drawing.Point(114, 98);
            this.labelPlanetPopulation.Name = "label7";
            this.labelPlanetPopulation.Size = new System.Drawing.Size(35, 13);
            this.labelPlanetPopulation.TabIndex = 15;
            this.labelPlanetPopulation.Text = "label7";
            // 
            // label6
            // 
            this.labelPlanetMinerals.AutoSize = true;
            this.labelPlanetMinerals.Location = new System.Drawing.Point(114, 70);
            this.labelPlanetMinerals.Name = "label6";
            this.labelPlanetMinerals.Size = new System.Drawing.Size(35, 13);
            this.labelPlanetMinerals.TabIndex = 14;
            this.labelPlanetMinerals.Text = "label6";
            // 
            // label5
            // 
            this.labelPlanetSize.AutoSize = true;
            this.labelPlanetSize.Location = new System.Drawing.Point(114, 43);
            this.labelPlanetSize.Name = "label5";
            this.labelPlanetSize.Size = new System.Drawing.Size(35, 13);
            this.labelPlanetSize.TabIndex = 13;
            this.labelPlanetSize.Text = "label5";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 43);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(27, 13);
            this.label4.TabIndex = 12;
            this.label4.Text = "Size";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 70);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(46, 13);
            this.label3.TabIndex = 11;
            this.label3.Text = "Minerals";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 98);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(82, 13);
            this.label2.TabIndex = 10;
            this.label2.Text = "Population (mln)";
            // 
            // label1
            // 
            this.labelPlanetName.AutoSize = true;
            this.labelPlanetName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelPlanetName.Location = new System.Drawing.Point(13, 13);
            this.labelPlanetName.Name = "label1";
            this.labelPlanetName.Size = new System.Drawing.Size(41, 13);
            this.labelPlanetName.TabIndex = 9;
            this.labelPlanetName.Text = "label1";
            // 
            // StarSystemForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(578, 374);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.pictureBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "StarSystemForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
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
        private System.Windows.Forms.Label labelPlanetPopulation;
        private System.Windows.Forms.Label labelPlanetMinerals;
        private System.Windows.Forms.Label labelPlanetSize;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label labelPlanetName;
    }
}

