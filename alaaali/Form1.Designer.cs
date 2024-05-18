namespace alaaali
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.imageProcessingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sepiaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.hueModifierToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rotateChannelsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.invertToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.colorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.colorSelectedAreaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.xRayScannerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openSecondImageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.compareImagesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.categorizeSeverityToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.openFileDialog2 = new System.Windows.Forms.OpenFileDialog();
            this.searchImagesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { this.fileToolStripMenuItem, this.imageProcessingToolStripMenuItem, this.colorToolStripMenuItem, this.colorSelectedAreaToolStripMenuItem, this.xRayScannerToolStripMenuItem, this.openSecondImageToolStripMenuItem, this.compareImagesToolStripMenuItem, this.categorizeSeverityToolStripMenuItem, this.searchImagesToolStripMenuItem });
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1369, 28);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { this.openToolStripMenuItem, this.saveToolStripMenuItem, this.exitToolStripMenuItem });
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(44, 24);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            this.openToolStripMenuItem.Size = new System.Drawing.Size(112, 24);
            this.openToolStripMenuItem.Text = "open";
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click_1);
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            this.saveToolStripMenuItem.Size = new System.Drawing.Size(112, 24);
            this.saveToolStripMenuItem.Text = "save";
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(112, 24);
            this.exitToolStripMenuItem.Text = "exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click_1);
            // 
            // imageProcessingToolStripMenuItem
            // 
            this.imageProcessingToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { this.sepiaToolStripMenuItem, this.hueModifierToolStripMenuItem, this.rotateChannelsToolStripMenuItem, this.invertToolStripMenuItem });
            this.imageProcessingToolStripMenuItem.Name = "imageProcessingToolStripMenuItem";
            this.imageProcessingToolStripMenuItem.Size = new System.Drawing.Size(133, 24);
            this.imageProcessingToolStripMenuItem.Text = "ImageProcessing";
            // 
            // sepiaToolStripMenuItem
            // 
            this.sepiaToolStripMenuItem.Name = "sepiaToolStripMenuItem";
            this.sepiaToolStripMenuItem.Size = new System.Drawing.Size(181, 24);
            this.sepiaToolStripMenuItem.Text = "sepia";
            this.sepiaToolStripMenuItem.Click += new System.EventHandler(this.sepiaToolStripMenuItem_Click_1);
            // 
            // hueModifierToolStripMenuItem
            // 
            this.hueModifierToolStripMenuItem.Name = "hueModifierToolStripMenuItem";
            this.hueModifierToolStripMenuItem.Size = new System.Drawing.Size(181, 24);
            this.hueModifierToolStripMenuItem.Text = "HueModifier";
            this.hueModifierToolStripMenuItem.Click += new System.EventHandler(this.hueModifierToolStripMenuItem_Click);
            // 
            // rotateChannelsToolStripMenuItem
            // 
            this.rotateChannelsToolStripMenuItem.Name = "rotateChannelsToolStripMenuItem";
            this.rotateChannelsToolStripMenuItem.Size = new System.Drawing.Size(181, 24);
            this.rotateChannelsToolStripMenuItem.Text = "RotateChannels";
            this.rotateChannelsToolStripMenuItem.Click += new System.EventHandler(this.rotateChannelsToolStripMenuItem_Click_1);
            // 
            // invertToolStripMenuItem
            // 
            this.invertToolStripMenuItem.Name = "invertToolStripMenuItem";
            this.invertToolStripMenuItem.Size = new System.Drawing.Size(181, 24);
            this.invertToolStripMenuItem.Text = "Invert";
            this.invertToolStripMenuItem.Click += new System.EventHandler(this.invertToolStripMenuItem_Click);
            // 
            // colorToolStripMenuItem
            // 
            this.colorToolStripMenuItem.Name = "colorToolStripMenuItem";
            this.colorToolStripMenuItem.Size = new System.Drawing.Size(57, 24);
            this.colorToolStripMenuItem.Text = "Color";
            this.colorToolStripMenuItem.Click += new System.EventHandler(this.colorToolStripMenuItem_Click);
            // 
            // colorSelectedAreaToolStripMenuItem
            // 
            this.colorSelectedAreaToolStripMenuItem.Name = "colorSelectedAreaToolStripMenuItem";
            this.colorSelectedAreaToolStripMenuItem.Size = new System.Drawing.Size(149, 24);
            this.colorSelectedAreaToolStripMenuItem.Text = "Color selected area";
            this.colorSelectedAreaToolStripMenuItem.Click += new System.EventHandler(this.colorSelectedAreaToolStripMenuItem_Click_1);
            // 
            // xRayScannerToolStripMenuItem
            // 
            this.xRayScannerToolStripMenuItem.Name = "xRayScannerToolStripMenuItem";
            this.xRayScannerToolStripMenuItem.Size = new System.Drawing.Size(104, 24);
            this.xRayScannerToolStripMenuItem.Text = "xRayScanner";
            this.xRayScannerToolStripMenuItem.Click += new System.EventHandler(this.xRayScannerToolStripMenuItem_Click);
            // 
            // openSecondImageToolStripMenuItem
            // 
            this.openSecondImageToolStripMenuItem.Name = "openSecondImageToolStripMenuItem";
            this.openSecondImageToolStripMenuItem.Size = new System.Drawing.Size(146, 24);
            this.openSecondImageToolStripMenuItem.Text = "openSecondImage";
            this.openSecondImageToolStripMenuItem.Click += new System.EventHandler(this.openSecondImageToolStripMenuItem_Click_1);
            // 
            // compareImagesToolStripMenuItem
            // 
            this.compareImagesToolStripMenuItem.Name = "compareImagesToolStripMenuItem";
            this.compareImagesToolStripMenuItem.Size = new System.Drawing.Size(128, 24);
            this.compareImagesToolStripMenuItem.Text = "compareImages";
            this.compareImagesToolStripMenuItem.Click += new System.EventHandler(this.compareImagesToolStripMenuItem_Click_1);
            // 
            // categorizeSeverityToolStripMenuItem
            // 
            this.categorizeSeverityToolStripMenuItem.Name = "categorizeSeverityToolStripMenuItem";
            this.categorizeSeverityToolStripMenuItem.Size = new System.Drawing.Size(143, 24);
            this.categorizeSeverityToolStripMenuItem.Text = "categorizeSeverity";
            this.categorizeSeverityToolStripMenuItem.Click += new System.EventHandler(this.categorizeSeverityToolStripMenuItem_Click_1);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.03712F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 49.96288F));
            this.tableLayoutPanel1.Controls.Add(this.pictureBox1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.pictureBox2, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 28);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1369, 695);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox1.Location = new System.Drawing.Point(3, 3);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(679, 689);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox2.Location = new System.Drawing.Point(688, 3);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(678, 689);
            this.pictureBox2.TabIndex = 1;
            this.pictureBox2.TabStop = false;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // openFileDialog2
            // 
            this.openFileDialog2.FileName = "openFileDialog2";
            // 
            // searchImagesToolStripMenuItem
            // 
            this.searchImagesToolStripMenuItem.Name = "searchImagesToolStripMenuItem";
            this.searchImagesToolStripMenuItem.Size = new System.Drawing.Size(111, 24);
            this.searchImagesToolStripMenuItem.Text = "searchImages";
            this.searchImagesToolStripMenuItem.Click += new System.EventHandler(this.searchImagesToolStripMenuItem_Click_1);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(1369, 723);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.menuStrip1);
            this.Location = new System.Drawing.Point(15, 15);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.ToolStripMenuItem searchImagesToolStripMenuItem;

        private System.Windows.Forms.ToolStripMenuItem categorizeSeverityToolStripMenuItem;

        private System.Windows.Forms.ToolStripMenuItem compareImagesToolStripMenuItem;

        private System.Windows.Forms.ToolStripMenuItem openSecondImageToolStripMenuItem;

        private System.Windows.Forms.OpenFileDialog openFileDialog2;

        private System.Windows.Forms.ToolStripMenuItem xRayScannerToolStripMenuItem;

        private System.Windows.Forms.OpenFileDialog openFileDialog1;

        private System.Windows.Forms.ToolStripMenuItem rotateChannelsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem invertToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem colorToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem colorSelectedAreaToolStripMenuItem;

        private System.Windows.Forms.ToolStripMenuItem hueModifierToolStripMenuItem;

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;

        private System.Windows.Forms.ToolStripMenuItem imageProcessingToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sepiaToolStripMenuItem;

        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.MenuStrip menuStrip1;

        #endregion
    }
}