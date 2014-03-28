namespace GalaxyConquest
{
    partial class Tech
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
            ((System.ComponentModel.ISupportInitialize)(this.TechTreeImage)).BeginInit();
            this.SuspendLayout();
            // 
            // TechTreeImage
            // 
            this.TechTreeImage.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TechTreeImage.BackColor = System.Drawing.Color.Black;
            this.TechTreeImage.Location = new System.Drawing.Point(123, 18);
            this.TechTreeImage.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.TechTreeImage.Name = "TechTreeImage";
            this.TechTreeImage.Size = new System.Drawing.Size(585, 432);
            this.TechTreeImage.TabIndex = 2;
            this.TechTreeImage.TabStop = false;
            this.TechTreeImage.MouseDown += new System.Windows.Forms.MouseEventHandler(this.TechTreeImage_MouseDown);
            this.TechTreeImage.MouseMove += new System.Windows.Forms.MouseEventHandler(this.TechTreeImage_MouseMove);
            // 
            // buttonScalingDown
            // 
            this.buttonScalingDown.FlatAppearance.BorderSize = 0;
            this.buttonScalingDown.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonScalingDown.Image = global::GalaxyConquest.Properties.Resources.btn_minus;
            this.buttonScalingDown.Location = new System.Drawing.Point(40, 60);
            this.buttonScalingDown.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.buttonScalingDown.Name = "buttonScalingDown";
            this.buttonScalingDown.Size = new System.Drawing.Size(32, 32);
            this.buttonScalingDown.TabIndex = 11;
            this.buttonScalingDown.UseVisualStyleBackColor = true;
            this.buttonScalingDown.Click += new System.EventHandler(this.buttonScalingDown_Click);
            // 
            // buttonScalingUp
            // 
            this.buttonScalingUp.FlatAppearance.BorderSize = 0;
            this.buttonScalingUp.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonScalingUp.Image = global::GalaxyConquest.Properties.Resources.btn_plus;
            this.buttonScalingUp.Location = new System.Drawing.Point(40, 18);
            this.buttonScalingUp.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.buttonScalingUp.Name = "buttonScalingUp";
            this.buttonScalingUp.Size = new System.Drawing.Size(32, 32);
            this.buttonScalingUp.TabIndex = 10;
            this.buttonScalingUp.UseVisualStyleBackColor = true;
            this.buttonScalingUp.Click += new System.EventHandler(this.buttonScalingUp_Click);
            // 
            // Tech_Tree
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(726, 469);
            this.Controls.Add(this.buttonScalingDown);
            this.Controls.Add(this.buttonScalingUp);
            this.Controls.Add(this.TechTreeImage);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "Tech_Tree";
            this.Text = "Tech_Tree";
            this.Resize += new System.EventHandler(this.Tech_Tree_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.TechTreeImage)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox TechTreeImage;
        private System.Windows.Forms.Button buttonScalingDown;
        private System.Windows.Forms.Button buttonScalingUp;

    }
}