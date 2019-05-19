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
            resources.ApplyResources(this.textBoxNick, "textBoxNick");
            this.textBoxNick.Name = "textBoxNick";
            this.textBoxNick.ReadOnly = true;
            // 
            // textBoxEmail
            // 
            resources.ApplyResources(this.textBoxEmail, "textBoxEmail");
            this.textBoxEmail.Name = "textBoxEmail";
            this.textBoxEmail.ReadOnly = true;
            // 
            // textBoxPatronymic
            // 
            resources.ApplyResources(this.textBoxPatronymic, "textBoxPatronymic");
            this.textBoxPatronymic.Name = "textBoxPatronymic";
            this.textBoxPatronymic.ReadOnly = true;
            // 
            // textBoxName
            // 
            resources.ApplyResources(this.textBoxName, "textBoxName");
            this.textBoxName.Name = "textBoxName";
            this.textBoxName.ReadOnly = true;
            // 
            // textBoxSurname
            // 
            resources.ApplyResources(this.textBoxSurname, "textBoxSurname");
            this.textBoxSurname.Name = "textBoxSurname";
            this.textBoxSurname.ReadOnly = true;
            // 
            // linkNewImage
            // 
            resources.ApplyResources(this.linkNewImage, "linkNewImage");
            this.linkNewImage.Cursor = System.Windows.Forms.Cursors.Hand;
            this.linkNewImage.Name = "linkNewImage";
            this.linkNewImage.TabStop = true;
            this.linkNewImage.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkNewImage_LinkClicked);
            // 
            // labelNick
            // 
            resources.ApplyResources(this.labelNick, "labelNick");
            this.labelNick.Name = "labelNick";
            // 
            // labelPatronymic
            // 
            resources.ApplyResources(this.labelPatronymic, "labelPatronymic");
            this.labelPatronymic.Name = "labelPatronymic";
            // 
            // labelEmail
            // 
            resources.ApplyResources(this.labelEmail, "labelEmail");
            this.labelEmail.Name = "labelEmail";
            // 
            // labelName
            // 
            resources.ApplyResources(this.labelName, "labelName");
            this.labelName.Name = "labelName";
            // 
            // labelSurname
            // 
            resources.ApplyResources(this.labelSurname, "labelSurname");
            this.labelSurname.Name = "labelSurname";
            // 
            // pictureBox
            // 
            this.pictureBox.InitialImage = global::AIS_shop.Properties.Resources.nofoto;
            resources.ApplyResources(this.pictureBox, "pictureBox");
            this.pictureBox.Name = "pictureBox";
            this.pictureBox.TabStop = false;
            // 
            // buttonDelImage
            // 
            resources.ApplyResources(this.buttonDelImage, "buttonDelImage");
            this.buttonDelImage.Cursor = System.Windows.Forms.Cursors.Hand;
            this.buttonDelImage.Name = "buttonDelImage";
            this.buttonDelImage.UseVisualStyleBackColor = true;
            this.buttonDelImage.Click += new System.EventHandler(this.buttonDelImage_Click);
            // 
            // AdminProfile
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Info;
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
            this.MaximizeBox = false;
            this.Name = "AdminProfile";
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