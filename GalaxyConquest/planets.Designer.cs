namespace GalaxyConquest
{
    partial class planets
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
            this.planetsViev = new Tao.Platform.Windows.SimpleOpenGlControl();
            this.showbutton = new System.Windows.Forms.Button();
            this.buttonSpinUp = new System.Windows.Forms.Button();
            this.buttonSpinRight = new System.Windows.Forms.Button();
            this.buttonSpinLeft = new System.Windows.Forms.Button();
            this.buttonSpinDown = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // planetsViev
            // 
            this.planetsViev.AccumBits = ((byte)(0));
            this.planetsViev.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.planetsViev.AutoCheckErrors = false;
            this.planetsViev.AutoFinish = false;
            this.planetsViev.AutoMakeCurrent = true;
            this.planetsViev.AutoSwapBuffers = true;
            this.planetsViev.BackColor = System.Drawing.Color.Black;
            this.planetsViev.ColorBits = ((byte)(32));
            this.planetsViev.DepthBits = ((byte)(16));
            this.planetsViev.Location = new System.Drawing.Point(13, 13);
            this.planetsViev.Name = "planetsViev";
            this.planetsViev.Size = new System.Drawing.Size(382, 290);
            this.planetsViev.StencilBits = ((byte)(0));
            this.planetsViev.TabIndex = 0;
            // 
            // showbutton
            // 
            this.showbutton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.showbutton.Location = new System.Drawing.Point(443, 13);
            this.showbutton.Name = "showbutton";
            this.showbutton.Size = new System.Drawing.Size(75, 23);
            this.showbutton.TabIndex = 1;
            this.showbutton.Text = "show";
            this.showbutton.UseVisualStyleBackColor = true;
            this.showbutton.Click += new System.EventHandler(this.showbutton_Click);
            // 
            // buttonSpinUp
            // 
            this.buttonSpinUp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSpinUp.Location = new System.Drawing.Point(443, 153);
            this.buttonSpinUp.Name = "buttonSpinUp";
            this.buttonSpinUp.Size = new System.Drawing.Size(75, 23);
            this.buttonSpinUp.TabIndex = 2;
            this.buttonSpinUp.Text = "Up";
            this.buttonSpinUp.UseVisualStyleBackColor = true;
            this.buttonSpinUp.Click += new System.EventHandler(this.buttonSpinUp_Click);
            // 
            // buttonSpinRight
            // 
            this.buttonSpinRight.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSpinRight.Location = new System.Drawing.Point(482, 182);
            this.buttonSpinRight.Name = "buttonSpinRight";
            this.buttonSpinRight.Size = new System.Drawing.Size(75, 23);
            this.buttonSpinRight.TabIndex = 3;
            this.buttonSpinRight.Text = "Right";
            this.buttonSpinRight.UseVisualStyleBackColor = true;
            this.buttonSpinRight.Click += new System.EventHandler(this.buttonSpinRight_Click);
            // 
            // buttonSpinLeft
            // 
            this.buttonSpinLeft.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSpinLeft.Location = new System.Drawing.Point(401, 182);
            this.buttonSpinLeft.Name = "buttonSpinLeft";
            this.buttonSpinLeft.Size = new System.Drawing.Size(75, 23);
            this.buttonSpinLeft.TabIndex = 4;
            this.buttonSpinLeft.Text = "Left";
            this.buttonSpinLeft.UseVisualStyleBackColor = true;
            this.buttonSpinLeft.Click += new System.EventHandler(this.buttonSpinLeft_Click);
            // 
            // buttonSpinDown
            // 
            this.buttonSpinDown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSpinDown.Location = new System.Drawing.Point(443, 211);
            this.buttonSpinDown.Name = "buttonSpinDown";
            this.buttonSpinDown.Size = new System.Drawing.Size(75, 23);
            this.buttonSpinDown.TabIndex = 5;
            this.buttonSpinDown.Text = "Down";
            this.buttonSpinDown.UseVisualStyleBackColor = true;
            this.buttonSpinDown.Click += new System.EventHandler(this.buttonSpinDown_Click);
            // 
            // planets
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(569, 315);
            this.Controls.Add(this.buttonSpinDown);
            this.Controls.Add(this.buttonSpinLeft);
            this.Controls.Add(this.buttonSpinRight);
            this.Controls.Add(this.buttonSpinUp);
            this.Controls.Add(this.showbutton);
            this.Controls.Add(this.planetsViev);
            this.Name = "planets";
            this.Text = "planets";
            this.Load += new System.EventHandler(this.planets_Load);
            this.Resize += new System.EventHandler(this.planets_Resize);
            this.ResumeLayout(false);

        }

        #endregion

        private Tao.Platform.Windows.SimpleOpenGlControl planetsViev;
        private System.Windows.Forms.Button showbutton;
        private System.Windows.Forms.Button buttonSpinUp;
        private System.Windows.Forms.Button buttonSpinRight;
        private System.Windows.Forms.Button buttonSpinLeft;
        private System.Windows.Forms.Button buttonSpinDown;
    }
}