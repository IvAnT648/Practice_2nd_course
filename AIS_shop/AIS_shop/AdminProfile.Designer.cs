namespace AIS_shop
{
    partial class AdminProfile
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AdminProfile));
            this.textBoxNick = new System.Windows.Forms.TextBox();
            this.textBoxEmail = new System.Windows.Forms.TextBox();
            this.textBoxPatronymic = new System.Windows.Forms.TextBox();
            this.textBoxName = new System.Windows.Forms.TextBox();
            this.textBoxSurname = new System.Windows.Forms.TextBox();
            this.linkNewImage = new System.Windows.Forms.LinkLabel();
            this.labelNick = new System.Windows.Forms.Label();
            this.labelPatronymic = new System.Windows.Forms.Label();
            this.labelEmail = new System.Windows.Forms.Label();
            this.labelName = new System.Windows.Forms.Label();
            this.labelSurname = new System.Windows.Forms.Label();
            this.pictureBox = new System.Windows.Forms.PictureBox();
            this.openNewImage = new System.Windows.Forms.OpenFileDialog();
            this.buttonDelImage = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // textBoxNick
            // 
            this.textBoxNick.Font = new System.Drawing.Font("Cambria", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBoxNick.Location = new System.Drawing.Point(344, 230);
            this.textBoxNick.Name = "textBoxNick";
            this.textBoxNick.ReadOnly = true;
            this.textBoxNick.Size = new System.Drawing.Size(278, 30);
            this.textBoxNick.TabIndex = 41;
            // 
            // textBoxEmail
            // 
            this.textBoxEmail.Font = new System.Drawing.Font("Cambria", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBoxEmail.Location = new System.Drawing.Point(344, 177);
            this.textBoxEmail.Name = "textBoxEmail";
            this.textBoxEmail.ReadOnly = true;
            this.textBoxEmail.Size = new System.Drawing.Size(279, 30);
            this.textBoxEmail.TabIndex = 40;
            // 
            // textBoxPatronymic
            // 
            this.textBoxPatronymic.Font = new System.Drawing.Font("Cambria", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBoxPatronymic.Location = new System.Drawing.Point(345, 127);
            this.textBoxPatronymic.Name = "textBoxPatronymic";
            this.textBoxPatronymic.ReadOnly = true;
            this.textBoxPatronymic.Size = new System.Drawing.Size(278, 30);
            this.textBoxPatronymic.TabIndex = 39;
            // 
            // textBoxName
            // 
            this.textBoxName.Font = new System.Drawing.Font("Cambria", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBoxName.Location = new System.Drawing.Point(344, 77);
            this.textBoxName.Name = "textBoxName";
            this.textBoxName.ReadOnly = true;
            this.textBoxName.Size = new System.Drawing.Size(279, 30);
            this.textBoxName.TabIndex = 38;
            // 
            // textBoxSurname
            // 
            this.textBoxSurname.Font = new System.Drawing.Font("Cambria", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textBoxSurname.Location = new System.Drawing.Point(345, 24);
            this.textBoxSurname.Name = "textBoxSurname";
            this.textBoxSurname.ReadOnly = true;
            this.textBoxSurname.Size = new System.Drawing.Size(279, 30);
            this.textBoxSurname.TabIndex = 37;
            // 
            // linkNewImage
            // 
            this.linkNewImage.AutoSize = true;
            this.linkNewImage.Cursor = System.Windows.Forms.Cursors.Hand;
            this.linkNewImage.Font = new System.Drawing.Font("Cambria", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.linkNewImage.Location = new System.Drawing.Point(49, 277);
            this.linkNewImage.Name = "linkNewImage";
            this.linkNewImage.Size = new System.Drawing.Size(149, 19);
            this.linkNewImage.TabIndex = 36;
            this.linkNewImage.TabStop = true;
            this.linkNewImage.Text = "Новое изображение";
            this.linkNewImage.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkNewImage_LinkClicked);
            // 
            // labelNick
            // 
            this.labelNick.AutoSize = true;
            this.labelNick.Font = new System.Drawing.Font("Cambria", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelNick.Location = new System.Drawing.Point(246, 233);
            this.labelNick.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelNick.Name = "labelNick";
            this.labelNick.Size = new System.Drawing.Size(93, 22);
            this.labelNick.TabIndex = 35;
            this.labelNick.Text = "Никнейм:";
            // 
            // labelPatronymic
            // 
            this.labelPatronymic.AutoSize = true;
            this.labelPatronymic.Font = new System.Drawing.Font("Cambria", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelPatronymic.Location = new System.Drawing.Point(246, 130);
            this.labelPatronymic.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelPatronymic.Name = "labelPatronymic";
            this.labelPatronymic.Size = new System.Drawing.Size(94, 22);
            this.labelPatronymic.TabIndex = 34;
            this.labelPatronymic.Text = "Отчество:";
            // 
            // labelEmail
            // 
            this.labelEmail.AutoSize = true;
            this.labelEmail.Font = new System.Drawing.Font("Cambria", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelEmail.Location = new System.Drawing.Point(246, 180);
            this.labelEmail.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelEmail.Name = "labelEmail";
            this.labelEmail.Size = new System.Drawing.Size(67, 22);
            this.labelEmail.TabIndex = 33;
            this.labelEmail.Text = "E-mail:";
            // 
            // labelName
            // 
            this.labelName.AutoSize = true;
            this.labelName.Font = new System.Drawing.Font("Cambria", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelName.Location = new System.Drawing.Point(246, 80);
            this.labelName.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelName.Name = "labelName";
            this.labelName.Size = new System.Drawing.Size(51, 22);
            this.labelName.TabIndex = 32;
            this.labelName.Text = "Имя:";
            // 
            // labelSurname
            // 
            this.labelSurname.AutoSize = true;
            this.labelSurname.Font = new System.Drawing.Font("Cambria", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelSurname.Location = new System.Drawing.Point(246, 28);
            this.labelSurname.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelSurname.Name = "labelSurname";
            this.labelSurname.Size = new System.Drawing.Size(95, 22);
            this.labelSurname.TabIndex = 31;
            this.labelSurname.Text = "Фамилия:";
            // 
            // pictureBox
            // 
            this.pictureBox.InitialImage = global::AIS_shop.Properties.Resources.nofoto;
            this.pictureBox.Location = new System.Drawing.Point(23, 24);
            this.pictureBox.Margin = new System.Windows.Forms.Padding(2);
            this.pictureBox.Name = "pictureBox";
            this.pictureBox.Size = new System.Drawing.Size(194, 236);
            this.pictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox.TabIndex = 30;
            this.pictureBox.TabStop = false;
            // 
            // buttonDelImage
            // 
            this.buttonDelImage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonDelImage.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonDelImage.Image = ((System.Drawing.Image)(resources.GetObject("buttonDelImage.Image")));
            this.buttonDelImage.Location = new System.Drawing.Point(23, 278);
            this.buttonDelImage.Name = "buttonDelImage";
            this.buttonDelImage.Size = new System.Drawing.Size(18, 18);
            this.buttonDelImage.TabIndex = 42;
            this.buttonDelImage.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.buttonDelImage.UseVisualStyleBackColor = true;
            this.buttonDelImage.Click += new System.EventHandler(this.buttonDelImage_Click);
            // 
            // AdminProfile
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Info;
            this.ClientSize = new System.Drawing.Size(655, 334);
            this.Controls.Add(this.buttonDelImage);
            this.Controls.Add(this.textBoxNick);
            this.Controls.Add(this.textBoxEmail);
            this.Controls.Add(this.textBoxPatronymic);
            this.Controls.Add(this.textBoxName);
            this.Controls.Add(this.textBoxSurname);
            this.Controls.Add(this.linkNewImage);
            this.Controls.Add(this.labelNick);
            this.Controls.Add(this.labelPatronymic);
            this.Controls.Add(this.labelEmail);
            this.Controls.Add(this.labelName);
            this.Controls.Add(this.labelSurname);
            this.Controls.Add(this.pictureBox);
            this.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(671, 373);
            this.MinimumSize = new System.Drawing.Size(671, 373);
            this.Name = "AdminProfile";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "DETShop - Профиль администратора";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.AdminProfile_FormClosing);
            this.Load += new System.EventHandler(this.AdminProfile_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxNick;
        private System.Windows.Forms.TextBox textBoxEmail;
        private System.Windows.Forms.TextBox textBoxPatronymic;
        private System.Windows.Forms.TextBox textBoxName;
        private System.Windows.Forms.TextBox textBoxSurname;
        private System.Windows.Forms.LinkLabel linkNewImage;
        private System.Windows.Forms.Label labelNick;
        private System.Windows.Forms.Label labelPatronymic;
        private System.Windows.Forms.Label labelEmail;
        private System.Windows.Forms.Label labelName;
        private System.Windows.Forms.Label labelSurname;
        private System.Windows.Forms.PictureBox pictureBox;
        private System.Windows.Forms.OpenFileDialog openNewImage;
        private System.Windows.Forms.Button buttonDelImage;
    }
}