namespace GalaxyConquest
{
    partial class Form1
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
            this.mainMenu = new System.Windows.Forms.MenuStrip();
            this.mainMenuFile = new System.Windows.Forms.ToolStripMenuItem();
            this.mainMenuNew = new System.Windows.Forms.ToolStripMenuItem();
            this.mainMenuOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.mainMenuSave = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.mainMenuQuit = new System.Windows.Forms.ToolStripMenuItem();
            this.mainMenuText = new System.Windows.Forms.ToolStripMenuItem();
            this.mainMenuAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.MainMenuTechTree = new System.Windows.Forms.ToolStripMenuItem();
            this.dModelsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.buttonDraw = new System.Windows.Forms.Button();
            this.buttonSpinLeft = new System.Windows.Forms.Button();
            this.buttonSpinRight = new System.Windows.Forms.Button();
            this.buttonSpinUp = new System.Windows.Forms.Button();
            this.buttonSpinDown = new System.Windows.Forms.Button();
            this.buttonScalingUp = new System.Windows.Forms.Button();
            this.buttonScalingDown = new System.Windows.Forms.Button();
            this.buttonMoveUp = new System.Windows.Forms.Button();
            this.buttonMoveLeft = new System.Windows.Forms.Button();
            this.buttonMoveRight = new System.Windows.Forms.Button();
            this.buttonMoveDown = new System.Windows.Forms.Button();
            this.galaxyImage = new System.Windows.Forms.PictureBox();
            this.vScrollBar1 = new System.Windows.Forms.VScrollBar();
            this.hScrollBar1 = new System.Windows.Forms.HScrollBar();
            this.mainMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.galaxyImage)).BeginInit();
            this.SuspendLayout();
            // 
            // mainMenu
            // 
            this.mainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mainMenuFile,
            this.mainMenuText,
            this.MainMenuTechTree,
            this.dModelsToolStripMenuItem});
            this.mainMenu.Location = new System.Drawing.Point(0, 0);
            this.mainMenu.Name = "mainMenu";
            this.mainMenu.Size = new System.Drawing.Size(580, 24);
            this.mainMenu.TabIndex = 0;
            this.mainMenu.Text = "menuStrip1";
            // 
            // mainMenuFile
            // 
            this.mainMenuFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mainMenuNew,
            this.mainMenuOpen,
            this.mainMenuSave,
            this.toolStripSeparator1,
            this.mainMenuQuit});
            this.mainMenuFile.Name = "mainMenuFile";
            this.mainMenuFile.Size = new System.Drawing.Size(35, 20);
            this.mainMenuFile.Text = "File";
            // 
            // mainMenuNew
            // 
            this.mainMenuNew.Name = "mainMenuNew";
            this.mainMenuNew.Size = new System.Drawing.Size(112, 22);
            this.mainMenuNew.Text = "New";
            this.mainMenuNew.Click += new System.EventHandler(this.mainMenuNew_Click);
            // 
            // mainMenuOpen
            // 
            this.mainMenuOpen.Name = "mainMenuOpen";
            this.mainMenuOpen.Size = new System.Drawing.Size(112, 22);
            this.mainMenuOpen.Text = "Open...";
            this.mainMenuOpen.Click += new System.EventHandler(this.mainMenuOpen_Click);
            // 
            // mainMenuSave
            // 
            this.mainMenuSave.Name = "mainMenuSave";
            this.mainMenuSave.Size = new System.Drawing.Size(112, 22);
            this.mainMenuSave.Text = "Save...";
            this.mainMenuSave.Click += new System.EventHandler(this.mainMenuSave_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(109, 6);
            // 
            // mainMenuQuit
            // 
            this.mainMenuQuit.Name = "mainMenuQuit";
            this.mainMenuQuit.Size = new System.Drawing.Size(112, 22);
            this.mainMenuQuit.Text = "Quit";
            this.mainMenuQuit.Click += new System.EventHandler(this.mainMenuQuit_Click);
            // 
            // mainMenuText
            // 
            this.mainMenuText.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.mainMenuText.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mainMenuAbout});
            this.mainMenuText.Name = "mainMenuText";
            this.mainMenuText.Size = new System.Drawing.Size(40, 20);
            this.mainMenuText.Text = "Help";
            // 
            // mainMenuAbout
            // 
            this.mainMenuAbout.Name = "mainMenuAbout";
            this.mainMenuAbout.Size = new System.Drawing.Size(115, 22);
            this.mainMenuAbout.Text = "About...";
            this.mainMenuAbout.Click += new System.EventHandler(this.mainMenuAbout_Click);
            // 
            // MainMenuTechTree
            // 
            this.MainMenuTechTree.Name = "MainMenuTechTree";
            this.MainMenuTechTree.Size = new System.Drawing.Size(65, 20);
            this.MainMenuTechTree.Text = "Tech tree";
            this.MainMenuTechTree.Click += new System.EventHandler(this.MainMenuTechTree_Click);
            // 
            // dModelsToolStripMenuItem
            // 
            this.dModelsToolStripMenuItem.Name = "dModelsToolStripMenuItem";
            this.dModelsToolStripMenuItem.Size = new System.Drawing.Size(67, 20);
            this.dModelsToolStripMenuItem.Text = "3d models";
            this.dModelsToolStripMenuItem.Click += new System.EventHandler(this.dModelsToolStripMenuItem_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Location = new System.Drawing.Point(0, 324);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(580, 22);
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // buttonDraw
            // 
            this.buttonDraw.Location = new System.Drawing.Point(56, 40);
            this.buttonDraw.Name = "buttonDraw";
            this.buttonDraw.Size = new System.Drawing.Size(75, 23);
            this.buttonDraw.TabIndex = 3;
            this.buttonDraw.Text = "Draw";
            this.buttonDraw.UseVisualStyleBackColor = true;
            this.buttonDraw.Click += new System.EventHandler(this.buttonDraw_Click);
            // 
            // buttonSpinLeft
            // 
            this.buttonSpinLeft.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSpinLeft.Location = new System.Drawing.Point(290, 298);
            this.buttonSpinLeft.Name = "buttonSpinLeft";
            this.buttonSpinLeft.Size = new System.Drawing.Size(75, 23);
            this.buttonSpinLeft.TabIndex = 4;
            this.buttonSpinLeft.Text = "<---";
            this.buttonSpinLeft.UseVisualStyleBackColor = true;
            this.buttonSpinLeft.Click += new System.EventHandler(this.buttonSpinLeft_Click);
            // 
            // buttonSpinRight
            // 
            this.buttonSpinRight.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSpinRight.Location = new System.Drawing.Point(371, 298);
            this.buttonSpinRight.Name = "buttonSpinRight";
            this.buttonSpinRight.Size = new System.Drawing.Size(75, 23);
            this.buttonSpinRight.TabIndex = 5;
            this.buttonSpinRight.Text = "--->";
            this.buttonSpinRight.UseVisualStyleBackColor = true;
            this.buttonSpinRight.Click += new System.EventHandler(this.buttonSpinRight_Click);
            // 
            // buttonSpinUp
            // 
            this.buttonSpinUp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSpinUp.Location = new System.Drawing.Point(543, 70);
            this.buttonSpinUp.Name = "buttonSpinUp";
            this.buttonSpinUp.Size = new System.Drawing.Size(25, 66);
            this.buttonSpinUp.TabIndex = 6;
            this.buttonSpinUp.Text = "/\\";
            this.buttonSpinUp.UseVisualStyleBackColor = true;
            this.buttonSpinUp.Click += new System.EventHandler(this.buttonSpinUp_Click);
            // 
            // buttonSpinDown
            // 
            this.buttonSpinDown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSpinDown.Location = new System.Drawing.Point(543, 142);
            this.buttonSpinDown.Name = "buttonSpinDown";
            this.buttonSpinDown.Size = new System.Drawing.Size(25, 66);
            this.buttonSpinDown.TabIndex = 7;
            this.buttonSpinDown.Text = "\\/";
            this.buttonSpinDown.UseVisualStyleBackColor = true;
            this.buttonSpinDown.Click += new System.EventHandler(this.buttonSpinDown_Click);
            // 
            // buttonScalingUp
            // 
            this.buttonScalingUp.Location = new System.Drawing.Point(56, 207);
            this.buttonScalingUp.Name = "buttonScalingUp";
            this.buttonScalingUp.Size = new System.Drawing.Size(75, 23);
            this.buttonScalingUp.TabIndex = 8;
            this.buttonScalingUp.Text = "+";
            this.buttonScalingUp.UseVisualStyleBackColor = true;
            this.buttonScalingUp.Click += new System.EventHandler(this.buttonScalingUp_Click);
            // 
            // buttonScalingDown
            // 
            this.buttonScalingDown.Location = new System.Drawing.Point(56, 236);
            this.buttonScalingDown.Name = "buttonScalingDown";
            this.buttonScalingDown.Size = new System.Drawing.Size(75, 23);
            this.buttonScalingDown.TabIndex = 9;
            this.buttonScalingDown.Text = "-";
            this.buttonScalingDown.UseVisualStyleBackColor = true;
            this.buttonScalingDown.Click += new System.EventHandler(this.buttonScalingDown_Click);
            // 
            // buttonMoveUp
            // 
            this.buttonMoveUp.Location = new System.Drawing.Point(56, 106);
            this.buttonMoveUp.Name = "buttonMoveUp";
            this.buttonMoveUp.Size = new System.Drawing.Size(75, 23);
            this.buttonMoveUp.TabIndex = 10;
            this.buttonMoveUp.Text = "up";
            this.buttonMoveUp.UseVisualStyleBackColor = true;
            this.buttonMoveUp.Click += new System.EventHandler(this.buttonMoveUp_Click);
            // 
            // buttonMoveLeft
            // 
            this.buttonMoveLeft.Location = new System.Drawing.Point(12, 135);
            this.buttonMoveLeft.Name = "buttonMoveLeft";
            this.buttonMoveLeft.Size = new System.Drawing.Size(75, 23);
            this.buttonMoveLeft.TabIndex = 11;
            this.buttonMoveLeft.Text = "left";
            this.buttonMoveLeft.UseVisualStyleBackColor = true;
            this.buttonMoveLeft.Click += new System.EventHandler(this.buttonMoveLeft_Click);
            // 
            // buttonMoveRight
            // 
            this.buttonMoveRight.Location = new System.Drawing.Point(93, 135);
            this.buttonMoveRight.Name = "buttonMoveRight";
            this.buttonMoveRight.Size = new System.Drawing.Size(75, 23);
            this.buttonMoveRight.TabIndex = 12;
            this.buttonMoveRight.Text = "right";
            this.buttonMoveRight.UseVisualStyleBackColor = true;
            this.buttonMoveRight.Click += new System.EventHandler(this.buttonMoveRight_Click);
            // 
            // buttonMoveDown
            // 
            this.buttonMoveDown.Location = new System.Drawing.Point(56, 164);
            this.buttonMoveDown.Name = "buttonMoveDown";
            this.buttonMoveDown.Size = new System.Drawing.Size(75, 23);
            this.buttonMoveDown.TabIndex = 13;
            this.buttonMoveDown.Text = "down";
            this.buttonMoveDown.UseVisualStyleBackColor = true;
            this.buttonMoveDown.Click += new System.EventHandler(this.buttonMoveDown_Click);
            // 
            // galaxyImage
            // 
            this.galaxyImage.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.galaxyImage.BackColor = System.Drawing.Color.Black;
            this.galaxyImage.Location = new System.Drawing.Point(174, 27);
            this.galaxyImage.Name = "galaxyImage";
            this.galaxyImage.Size = new System.Drawing.Size(363, 265);
            this.galaxyImage.TabIndex = 1;
            this.galaxyImage.TabStop = false;
            this.galaxyImage.MouseClick += new System.Windows.Forms.MouseEventHandler(this.galaxyImage_MouseClick);
            this.galaxyImage.MouseDown += new System.Windows.Forms.MouseEventHandler(this.galaxyImage_MouseDown);
            this.galaxyImage.MouseMove += new System.Windows.Forms.MouseEventHandler(this.galaxyImage_MouseMove);
            // 
            // vScrollBar1
            // 
            this.vScrollBar1.Dock = System.Windows.Forms.DockStyle.Right;
            this.vScrollBar1.LargeChange = 100;
            this.vScrollBar1.Location = new System.Drawing.Point(564, 24);
            this.vScrollBar1.Maximum = 500;
            this.vScrollBar1.Minimum = -500;
            this.vScrollBar1.Name = "vScrollBar1";
            this.vScrollBar1.Size = new System.Drawing.Size(16, 284);
            this.vScrollBar1.SmallChange = 50;
            this.vScrollBar1.TabIndex = 15;
            this.vScrollBar1.Scroll += new System.Windows.Forms.ScrollEventHandler(this.vScrollBar1_Scroll);
            // 
            // hScrollBar1
            // 
            this.hScrollBar1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.hScrollBar1.LargeChange = 100;
            this.hScrollBar1.Location = new System.Drawing.Point(0, 308);
            this.hScrollBar1.Maximum = 500;
            this.hScrollBar1.Minimum = -500;
            this.hScrollBar1.Name = "hScrollBar1";
            this.hScrollBar1.Size = new System.Drawing.Size(580, 16);
            this.hScrollBar1.SmallChange = 50;
            this.hScrollBar1.TabIndex = 14;
            this.hScrollBar1.Scroll += new System.Windows.Forms.ScrollEventHandler(this.hScrollBar1_Scroll);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(580, 346);
            this.Controls.Add(this.vScrollBar1);
            this.Controls.Add(this.hScrollBar1);
            this.Controls.Add(this.buttonMoveDown);
            this.Controls.Add(this.buttonMoveRight);
            this.Controls.Add(this.buttonMoveLeft);
            this.Controls.Add(this.buttonMoveUp);
            this.Controls.Add(this.buttonScalingDown);
            this.Controls.Add(this.buttonScalingUp);
            this.Controls.Add(this.buttonSpinDown);
            this.Controls.Add(this.buttonSpinUp);
            this.Controls.Add(this.buttonSpinRight);
            this.Controls.Add(this.buttonSpinLeft);
            this.Controls.Add(this.buttonDraw);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.galaxyImage);
            this.Controls.Add(this.mainMenu);
            this.MainMenuStrip = this.mainMenu;
            this.Name = "Form1";
            this.Text = "GalaxyConquest";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Form1_Load);
            this.mainMenu.ResumeLayout(false);
            this.mainMenu.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.galaxyImage)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip mainMenu;
        private System.Windows.Forms.ToolStripMenuItem mainMenuFile;
        private System.Windows.Forms.ToolStripMenuItem mainMenuQuit;
        private System.Windows.Forms.PictureBox galaxyImage;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripMenuItem mainMenuNew;
        private System.Windows.Forms.Button buttonDraw;
        private System.Windows.Forms.Button buttonSpinLeft;
        private System.Windows.Forms.Button buttonSpinRight;
        private System.Windows.Forms.Button buttonSpinUp;
        private System.Windows.Forms.Button buttonSpinDown;
        private System.Windows.Forms.ToolStripMenuItem mainMenuOpen;
        private System.Windows.Forms.ToolStripMenuItem mainMenuSave;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem mainMenuText;
        private System.Windows.Forms.ToolStripMenuItem mainMenuAbout;
        private System.Windows.Forms.Button buttonScalingUp;
        private System.Windows.Forms.Button buttonScalingDown;
        private System.Windows.Forms.Button buttonMoveUp;
        private System.Windows.Forms.Button buttonMoveLeft;
        private System.Windows.Forms.Button buttonMoveRight;
        private System.Windows.Forms.Button buttonMoveDown;
        private System.Windows.Forms.ToolStripMenuItem MainMenuTechTree;
        private System.Windows.Forms.ToolStripMenuItem dModelsToolStripMenuItem;
        private System.Windows.Forms.VScrollBar vScrollBar1;
        private System.Windows.Forms.HScrollBar hScrollBar1;
    }
}

