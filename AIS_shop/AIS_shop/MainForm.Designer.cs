namespace AIS_shop
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.файлToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.оПрограммеToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.выходToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.личныйКабинетToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.перейтиВЛичныйКабинетToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.выйтиИзУчетнойЗаписиToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.войтиToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.зарегистрироватьсяToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.товарыToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.корзинаToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.файлToolStripMenuItem,
            this.личныйКабинетToolStripMenuItem,
            this.товарыToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(4, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(984, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            this.menuStrip1.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.menuStrip1_ItemClicked);
            // 
            // файлToolStripMenuItem
            // 
            this.файлToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.оПрограммеToolStripMenuItem,
            this.выходToolStripMenuItem});
            this.файлToolStripMenuItem.Name = "файлToolStripMenuItem";
            this.файлToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
            this.файлToolStripMenuItem.Text = "Файл";
            // 
            // оПрограммеToolStripMenuItem
            // 
            this.оПрограммеToolStripMenuItem.Name = "оПрограммеToolStripMenuItem";
            this.оПрограммеToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.оПрограммеToolStripMenuItem.Text = "О программе";
            this.оПрограммеToolStripMenuItem.Click += new System.EventHandler(this.оПрограммеToolStripMenuItem_Click);
            // 
            // выходToolStripMenuItem
            // 
            this.выходToolStripMenuItem.Name = "выходToolStripMenuItem";
            this.выходToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.выходToolStripMenuItem.Text = "Выход";
            this.выходToolStripMenuItem.Click += new System.EventHandler(this.выходToolStripMenuItem_Click);
            // 
            // личныйКабинетToolStripMenuItem
            // 
            this.личныйКабинетToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.перейтиВЛичныйКабинетToolStripMenuItem,
            this.выйтиИзУчетнойЗаписиToolStripMenuItem,
            this.войтиToolStripMenuItem,
            this.зарегистрироватьсяToolStripMenuItem});
            this.личныйКабинетToolStripMenuItem.Name = "личныйКабинетToolStripMenuItem";
            this.личныйКабинетToolStripMenuItem.Size = new System.Drawing.Size(111, 20);
            this.личныйКабинетToolStripMenuItem.Text = "Личный кабинет";
            this.личныйКабинетToolStripMenuItem.Click += new System.EventHandler(this.личныйКабинетToolStripMenuItem_Click);
            // 
            // перейтиВЛичныйКабинетToolStripMenuItem
            // 
            this.перейтиВЛичныйКабинетToolStripMenuItem.Name = "перейтиВЛичныйКабинетToolStripMenuItem";
            this.перейтиВЛичныйКабинетToolStripMenuItem.Size = new System.Drawing.Size(224, 22);
            this.перейтиВЛичныйКабинетToolStripMenuItem.Text = "Перейти в личный кабинет";
            this.перейтиВЛичныйКабинетToolStripMenuItem.Click += new System.EventHandler(this.перейтиВЛичныйКабинетToolStripMenuItem_Click);
            // 
            // выйтиИзУчетнойЗаписиToolStripMenuItem
            // 
            this.выйтиИзУчетнойЗаписиToolStripMenuItem.Name = "выйтиИзУчетнойЗаписиToolStripMenuItem";
            this.выйтиИзУчетнойЗаписиToolStripMenuItem.Size = new System.Drawing.Size(224, 22);
            this.выйтиИзУчетнойЗаписиToolStripMenuItem.Text = "Выйти из учетной записи";
            this.выйтиИзУчетнойЗаписиToolStripMenuItem.Click += new System.EventHandler(this.выйтиИзУчетнойЗаписиToolStripMenuItem_Click);
            // 
            // войтиToolStripMenuItem
            // 
            this.войтиToolStripMenuItem.Name = "войтиToolStripMenuItem";
            this.войтиToolStripMenuItem.Size = new System.Drawing.Size(224, 22);
            this.войтиToolStripMenuItem.Text = "Вход";
            this.войтиToolStripMenuItem.Click += new System.EventHandler(this.войтиToolStripMenuItem_Click);
            // 
            // зарегистрироватьсяToolStripMenuItem
            // 
            this.зарегистрироватьсяToolStripMenuItem.Name = "зарегистрироватьсяToolStripMenuItem";
            this.зарегистрироватьсяToolStripMenuItem.Size = new System.Drawing.Size(224, 22);
            this.зарегистрироватьсяToolStripMenuItem.Text = "Регистрация";
            this.зарегистрироватьсяToolStripMenuItem.Click += new System.EventHandler(this.зарегистрироватьсяToolStripMenuItem_Click);
            // 
            // товарыToolStripMenuItem
            // 
            this.товарыToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.корзинаToolStripMenuItem});
            this.товарыToolStripMenuItem.Name = "товарыToolStripMenuItem";
            this.товарыToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.товарыToolStripMenuItem.Text = "Товары";
            // 
            // корзинаToolStripMenuItem
            // 
            this.корзинаToolStripMenuItem.Name = "корзинаToolStripMenuItem";
            this.корзинаToolStripMenuItem.Size = new System.Drawing.Size(120, 22);
            this.корзинаToolStripMenuItem.Text = "Корзина";
            this.корзинаToolStripMenuItem.Click += new System.EventHandler(this.корзинаToolStripMenuItem_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.dataGridView1.Location = new System.Drawing.Point(0, 132);
            this.dataGridView1.Margin = new System.Windows.Forms.Padding(2);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 24;
            this.dataGridView1.Size = new System.Drawing.Size(984, 529);
            this.dataGridView1.TabIndex = 0;
            // 
            // comboBox1
            // 
            this.comboBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "Computers"});
            this.comboBox1.Location = new System.Drawing.Point(239, 93);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(121, 21);
            this.comboBox1.Sorted = true;
            this.comboBox1.TabIndex = 7;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(17, 95);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(216, 16);
            this.label2.TabIndex = 8;
            this.label2.Text = "Выберите категорию из списка:";
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Location = new System.Drawing.Point(875, 83);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(97, 39);
            this.button1.TabIndex = 9;
            this.button1.Text = "Фильтры";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.label1.Font = new System.Drawing.Font("Cambria", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Italic | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(811, 9);
            this.label1.Name = "label1";
            this.label1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.label1.Size = new System.Drawing.Size(25, 15);
            this.label1.TabIndex = 10;
            this.label1.Text = "zzz";
            this.label1.Visible = false;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(984, 661);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MinimumSize = new System.Drawing.Size(533, 608);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "DETShop - Магазин цифровой электронной техники";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem файлToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem оПрограммеToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem выходToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem личныйКабинетToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem перейтиВЛичныйКабинетToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem выйтиИзУчетнойЗаписиToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem товарыToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem корзинаToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem войтиToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem зарегистрироватьсяToolStripMenuItem;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
    }
}