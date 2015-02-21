namespace GalaxyConquest
{
    partial class StarShop
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StarShop));
            this.shipListBox = new System.Windows.Forms.ListBox();
            this.shipLargeImage = new System.Windows.Forms.PictureBox();
            this.assaultPicture = new System.Windows.Forms.PictureBox();
            this.scoutPicture = new System.Windows.Forms.PictureBox();
            this.colonysatorPicture = new System.Windows.Forms.PictureBox();
            this.fregatPicture = new System.Windows.Forms.PictureBox();
            this.descriptionText = new System.Windows.Forms.RichTextBox();
            this.removeButton = new System.Windows.Forms.Button();
            this.addButton = new System.Windows.Forms.Button();
            this.buyButton = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.priceLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.shipLargeImage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.assaultPicture)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.scoutPicture)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.colonysatorPicture)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.fregatPicture)).BeginInit();
            this.SuspendLayout();
            // 
            // shipListBox
            // 
            this.shipListBox.FormattingEnabled = true;
            this.shipListBox.Location = new System.Drawing.Point(12, 12);
            this.shipListBox.Name = "shipListBox";
            this.shipListBox.Size = new System.Drawing.Size(153, 186);
            this.shipListBox.TabIndex = 0;
            // 
            // shipLargeImage
            // 
            this.shipLargeImage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.shipLargeImage.Location = new System.Drawing.Point(171, 48);
            this.shipLargeImage.Name = "shipLargeImage";
            this.shipLargeImage.Size = new System.Drawing.Size(150, 150);
            this.shipLargeImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.shipLargeImage.TabIndex = 1;
            this.shipLargeImage.TabStop = false;
            // 
            // assaultPicture
            // 
            this.assaultPicture.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.assaultPicture.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.assaultPicture.Image = ((System.Drawing.Image)(resources.GetObject("assaultPicture.Image")));
            this.assaultPicture.Location = new System.Drawing.Point(171, 10);
            this.assaultPicture.Name = "assaultPicture";
            this.assaultPicture.Size = new System.Drawing.Size(32, 32);
            this.assaultPicture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.assaultPicture.TabIndex = 2;
            this.assaultPicture.TabStop = false;
            this.assaultPicture.Click += new System.EventHandler(this.pictureBox_Click);
            // 
            // scoutPicture
            // 
            this.scoutPicture.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.scoutPicture.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.scoutPicture.Image = ((System.Drawing.Image)(resources.GetObject("scoutPicture.Image")));
            this.scoutPicture.Location = new System.Drawing.Point(209, 10);
            this.scoutPicture.Name = "scoutPicture";
            this.scoutPicture.Size = new System.Drawing.Size(32, 32);
            this.scoutPicture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.scoutPicture.TabIndex = 3;
            this.scoutPicture.TabStop = false;
            this.scoutPicture.Click += new System.EventHandler(this.pictureBox_Click);
            // 
            // colonysatorPicture
            // 
            this.colonysatorPicture.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.colonysatorPicture.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.colonysatorPicture.Image = ((System.Drawing.Image)(resources.GetObject("colonysatorPicture.Image")));
            this.colonysatorPicture.Location = new System.Drawing.Point(251, 10);
            this.colonysatorPicture.Name = "colonysatorPicture";
            this.colonysatorPicture.Size = new System.Drawing.Size(32, 32);
            this.colonysatorPicture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.colonysatorPicture.TabIndex = 4;
            this.colonysatorPicture.TabStop = false;
            this.colonysatorPicture.Click += new System.EventHandler(this.pictureBox_Click);
            // 
            // fregatPicture
            // 
            this.fregatPicture.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.fregatPicture.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.fregatPicture.Image = ((System.Drawing.Image)(resources.GetObject("fregatPicture.Image")));
            this.fregatPicture.Location = new System.Drawing.Point(289, 10);
            this.fregatPicture.Name = "fregatPicture";
            this.fregatPicture.Size = new System.Drawing.Size(32, 32);
            this.fregatPicture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.fregatPicture.TabIndex = 5;
            this.fregatPicture.TabStop = false;
            this.fregatPicture.Click += new System.EventHandler(this.pictureBox_Click);
            // 
            // descriptionText
            // 
            this.descriptionText.BackColor = System.Drawing.SystemColors.Control;
            this.descriptionText.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.descriptionText.Location = new System.Drawing.Point(327, 48);
            this.descriptionText.Name = "descriptionText";
            this.descriptionText.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.descriptionText.Size = new System.Drawing.Size(150, 150);
            this.descriptionText.TabIndex = 6;
            this.descriptionText.Text = "Description";
            // 
            // removeButton
            // 
            this.removeButton.Location = new System.Drawing.Point(37, 204);
            this.removeButton.Name = "removeButton";
            this.removeButton.Size = new System.Drawing.Size(112, 24);
            this.removeButton.TabIndex = 7;
            this.removeButton.Text = "Убрать из списка";
            this.removeButton.UseVisualStyleBackColor = true;
            this.removeButton.Click += new System.EventHandler(this.removeButton_Click);
            // 
            // addButton
            // 
            this.addButton.Location = new System.Drawing.Point(189, 204);
            this.addButton.Name = "addButton";
            this.addButton.Size = new System.Drawing.Size(116, 24);
            this.addButton.TabIndex = 8;
            this.addButton.Text = "Добавить в список";
            this.addButton.UseVisualStyleBackColor = true;
            this.addButton.Click += new System.EventHandler(this.addButton_Click);
            // 
            // buyButton
            // 
            this.buyButton.Font = new System.Drawing.Font("Impact", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buyButton.Location = new System.Drawing.Point(153, 245);
            this.buyButton.Name = "buyButton";
            this.buyButton.Size = new System.Drawing.Size(113, 34);
            this.buyButton.TabIndex = 9;
            this.buyButton.Text = "Купить";
            this.buyButton.UseVisualStyleBackColor = true;
            this.buyButton.Click += new System.EventHandler(this.buyButton_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 256);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 13);
            this.label1.TabIndex = 10;
            this.label1.Text = "Total price:";
            // 
            // priceLabel
            // 
            this.priceLabel.AutoSize = true;
            this.priceLabel.ForeColor = System.Drawing.Color.Green;
            this.priceLabel.Location = new System.Drawing.Point(75, 256);
            this.priceLabel.Name = "priceLabel";
            this.priceLabel.Size = new System.Drawing.Size(22, 13);
            this.priceLabel.TabIndex = 11;
            this.priceLabel.Text = "0 $";
            // 
            // StarShop
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(486, 284);
            this.Controls.Add(this.priceLabel);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buyButton);
            this.Controls.Add(this.addButton);
            this.Controls.Add(this.removeButton);
            this.Controls.Add(this.descriptionText);
            this.Controls.Add(this.fregatPicture);
            this.Controls.Add(this.colonysatorPicture);
            this.Controls.Add(this.scoutPicture);
            this.Controls.Add(this.assaultPicture);
            this.Controls.Add(this.shipLargeImage);
            this.Controls.Add(this.shipListBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "StarShop";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Star Shop";
            this.TopMost = true;
            ((System.ComponentModel.ISupportInitialize)(this.shipLargeImage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.assaultPicture)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.scoutPicture)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.colonysatorPicture)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.fregatPicture)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox shipListBox;
        private System.Windows.Forms.PictureBox shipLargeImage;
        private System.Windows.Forms.PictureBox assaultPicture;
        private System.Windows.Forms.PictureBox scoutPicture;
        private System.Windows.Forms.PictureBox colonysatorPicture;
        private System.Windows.Forms.PictureBox fregatPicture;
        private System.Windows.Forms.RichTextBox descriptionText;
        private System.Windows.Forms.Button removeButton;
        private System.Windows.Forms.Button addButton;
        private System.Windows.Forms.Button buyButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label priceLabel;
    }
}