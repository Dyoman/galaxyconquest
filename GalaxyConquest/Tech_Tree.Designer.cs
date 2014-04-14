namespace GalaxyConquest
{
    partial class Tech_Tree
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
            this.TechTreeImage = new System.Windows.Forms.PictureBox();
            this.buttonScalingDown = new System.Windows.Forms.Button();
            this.buttonScalingUp = new System.Windows.Forms.Button();
            this.learn_tech_button = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.properties_tech_textBox = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.TechTreeImage)).BeginInit();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // TechTreeImage
            // 
            this.TechTreeImage.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TechTreeImage.BackColor = System.Drawing.Color.Black;
            this.TechTreeImage.Location = new System.Drawing.Point(118, 12);
            this.TechTreeImage.Name = "TechTreeImage";
            this.TechTreeImage.Size = new System.Drawing.Size(662, 549);
            this.TechTreeImage.TabIndex = 2;
            this.TechTreeImage.TabStop = false;
            this.TechTreeImage.MouseClick += new System.Windows.Forms.MouseEventHandler(this.TechTreeImage_MouseClick);
            this.TechTreeImage.MouseDown += new System.Windows.Forms.MouseEventHandler(this.TechTreeImage_MouseDown);
            this.TechTreeImage.MouseMove += new System.Windows.Forms.MouseEventHandler(this.TechTreeImage_MouseMove);
            // 
            // buttonScalingDown
            // 
            this.buttonScalingDown.FlatAppearance.BorderSize = 0;
            this.buttonScalingDown.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonScalingDown.Image = global::GalaxyConquest.Properties.Resources.btn_minus;
            this.buttonScalingDown.Location = new System.Drawing.Point(40, 30);
            this.buttonScalingDown.Name = "buttonScalingDown";
            this.buttonScalingDown.Size = new System.Drawing.Size(21, 21);
            this.buttonScalingDown.TabIndex = 11;
            this.buttonScalingDown.UseVisualStyleBackColor = true;
            this.buttonScalingDown.Click += new System.EventHandler(this.buttonScalingDown_Click);
            // 
            // buttonScalingUp
            // 
            this.buttonScalingUp.FlatAppearance.BorderSize = 0;
            this.buttonScalingUp.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonScalingUp.Image = global::GalaxyConquest.Properties.Resources.btn_plus;
            this.buttonScalingUp.Location = new System.Drawing.Point(40, 3);
            this.buttonScalingUp.Name = "buttonScalingUp";
            this.buttonScalingUp.Size = new System.Drawing.Size(21, 21);
            this.buttonScalingUp.TabIndex = 10;
            this.buttonScalingUp.UseVisualStyleBackColor = true;
            this.buttonScalingUp.Click += new System.EventHandler(this.buttonScalingUp_Click);
            // 
            // learn_tech_button
            // 
            this.learn_tech_button.Location = new System.Drawing.Point(12, 225);
            this.learn_tech_button.Name = "learn_tech_button";
            this.learn_tech_button.Size = new System.Drawing.Size(75, 23);
            this.learn_tech_button.TabIndex = 13;
            this.learn_tech_button.Text = "Learn";
            this.learn_tech_button.UseVisualStyleBackColor = true;
            this.learn_tech_button.Click += new System.EventHandler(this.learn_tech_button_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Controls.Add(this.buttonScalingUp);
            this.panel1.Controls.Add(this.learn_tech_button);
            this.panel1.Controls.Add(this.buttonScalingDown);
            this.panel1.Location = new System.Drawing.Point(12, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(100, 260);
            this.panel1.TabIndex = 14;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.properties_tech_textBox);
            this.groupBox1.Location = new System.Drawing.Point(3, 104);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(94, 100);
            this.groupBox1.TabIndex = 14;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "groupBox1";
            this.groupBox1.Visible = false;
            // 
            // properties_tech_textBox
            // 
            this.properties_tech_textBox.Location = new System.Drawing.Point(6, 19);
            this.properties_tech_textBox.Multiline = true;
            this.properties_tech_textBox.Name = "properties_tech_textBox";
            this.properties_tech_textBox.Size = new System.Drawing.Size(82, 75);
            this.properties_tech_textBox.TabIndex = 13;
            // 
            // Tech_Tree
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(792, 573);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.TechTreeImage);
            this.Name = "Tech_Tree";
            this.Text = "Tech_Tree";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Tech_Tree_FormClosed);
            this.VisibleChanged += new System.EventHandler(this.Tech_Tree_VisibleChanged);
            this.Resize += new System.EventHandler(this.Tech_Tree_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.TechTreeImage)).EndInit();
            this.panel1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox TechTreeImage;
        private System.Windows.Forms.Button buttonScalingDown;
        private System.Windows.Forms.Button buttonScalingUp;
        private System.Windows.Forms.Button learn_tech_button;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox properties_tech_textBox;

    }
}