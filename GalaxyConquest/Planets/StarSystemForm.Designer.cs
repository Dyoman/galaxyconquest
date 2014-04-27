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
            this.buildings = new System.Windows.Forms.TextBox();
            this.labelPlanetMinerals = new System.Windows.Forms.Label();
            this.labelPlanetSize = new System.Windows.Forms.Label();
            this.Populn = new System.Windows.Forms.Label();
            this.ownername = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.profit = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.labelPlanetName = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.labelPlanetPopulation = new System.Windows.Forms.Label();
            this.localstepbutton = new System.Windows.Forms.Button();
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
            this.panel1.Controls.Add(this.localstepbutton);
            this.panel1.Controls.Add(this.buildings);
            this.panel1.Controls.Add(this.labelPlanetMinerals);
            this.panel1.Controls.Add(this.labelPlanetSize);
            this.panel1.Controls.Add(this.Populn);
            this.panel1.Controls.Add(this.ownername);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.profit);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.labelPlanetName);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.label9);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label10);
            this.panel1.Controls.Add(this.labelPlanetPopulation);
            this.panel1.Location = new System.Drawing.Point(12, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(162, 350);
            this.panel1.TabIndex = 1;
            // 
            // buildings
            // 
            this.buildings.Location = new System.Drawing.Point(16, 192);
            this.buildings.Multiline = true;
            this.buildings.Name = "buildings";
            this.buildings.ReadOnly = true;
            this.buildings.Size = new System.Drawing.Size(133, 114);
            this.buildings.TabIndex = 38;
            // 
            // labelPlanetMinerals
            // 
            this.labelPlanetMinerals.AutoSize = true;
            this.labelPlanetMinerals.Location = new System.Drawing.Point(114, 65);
            this.labelPlanetMinerals.Name = "labelPlanetMinerals";
            this.labelPlanetMinerals.Size = new System.Drawing.Size(35, 13);
            this.labelPlanetMinerals.TabIndex = 37;
            this.labelPlanetMinerals.Text = "label6";
            // 
            // labelPlanetSize
            // 
            this.labelPlanetSize.AutoSize = true;
            this.labelPlanetSize.Location = new System.Drawing.Point(114, 38);
            this.labelPlanetSize.Name = "labelPlanetSize";
            this.labelPlanetSize.Size = new System.Drawing.Size(35, 13);
            this.labelPlanetSize.TabIndex = 36;
            this.labelPlanetSize.Text = "label5";
            // 
            // Populn
            // 
            this.Populn.AutoSize = true;
            this.Populn.Location = new System.Drawing.Point(114, 91);
            this.Populn.Name = "Populn";
            this.Populn.Size = new System.Drawing.Size(35, 13);
            this.Populn.TabIndex = 24;
            this.Populn.Text = "label5";
            // 
            // ownername
            // 
            this.ownername.AutoSize = true;
            this.ownername.Location = new System.Drawing.Point(114, 176);
            this.ownername.Name = "ownername";
            this.ownername.Size = new System.Drawing.Size(35, 13);
            this.ownername.TabIndex = 35;
            this.ownername.Text = "label7";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(13, 176);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(38, 13);
            this.label6.TabIndex = 32;
            this.label6.Text = "Owner";
            // 
            // profit
            // 
            this.profit.AutoSize = true;
            this.profit.Location = new System.Drawing.Point(114, 147);
            this.profit.Name = "profit";
            this.profit.Size = new System.Drawing.Size(35, 13);
            this.profit.TabIndex = 34;
            this.profit.Text = "label8";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(13, 147);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(31, 13);
            this.label7.TabIndex = 33;
            this.label7.Text = "Profit";
            // 
            // labelPlanetName
            // 
            this.labelPlanetName.AutoSize = true;
            this.labelPlanetName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelPlanetName.Location = new System.Drawing.Point(13, 13);
            this.labelPlanetName.Name = "labelPlanetName";
            this.labelPlanetName.Size = new System.Drawing.Size(41, 13);
            this.labelPlanetName.TabIndex = 9;
            this.labelPlanetName.Text = "label1";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 91);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 13);
            this.label1.TabIndex = 31;
            this.label1.Text = "Population Now";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(13, 38);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(27, 13);
            this.label9.TabIndex = 26;
            this.label9.Text = "Size";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 119);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(105, 13);
            this.label2.TabIndex = 30;
            this.label2.Text = "Population Max (mln)";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(13, 65);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(46, 13);
            this.label10.TabIndex = 25;
            this.label10.Text = "Minerals";
            // 
            // labelPlanetPopulation
            // 
            this.labelPlanetPopulation.AutoSize = true;
            this.labelPlanetPopulation.Location = new System.Drawing.Point(114, 119);
            this.labelPlanetPopulation.Name = "labelPlanetPopulation";
            this.labelPlanetPopulation.Size = new System.Drawing.Size(35, 13);
            this.labelPlanetPopulation.TabIndex = 29;
            this.labelPlanetPopulation.Text = "label7";
            // 
            // localstepbutton
            // 
            this.localstepbutton.Location = new System.Drawing.Point(43, 312);
            this.localstepbutton.Name = "localstepbutton";
            this.localstepbutton.Size = new System.Drawing.Size(75, 23);
            this.localstepbutton.TabIndex = 39;
            this.localstepbutton.Text = "step";
            this.localstepbutton.UseVisualStyleBackColor = true;
            this.localstepbutton.Click += new System.EventHandler(this.localstepbutton_Click);
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
        private System.Windows.Forms.Label labelPlanetName;
        private System.Windows.Forms.Label Populn;
        private System.Windows.Forms.Label ownername;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label profit;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label labelPlanetPopulation;
        private System.Windows.Forms.Label labelPlanetMinerals;
        private System.Windows.Forms.Label labelPlanetSize;
        private System.Windows.Forms.TextBox buildings;
        private System.Windows.Forms.Button localstepbutton;
    }
}

