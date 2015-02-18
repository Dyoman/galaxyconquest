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
            this.components = new System.ComponentModel.Container();
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
            this.toolStripStatusSelectFleet = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusXY = new System.Windows.Forms.ToolStripStatusLabel();
            this.step_button = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.conquer_progressBar = new System.Windows.Forms.ProgressBar();
            this.button3 = new System.Windows.Forms.Button();
            this.tech_progressBar = new System.Windows.Forms.ProgressBar();
            this.tech_label = new System.Windows.Forms.Label();
            this.sound_button = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.EnergyStatus = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.MineralStatus = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.CreditsStatus = new System.Windows.Forms.Label();
            this.Credits = new System.Windows.Forms.Label();
            this.marketButton = new System.Windows.Forms.Button();
            this.dateLabel = new System.Windows.Forms.Label();
            this.fleetsButton = new System.Windows.Forms.Button();
            this.listView = new System.Windows.Forms.ListView();
            this.systemsButton = new System.Windows.Forms.Button();
            this.galaxyNameLablel = new System.Windows.Forms.Label();
            this.StepWorker = new System.ComponentModel.BackgroundWorker();
            this.TechWorker = new System.ComponentModel.BackgroundWorker();
            this.GameTimer = new System.Windows.Forms.Timer(this.components);
            this.galaxyImage = new System.Windows.Forms.PictureBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.mainMenu.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
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
            this.mainMenu.Size = new System.Drawing.Size(1008, 24);
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
            this.mainMenuFile.Size = new System.Drawing.Size(37, 20);
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
            this.mainMenuSave.Enabled = false;
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
            this.mainMenuText.Size = new System.Drawing.Size(44, 20);
            this.mainMenuText.Text = "Help";
            // 
            // mainMenuAbout
            // 
            this.mainMenuAbout.Name = "mainMenuAbout";
            this.mainMenuAbout.Size = new System.Drawing.Size(116, 22);
            this.mainMenuAbout.Text = "About...";
            this.mainMenuAbout.Click += new System.EventHandler(this.mainMenuAbout_Click);
            // 
            // MainMenuTechTree
            // 
            this.MainMenuTechTree.Enabled = false;
            this.MainMenuTechTree.Name = "MainMenuTechTree";
            this.MainMenuTechTree.Size = new System.Drawing.Size(68, 20);
            this.MainMenuTechTree.Text = "Tech tree";
            this.MainMenuTechTree.Click += new System.EventHandler(this.MainMenuTechTree_Click);
            // 
            // dModelsToolStripMenuItem
            // 
            this.dModelsToolStripMenuItem.Name = "dModelsToolStripMenuItem";
            this.dModelsToolStripMenuItem.Size = new System.Drawing.Size(12, 20);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusSelectFleet,
            this.toolStripStatusXY});
            this.statusStrip1.Location = new System.Drawing.Point(0, 452);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1008, 22);
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusSelectFleet
            // 
            this.toolStripStatusSelectFleet.Name = "toolStripStatusSelectFleet";
            this.toolStripStatusSelectFleet.Size = new System.Drawing.Size(140, 17);
            this.toolStripStatusSelectFleet.Text = "toolStripStatusSelectFleet";
            // 
            // toolStripStatusXY
            // 
            this.toolStripStatusXY.Name = "toolStripStatusXY";
            this.toolStripStatusXY.Size = new System.Drawing.Size(98, 17);
            this.toolStripStatusXY.Text = "toolStripStatusXY";
            // 
            // step_button
            // 
            this.step_button.Font = new System.Drawing.Font("Impact", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.step_button.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.step_button.Location = new System.Drawing.Point(3, 3);
            this.step_button.Name = "step_button";
            this.step_button.Size = new System.Drawing.Size(153, 53);
            this.step_button.TabIndex = 18;
            this.step_button.Text = "STEP";
            this.step_button.UseVisualStyleBackColor = true;
            this.step_button.Click += new System.EventHandler(this.step_button_Click);
            // 
            // button2
            // 
            this.button2.Enabled = false;
            this.button2.Location = new System.Drawing.Point(17, 109);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(122, 23);
            this.button2.TabIndex = 19;
            this.button2.Text = "Захватить систему";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // conquer_progressBar
            // 
            this.conquer_progressBar.Location = new System.Drawing.Point(27, 138);
            this.conquer_progressBar.Maximum = 5;
            this.conquer_progressBar.Name = "conquer_progressBar";
            this.conquer_progressBar.Size = new System.Drawing.Size(100, 23);
            this.conquer_progressBar.Step = 1;
            this.conquer_progressBar.TabIndex = 20;
            this.conquer_progressBar.Visible = false;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(17, 167);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(122, 23);
            this.button3.TabIndex = 21;
            this.button3.Text = "Отменить захват";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Visible = false;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // tech_progressBar
            // 
            this.tech_progressBar.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.tech_progressBar.Location = new System.Drawing.Point(490, 27);
            this.tech_progressBar.Maximum = 5;
            this.tech_progressBar.Name = "tech_progressBar";
            this.tech_progressBar.Size = new System.Drawing.Size(100, 23);
            this.tech_progressBar.Step = 1;
            this.tech_progressBar.TabIndex = 22;
            this.tech_progressBar.Visible = false;
            // 
            // tech_label
            // 
            this.tech_label.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.tech_label.AutoSize = true;
            this.tech_label.Location = new System.Drawing.Point(442, 32);
            this.tech_label.Name = "tech_label";
            this.tech_label.Size = new System.Drawing.Size(35, 13);
            this.tech_label.TabIndex = 23;
            this.tech_label.Text = "label1";
            // 
            // sound_button
            // 
            this.sound_button.Location = new System.Drawing.Point(34, 62);
            this.sound_button.Name = "sound_button";
            this.sound_button.Size = new System.Drawing.Size(75, 23);
            this.sound_button.TabIndex = 24;
            this.sound_button.Text = "Mute";
            this.sound_button.UseVisualStyleBackColor = true;
            this.sound_button.Click += new System.EventHandler(this.sound_button_Click);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Controls.Add(this.sound_button);
            this.panel1.Controls.Add(this.marketButton);
            this.panel1.Controls.Add(this.button3);
            this.panel1.Controls.Add(this.conquer_progressBar);
            this.panel1.Controls.Add(this.button2);
            this.panel1.Controls.Add(this.step_button);
            this.panel1.Enabled = false;
            this.panel1.Location = new System.Drawing.Point(9, 27);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(159, 422);
            this.panel1.TabIndex = 25;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.EnergyStatus);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.MineralStatus);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.CreditsStatus);
            this.groupBox1.Controls.Add(this.Credits);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.groupBox1.Location = new System.Drawing.Point(16, 208);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(123, 96);
            this.groupBox1.TabIndex = 42;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Player";
            // 
            // EnergyStatus
            // 
            this.EnergyStatus.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.EnergyStatus.AutoSize = true;
            this.EnergyStatus.Location = new System.Drawing.Point(63, 66);
            this.EnergyStatus.Name = "EnergyStatus";
            this.EnergyStatus.Size = new System.Drawing.Size(15, 16);
            this.EnergyStatus.TabIndex = 45;
            this.EnergyStatus.Text = "0";
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 66);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(51, 16);
            this.label3.TabIndex = 44;
            this.label3.Text = "Energy";
            // 
            // MineralStatus
            // 
            this.MineralStatus.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.MineralStatus.AutoSize = true;
            this.MineralStatus.Location = new System.Drawing.Point(63, 43);
            this.MineralStatus.Name = "MineralStatus";
            this.MineralStatus.Size = new System.Drawing.Size(15, 16);
            this.MineralStatus.TabIndex = 43;
            this.MineralStatus.Text = "0";
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 43);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 16);
            this.label2.TabIndex = 42;
            this.label2.Text = "Minerals";
            // 
            // CreditsStatus
            // 
            this.CreditsStatus.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.CreditsStatus.AutoSize = true;
            this.CreditsStatus.Location = new System.Drawing.Point(63, 20);
            this.CreditsStatus.Name = "CreditsStatus";
            this.CreditsStatus.Size = new System.Drawing.Size(15, 16);
            this.CreditsStatus.TabIndex = 41;
            this.CreditsStatus.Text = "0";
            // 
            // Credits
            // 
            this.Credits.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.Credits.AutoSize = true;
            this.Credits.Location = new System.Drawing.Point(6, 20);
            this.Credits.Name = "Credits";
            this.Credits.Size = new System.Drawing.Size(50, 16);
            this.Credits.TabIndex = 40;
            this.Credits.Text = "Credits";
            // 
            // marketButton
            // 
            this.marketButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.marketButton.Font = new System.Drawing.Font("Impact", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.marketButton.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.marketButton.Location = new System.Drawing.Point(70, 339);
            this.marketButton.Name = "marketButton";
            this.marketButton.Size = new System.Drawing.Size(86, 80);
            this.marketButton.TabIndex = 27;
            this.marketButton.Text = "Market";
            this.marketButton.UseVisualStyleBackColor = false;
            this.marketButton.Click += new System.EventHandler(this.Shop_button_Click);
            // 
            // dateLabel
            // 
            this.dateLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.dateLabel.AutoSize = true;
            this.dateLabel.BackColor = System.Drawing.Color.Black;
            this.dateLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.dateLabel.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.dateLabel.Location = new System.Drawing.Point(841, 30);
            this.dateLabel.Name = "dateLabel";
            this.dateLabel.Size = new System.Drawing.Size(87, 31);
            this.dateLabel.TabIndex = 26;
            this.dateLabel.Text = "DATE";
            // 
            // fleetsButton
            // 
            this.fleetsButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.fleetsButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.fleetsButton.Enabled = false;
            this.fleetsButton.Font = new System.Drawing.Font("Impact", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.fleetsButton.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.fleetsButton.Location = new System.Drawing.Point(195, 369);
            this.fleetsButton.Name = "fleetsButton";
            this.fleetsButton.Size = new System.Drawing.Size(86, 80);
            this.fleetsButton.TabIndex = 28;
            this.fleetsButton.Tag = "fleet";
            this.fleetsButton.Text = "Fleets";
            this.fleetsButton.UseVisualStyleBackColor = false;
            this.fleetsButton.Click += new System.EventHandler(this.fleetsButton_Click);
            // 
            // listView
            // 
            this.listView.Activation = System.Windows.Forms.ItemActivation.OneClick;
            this.listView.Alignment = System.Windows.Forms.ListViewAlignment.Default;
            this.listView.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.listView.AutoArrange = false;
            this.listView.BackColor = System.Drawing.Color.Black;
            this.listView.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.listView.Cursor = System.Windows.Forms.Cursors.Default;
            this.listView.Font = new System.Drawing.Font("Impact", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.listView.ForeColor = System.Drawing.Color.White;
            this.listView.Location = new System.Drawing.Point(195, 113);
            this.listView.MultiSelect = false;
            this.listView.Name = "listView";
            this.listView.ShowItemToolTips = true;
            this.listView.Size = new System.Drawing.Size(178, 250);
            this.listView.TabIndex = 29;
            this.listView.Tag = "-";
            this.listView.TileSize = new System.Drawing.Size(175, 50);
            this.listView.UseCompatibleStateImageBehavior = false;
            this.listView.View = System.Windows.Forms.View.Tile;
            this.listView.ItemMouseHover += new System.Windows.Forms.ListViewItemMouseHoverEventHandler(this.listView_ItemMouseHover);
            this.listView.SelectedIndexChanged += new System.EventHandler(this.listView_SelectedIndexChanged);
            this.listView.MouseClick += new System.Windows.Forms.MouseEventHandler(this.listView_MouseClick);
            // 
            // systemsButton
            // 
            this.systemsButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.systemsButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.systemsButton.Enabled = false;
            this.systemsButton.Font = new System.Drawing.Font("Impact", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.systemsButton.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.systemsButton.Location = new System.Drawing.Point(287, 369);
            this.systemsButton.Name = "systemsButton";
            this.systemsButton.Size = new System.Drawing.Size(86, 80);
            this.systemsButton.TabIndex = 30;
            this.systemsButton.Tag = "system";
            this.systemsButton.Text = "Systems";
            this.systemsButton.UseVisualStyleBackColor = false;
            this.systemsButton.Click += new System.EventHandler(this.planetsButton_Click);
            // 
            // galaxyNameLablel
            // 
            this.galaxyNameLablel.AutoSize = true;
            this.galaxyNameLablel.BackColor = System.Drawing.Color.Black;
            this.galaxyNameLablel.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.galaxyNameLablel.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.galaxyNameLablel.Location = new System.Drawing.Point(174, 27);
            this.galaxyNameLablel.Name = "galaxyNameLablel";
            this.galaxyNameLablel.Size = new System.Drawing.Size(207, 31);
            this.galaxyNameLablel.TabIndex = 31;
            this.galaxyNameLablel.Text = "GALAXY NAME";
            // 
            // StepWorker
            // 
            this.StepWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.StepWorker_DoWork);
            this.StepWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.StepWorker_RunWorkerCompleted);
            // 
            // TechWorker
            // 
            this.TechWorker.WorkerReportsProgress = true;
            this.TechWorker.WorkerSupportsCancellation = true;
            // 
            // GameTimer
            // 
            this.GameTimer.Interval = 10;
            this.GameTimer.Tick += new System.EventHandler(this.GameTimer_Tick);
            // 
            // galaxyImage
            // 
            this.galaxyImage.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.galaxyImage.BackColor = System.Drawing.Color.Black;
            this.galaxyImage.Location = new System.Drawing.Point(174, 27);
            this.galaxyImage.Name = "galaxyImage";
            this.galaxyImage.Size = new System.Drawing.Size(834, 422);
            this.galaxyImage.TabIndex = 1;
            this.galaxyImage.TabStop = false;
            this.galaxyImage.Paint += new System.Windows.Forms.PaintEventHandler(this.galaxyImage_Paint);
            this.galaxyImage.MouseClick += new System.Windows.Forms.MouseEventHandler(this.galaxyImage_MouseClick);
            this.galaxyImage.MouseDown += new System.Windows.Forms.MouseEventHandler(this.galaxyImage_MouseDown);
            this.galaxyImage.MouseMove += new System.Windows.Forms.MouseEventHandler(this.galaxyImage_MouseMove);
            // 
            // toolTip1
            // 
            this.toolTip1.AutoPopDelay = 5000;
            this.toolTip1.InitialDelay = 400;
            this.toolTip1.IsBalloon = true;
            this.toolTip1.ReshowDelay = 400;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1008, 474);
            this.Controls.Add(this.galaxyNameLablel);
            this.Controls.Add(this.systemsButton);
            this.Controls.Add(this.listView);
            this.Controls.Add(this.fleetsButton);
            this.Controls.Add(this.dateLabel);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.tech_label);
            this.Controls.Add(this.tech_progressBar);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.galaxyImage);
            this.Controls.Add(this.mainMenu);
            this.MainMenuStrip = this.mainMenu;
            this.Name = "Form1";
            this.Text = "GalaxyConquest";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.mainMenu.ResumeLayout(false);
            this.mainMenu.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
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
        private System.Windows.Forms.ToolStripMenuItem mainMenuOpen;
        private System.Windows.Forms.ToolStripMenuItem mainMenuSave;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem mainMenuText;
        private System.Windows.Forms.ToolStripMenuItem mainMenuAbout;
        private System.Windows.Forms.ToolStripMenuItem MainMenuTechTree;
        private System.Windows.Forms.ToolStripMenuItem dModelsToolStripMenuItem;
        private System.Windows.Forms.Button step_button;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.ProgressBar conquer_progressBar;
        private System.Windows.Forms.Button button3;
        public System.Windows.Forms.ProgressBar tech_progressBar;
        public System.Windows.Forms.Label tech_label;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusSelectFleet;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusXY;
        private System.Windows.Forms.Button sound_button;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label Credits;
        private System.Windows.Forms.Label CreditsStatus;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label dateLabel;
        private System.Windows.Forms.Button marketButton;
        private System.Windows.Forms.Button fleetsButton;
        private System.Windows.Forms.Button systemsButton;
        private System.Windows.Forms.ListView listView;
        private System.Windows.Forms.Label galaxyNameLablel;
        private System.ComponentModel.BackgroundWorker StepWorker;
        private System.ComponentModel.BackgroundWorker TechWorker;
        private System.Windows.Forms.Timer GameTimer;
        private System.Windows.Forms.Label EnergyStatus;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label MineralStatus;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}

