namespace Test_DB
{
    partial class Form2
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form2));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.файлToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.печатьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.сменитьПользователяToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.выходToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.правкаToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.сортироватьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.поТипуАвтомобиляToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.поСтоимостиToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.поПробегуToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.поТипуДвигателяToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.поСостояниюToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.отображатьПроданныеАвтомобилиToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.разместитьОбъявлениеToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.listView1 = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader7 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader8 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader9 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.подробнееToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.редактироватьToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label1 = new System.Windows.Forms.Label();
            this.printDocument1 = new System.Drawing.Printing.PrintDocument();
            this.printDialog1 = new System.Windows.Forms.PrintDialog();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton3 = new System.Windows.Forms.ToolStripButton();
            this.menuStrip1.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.файлToolStripMenuItem,
            this.правкаToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(894, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // файлToolStripMenuItem
            // 
            this.файлToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.печатьToolStripMenuItem,
            this.сменитьПользователяToolStripMenuItem,
            this.выходToolStripMenuItem});
            this.файлToolStripMenuItem.Name = "файлToolStripMenuItem";
            this.файлToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
            this.файлToolStripMenuItem.Text = "Файл";
            // 
            // печатьToolStripMenuItem
            // 
            this.печатьToolStripMenuItem.Name = "печатьToolStripMenuItem";
            this.печатьToolStripMenuItem.Size = new System.Drawing.Size(200, 22);
            this.печатьToolStripMenuItem.Text = "Печать";
            this.печатьToolStripMenuItem.Click += new System.EventHandler(this.печатьToolStripMenuItem_Click);
            // 
            // сменитьПользователяToolStripMenuItem
            // 
            this.сменитьПользователяToolStripMenuItem.Name = "сменитьПользователяToolStripMenuItem";
            this.сменитьПользователяToolStripMenuItem.Size = new System.Drawing.Size(200, 22);
            this.сменитьПользователяToolStripMenuItem.Text = "Сменить пользователя";
            this.сменитьПользователяToolStripMenuItem.Click += new System.EventHandler(this.сменитьПользователяToolStripMenuItem_Click);
            // 
            // выходToolStripMenuItem
            // 
            this.выходToolStripMenuItem.Name = "выходToolStripMenuItem";
            this.выходToolStripMenuItem.Size = new System.Drawing.Size(200, 22);
            this.выходToolStripMenuItem.Text = "Выход";
            this.выходToolStripMenuItem.Click += new System.EventHandler(this.выходToolStripMenuItem_Click);
            // 
            // правкаToolStripMenuItem
            // 
            this.правкаToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.сортироватьToolStripMenuItem,
            this.отображатьПроданныеАвтомобилиToolStripMenuItem,
            this.разместитьОбъявлениеToolStripMenuItem});
            this.правкаToolStripMenuItem.Name = "правкаToolStripMenuItem";
            this.правкаToolStripMenuItem.Size = new System.Drawing.Size(59, 20);
            this.правкаToolStripMenuItem.Text = "Правка";
            // 
            // сортироватьToolStripMenuItem
            // 
            this.сортироватьToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.поТипуАвтомобиляToolStripMenuItem,
            this.поСтоимостиToolStripMenuItem,
            this.поПробегуToolStripMenuItem,
            this.поТипуДвигателяToolStripMenuItem,
            this.поСостояниюToolStripMenuItem});
            this.сортироватьToolStripMenuItem.Name = "сортироватьToolStripMenuItem";
            this.сортироватьToolStripMenuItem.Size = new System.Drawing.Size(277, 22);
            this.сортироватьToolStripMenuItem.Text = "Сортировать";
            // 
            // поТипуАвтомобиляToolStripMenuItem
            // 
            this.поТипуАвтомобиляToolStripMenuItem.CheckOnClick = true;
            this.поТипуАвтомобиляToolStripMenuItem.Name = "поТипуАвтомобиляToolStripMenuItem";
            this.поТипуАвтомобиляToolStripMenuItem.Size = new System.Drawing.Size(175, 22);
            this.поТипуАвтомобиляToolStripMenuItem.Text = "По году выпуска";
            this.поТипуАвтомобиляToolStripMenuItem.Click += new System.EventHandler(this.поТипуАвтомобиляToolStripMenuItem_Click);
            // 
            // поСтоимостиToolStripMenuItem
            // 
            this.поСтоимостиToolStripMenuItem.Checked = true;
            this.поСтоимостиToolStripMenuItem.CheckOnClick = true;
            this.поСтоимостиToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.поСтоимостиToolStripMenuItem.Name = "поСтоимостиToolStripMenuItem";
            this.поСтоимостиToolStripMenuItem.Size = new System.Drawing.Size(175, 22);
            this.поСтоимостиToolStripMenuItem.Text = "По стоимости";
            this.поСтоимостиToolStripMenuItem.Click += new System.EventHandler(this.поСтоимостиToolStripMenuItem_Click);
            // 
            // поПробегуToolStripMenuItem
            // 
            this.поПробегуToolStripMenuItem.CheckOnClick = true;
            this.поПробегуToolStripMenuItem.Name = "поПробегуToolStripMenuItem";
            this.поПробегуToolStripMenuItem.Size = new System.Drawing.Size(175, 22);
            this.поПробегуToolStripMenuItem.Text = "По пробегу";
            this.поПробегуToolStripMenuItem.Click += new System.EventHandler(this.поПробегуToolStripMenuItem_Click);
            // 
            // поТипуДвигателяToolStripMenuItem
            // 
            this.поТипуДвигателяToolStripMenuItem.CheckOnClick = true;
            this.поТипуДвигателяToolStripMenuItem.Name = "поТипуДвигателяToolStripMenuItem";
            this.поТипуДвигателяToolStripMenuItem.Size = new System.Drawing.Size(175, 22);
            this.поТипуДвигателяToolStripMenuItem.Text = "По типу двигателя";
            this.поТипуДвигателяToolStripMenuItem.Click += new System.EventHandler(this.поТипуДвигателяToolStripMenuItem_Click);
            // 
            // поСостояниюToolStripMenuItem
            // 
            this.поСостояниюToolStripMenuItem.CheckOnClick = true;
            this.поСостояниюToolStripMenuItem.Name = "поСостояниюToolStripMenuItem";
            this.поСостояниюToolStripMenuItem.Size = new System.Drawing.Size(175, 22);
            this.поСостояниюToolStripMenuItem.Text = "По названию";
            this.поСостояниюToolStripMenuItem.Click += new System.EventHandler(this.поСостояниюToolStripMenuItem_Click);
            // 
            // отображатьПроданныеАвтомобилиToolStripMenuItem
            // 
            this.отображатьПроданныеАвтомобилиToolStripMenuItem.Checked = true;
            this.отображатьПроданныеАвтомобилиToolStripMenuItem.CheckOnClick = true;
            this.отображатьПроданныеАвтомобилиToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.отображатьПроданныеАвтомобилиToolStripMenuItem.Name = "отображатьПроданныеАвтомобилиToolStripMenuItem";
            this.отображатьПроданныеАвтомобилиToolStripMenuItem.Size = new System.Drawing.Size(277, 22);
            this.отображатьПроданныеАвтомобилиToolStripMenuItem.Text = "Отображать проданные автомобили";
            this.отображатьПроданныеАвтомобилиToolStripMenuItem.Click += new System.EventHandler(this.отображатьПроданныеАвтомобилиToolStripMenuItem_Click);
            // 
            // разместитьОбъявлениеToolStripMenuItem
            // 
            this.разместитьОбъявлениеToolStripMenuItem.Name = "разместитьОбъявлениеToolStripMenuItem";
            this.разместитьОбъявлениеToolStripMenuItem.Size = new System.Drawing.Size(277, 22);
            this.разместитьОбъявлениеToolStripMenuItem.Text = "Разместить объявление";
            this.разместитьОбъявлениеToolStripMenuItem.Click += new System.EventHandler(this.разместитьОбъявлениеToolStripMenuItem_Click);
            // 
            // listView1
            // 
            this.listView1.Activation = System.Windows.Forms.ItemActivation.OneClick;
            this.listView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader5,
            this.columnHeader6,
            this.columnHeader7,
            this.columnHeader8,
            this.columnHeader9});
            this.listView1.ContextMenuStrip = this.contextMenuStrip1;
            this.listView1.Cursor = System.Windows.Forms.Cursors.Default;
            this.listView1.GridLines = true;
            this.listView1.HoverSelection = true;
            this.listView1.LabelEdit = true;
            this.listView1.Location = new System.Drawing.Point(0, 52);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(894, 401);
            this.listView1.TabIndex = 1;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Название";
            this.columnHeader1.Width = 208;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Коробка передач";
            this.columnHeader2.Width = 103;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Пробег";
            this.columnHeader3.Width = 54;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Год выпуска";
            this.columnHeader4.Width = 83;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "Тип двигателя";
            this.columnHeader5.Width = 91;
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "Состояние";
            this.columnHeader6.Width = 70;
            // 
            // columnHeader7
            // 
            this.columnHeader7.Text = "Тип автомобиля";
            this.columnHeader7.Width = 97;
            // 
            // columnHeader8
            // 
            this.columnHeader8.Text = "Стоимость";
            this.columnHeader8.Width = 86;
            // 
            // columnHeader9
            // 
            this.columnHeader9.Text = "Продан";
            this.columnHeader9.Width = 80;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.подробнееToolStripMenuItem,
            this.редактироватьToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(155, 48);
            // 
            // подробнееToolStripMenuItem
            // 
            this.подробнееToolStripMenuItem.Name = "подробнееToolStripMenuItem";
            this.подробнееToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.подробнееToolStripMenuItem.Text = "Подробнее";
            this.подробнееToolStripMenuItem.Click += new System.EventHandler(this.подробнееToolStripMenuItem_Click);
            // 
            // редактироватьToolStripMenuItem
            // 
            this.редактироватьToolStripMenuItem.Name = "редактироватьToolStripMenuItem";
            this.редактироватьToolStripMenuItem.Size = new System.Drawing.Size(154, 22);
            this.редактироватьToolStripMenuItem.Text = "Редактировать";
            this.редактироватьToolStripMenuItem.Click += new System.EventHandler(this.редактироватьToolStripMenuItem_Click);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.Location = new System.Drawing.Point(493, 1);
            this.label1.MaximumSize = new System.Drawing.Size(400, 23);
            this.label1.MinimumSize = new System.Drawing.Size(70, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(400, 23);
            this.label1.TabIndex = 3;
            this.label1.Text = "Войдите или загегистрируйтесь";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // printDocument1
            // 
            this.printDocument1.DocumentName = "Каталог автомобилей";
            this.printDocument1.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(this.printDocument1_PrintPage);
            // 
            // printDialog1
            // 
            this.printDialog1.UseEXDialog = true;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1,
            this.toolStripButton2,
            this.toolStripButton3});
            this.toolStrip1.Location = new System.Drawing.Point(0, 24);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(894, 25);
            this.toolStrip1.TabIndex = 4;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton1.Text = "toolStripButton1";
            this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // toolStripButton2
            // 
            this.toolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton2.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton2.Image")));
            this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton2.Name = "toolStripButton2";
            this.toolStripButton2.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton2.Text = "toolStripButton2";
            this.toolStripButton2.Click += new System.EventHandler(this.toolStripButton2_Click);
            // 
            // toolStripButton3
            // 
            this.toolStripButton3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton3.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton3.Image")));
            this.toolStripButton3.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton3.Name = "toolStripButton3";
            this.toolStripButton3.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton3.Text = "toolStripButton3";
            this.toolStripButton3.Click += new System.EventHandler(this.toolStripButton3_Click);
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(894, 455);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form2";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Торговая площадка Car Market";
            this.Load += new System.EventHandler(this.Form2_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.contextMenuStrip1.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem файлToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem печатьToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem выходToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem правкаToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem сортироватьToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem поТипуАвтомобиляToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem поСтоимостиToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem поПробегуToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem поТипуДвигателяToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem поСостояниюToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem отображатьПроданныеАвтомобилиToolStripMenuItem;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.ColumnHeader columnHeader7;
        private System.Windows.Forms.ColumnHeader columnHeader9;
        private System.Windows.Forms.ColumnHeader columnHeader8;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem подробнееToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem разместитьОбъявлениеToolStripMenuItem;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolStripMenuItem сменитьПользователяToolStripMenuItem;
        private System.Drawing.Printing.PrintDocument printDocument1;
        private System.Windows.Forms.PrintDialog printDialog1;
        private System.Windows.Forms.ToolStripMenuItem редактироватьToolStripMenuItem;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolStripButton1;
        private System.Windows.Forms.ToolStripButton toolStripButton2;
        private System.Windows.Forms.ToolStripButton toolStripButton3;
    }
}