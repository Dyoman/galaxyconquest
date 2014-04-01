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
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.TechTreeImage)).BeginInit();
            this.SuspendLayout();
            // 
            // TechTreeImage
            // 
            this.TechTreeImage.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TechTreeImage.BackColor = System.Drawing.Color.Black;
            this.TechTreeImage.Location = new System.Drawing.Point(82, 12);
            this.TechTreeImage.Name = "TechTreeImage";
            this.TechTreeImage.Size = new System.Drawing.Size(698, 549);
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
            this.buttonScalingDown.Location = new System.Drawing.Point(27, 39);
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
            this.buttonScalingUp.Location = new System.Drawing.Point(27, 12);
            this.buttonScalingUp.Name = "buttonScalingUp";
            this.buttonScalingUp.Size = new System.Drawing.Size(21, 21);
            this.buttonScalingUp.TabIndex = 10;
            this.buttonScalingUp.UseVisualStyleBackColor = true;
            this.buttonScalingUp.Click += new System.EventHandler(this.buttonScalingUp_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 79);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 12;
            this.label1.Text = "label1";
            // 
            // Tech_Tree
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(792, 573);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonScalingDown);
            this.Controls.Add(this.buttonScalingUp);
            this.Controls.Add(this.TechTreeImage);
            this.Name = "Tech_Tree";
            this.Text = "Tech_Tree";
            this.Resize += new System.EventHandler(this.Tech_Tree_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.TechTreeImage)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox TechTreeImage;
        private System.Windows.Forms.Button buttonScalingDown;
        private System.Windows.Forms.Button buttonScalingUp;
        private System.Windows.Forms.Label label1;

    }
}