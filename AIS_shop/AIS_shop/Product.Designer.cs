namespace AIS_shop
{
    partial class Product
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Product));
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabPageDescription = new System.Windows.Forms.TabPage();
            this.richTextBoxDescription = new System.Windows.Forms.RichTextBox();
            this.tabPageCharacteristic = new System.Windows.Forms.TabPage();
            this.listBoxChars = new System.Windows.Forms.ListBox();
            this.tabPageReviews = new System.Windows.Forms.TabPage();
            this.richTextBoxReviews = new System.Windows.Forms.RichTextBox();
            this.bAddToCart = new System.Windows.Forms.Button();
            this.labelCost = new System.Windows.Forms.Label();
            this.lableProductName = new System.Windows.Forms.Label();
            this.labelInStock = new System.Windows.Forms.Label();
            this.pictureBox = new System.Windows.Forms.PictureBox();
            this.buttonAddReview = new System.Windows.Forms.Button();
            this.tabControl.SuspendLayout();
            this.tabPageDescription.SuspendLayout();
            this.tabPageCharacteristic.SuspendLayout();
            this.tabPageReviews.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Location = new System.Drawing.Point(276, 46);
            this.linkLabel1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(0, 15);
            this.linkLabel1.TabIndex = 1;
            // 
            // tabControl
            // 
            this.tabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl.Controls.Add(this.tabPageDescription);
            this.tabControl.Controls.Add(this.tabPageCharacteristic);
            this.tabControl.Controls.Add(this.tabPageReviews);
            this.tabControl.Location = new System.Drawing.Point(0, 343);
            this.tabControl.Margin = new System.Windows.Forms.Padding(2);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(798, 216);
            this.tabControl.TabIndex = 3;
            // 
            // tabPageDescription
            // 
            this.tabPageDescription.Controls.Add(this.richTextBoxDescription);
            this.tabPageDescription.Location = new System.Drawing.Point(4, 24);
            this.tabPageDescription.Margin = new System.Windows.Forms.Padding(2);
            this.tabPageDescription.Name = "tabPageDescription";
            this.tabPageDescription.Padding = new System.Windows.Forms.Padding(2);
            this.tabPageDescription.Size = new System.Drawing.Size(790, 188);
            this.tabPageDescription.TabIndex = 0;
            this.tabPageDescription.Text = "Описание";
            this.tabPageDescription.UseVisualStyleBackColor = true;
            // 
            // richTextBoxDescription
            // 
            this.richTextBoxDescription.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBoxDescription.Font = new System.Drawing.Font("Cambria", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.richTextBoxDescription.Location = new System.Drawing.Point(2, 2);
            this.richTextBoxDescription.Name = "richTextBoxDescription";
            this.richTextBoxDescription.ReadOnly = true;
            this.richTextBoxDescription.Size = new System.Drawing.Size(786, 184);
            this.richTextBoxDescription.TabIndex = 0;
            this.richTextBoxDescription.Text = "";
            // 
            // tabPageCharacteristic
            // 
            this.tabPageCharacteristic.Controls.Add(this.listBoxChars);
            this.tabPageCharacteristic.Location = new System.Drawing.Point(4, 22);
            this.tabPageCharacteristic.Margin = new System.Windows.Forms.Padding(2);
            this.tabPageCharacteristic.Name = "tabPageCharacteristic";
            this.tabPageCharacteristic.Padding = new System.Windows.Forms.Padding(2);
            this.tabPageCharacteristic.Size = new System.Drawing.Size(1082, 394);
            this.tabPageCharacteristic.TabIndex = 1;
            this.tabPageCharacteristic.Text = "Характеристики";
            this.tabPageCharacteristic.UseVisualStyleBackColor = true;
            // 
            // listBoxChars
            // 
            this.listBoxChars.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBoxChars.Font = new System.Drawing.Font("Cambria", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.listBoxChars.FormattingEnabled = true;
            this.listBoxChars.ItemHeight = 19;
            this.listBoxChars.Location = new System.Drawing.Point(2, 2);
            this.listBoxChars.Name = "listBoxChars";
            this.listBoxChars.SelectionMode = System.Windows.Forms.SelectionMode.None;
            this.listBoxChars.Size = new System.Drawing.Size(1078, 390);
            this.listBoxChars.TabIndex = 0;
            // 
            // tabPageReviews
            // 
            this.tabPageReviews.Controls.Add(this.richTextBoxReviews);
            this.tabPageReviews.Location = new System.Drawing.Point(4, 22);
            this.tabPageReviews.Margin = new System.Windows.Forms.Padding(2);
            this.tabPageReviews.Name = "tabPageReviews";
            this.tabPageReviews.Size = new System.Drawing.Size(1082, 394);
            this.tabPageReviews.TabIndex = 2;
            this.tabPageReviews.Text = "Отзывы";
            this.tabPageReviews.UseVisualStyleBackColor = true;
            // 
            // richTextBoxReviews
            // 
            this.richTextBoxReviews.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBoxReviews.Font = new System.Drawing.Font("Cambria", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.richTextBoxReviews.Location = new System.Drawing.Point(0, 0);
            this.richTextBoxReviews.Name = "richTextBoxReviews";
            this.richTextBoxReviews.ReadOnly = true;
            this.richTextBoxReviews.Size = new System.Drawing.Size(1082, 394);
            this.richTextBoxReviews.TabIndex = 0;
            this.richTextBoxReviews.Text = "";
            // 
            // bAddToCart
            // 
            this.bAddToCart.Location = new System.Drawing.Point(371, 281);
            this.bAddToCart.Margin = new System.Windows.Forms.Padding(2);
            this.bAddToCart.Name = "bAddToCart";
            this.bAddToCart.Size = new System.Drawing.Size(154, 42);
            this.bAddToCart.TabIndex = 5;
            this.bAddToCart.Text = "Добавить в корзину";
            this.bAddToCart.UseVisualStyleBackColor = true;
            this.bAddToCart.Click += new System.EventHandler(this.buttonAddToCart_Click);
            // 
            // labelCost
            // 
            this.labelCost.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelCost.AutoSize = true;
            this.labelCost.Font = new System.Drawing.Font("Cambria", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelCost.Location = new System.Drawing.Point(365, 158);
            this.labelCost.Name = "labelCost";
            this.labelCost.Size = new System.Drawing.Size(66, 28);
            this.labelCost.TabIndex = 6;
            this.labelCost.Text = "Цена";
            // 
            // lableProductName
            // 
            this.lableProductName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lableProductName.AutoSize = true;
            this.lableProductName.Font = new System.Drawing.Font("Cambria", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lableProductName.Location = new System.Drawing.Point(364, 46);
            this.lableProductName.Name = "lableProductName";
            this.lableProductName.Size = new System.Drawing.Size(220, 32);
            this.lableProductName.TabIndex = 7;
            this.lableProductName.Text = "Название товара";
            // 
            // labelInStock
            // 
            this.labelInStock.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.labelInStock.AutoSize = true;
            this.labelInStock.Font = new System.Drawing.Font("Cambria", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.labelInStock.Location = new System.Drawing.Point(365, 211);
            this.labelInStock.Name = "labelInStock";
            this.labelInStock.Size = new System.Drawing.Size(122, 25);
            this.labelInStock.TabIndex = 8;
            this.labelInStock.Text = "В наличии: ";
            // 
            // pictureBox
            // 
            this.pictureBox.Location = new System.Drawing.Point(24, 23);
            this.pictureBox.Margin = new System.Windows.Forms.Padding(2);
            this.pictureBox.Name = "pictureBox";
            this.pictureBox.Size = new System.Drawing.Size(303, 300);
            this.pictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox.TabIndex = 0;
            this.pictureBox.TabStop = false;
            // 
            // buttonAddReview
            // 
            this.buttonAddReview.Location = new System.Drawing.Point(551, 281);
            this.buttonAddReview.Name = "buttonAddReview";
            this.buttonAddReview.Size = new System.Drawing.Size(154, 42);
            this.buttonAddReview.TabIndex = 9;
            this.buttonAddReview.Text = "Добавить отзыв";
            this.buttonAddReview.UseVisualStyleBackColor = true;
            this.buttonAddReview.Click += new System.EventHandler(this.buttonAddReview_Click);
            // 
            // Product
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(798, 559);
            this.Controls.Add(this.buttonAddReview);
            this.Controls.Add(this.labelInStock);
            this.Controls.Add(this.lableProductName);
            this.Controls.Add(this.labelCost);
            this.Controls.Add(this.bAddToCart);
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.linkLabel1);
            this.Controls.Add(this.pictureBox);
            this.Font = new System.Drawing.Font("Cambria", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MinimumSize = new System.Drawing.Size(814, 479);
            this.Name = "Product";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "DETShop - Информация о товаре";
            this.Load += new System.EventHandler(this.Product_Load);
            this.tabControl.ResumeLayout(false);
            this.tabPageDescription.ResumeLayout(false);
            this.tabPageCharacteristic.ResumeLayout(false);
            this.tabPageReviews.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabPageDescription;
        private System.Windows.Forms.TabPage tabPageCharacteristic;
        private System.Windows.Forms.Button bAddToCart;
        private System.Windows.Forms.TabPage tabPageReviews;
        private System.Windows.Forms.Label labelCost;
        private System.Windows.Forms.Label lableProductName;
        private System.Windows.Forms.Label labelInStock;
        private System.Windows.Forms.RichTextBox richTextBoxDescription;
        private System.Windows.Forms.ListBox listBoxChars;
        private System.Windows.Forms.RichTextBox richTextBoxReviews;
        private System.Windows.Forms.Button buttonAddReview;
    }
}