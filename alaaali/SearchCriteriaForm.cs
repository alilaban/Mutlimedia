using System;
using System.Windows.Forms;

namespace alaaali
{
    public partial class SearchCriteriaForm : Form
    {
        public long MinSize { get; private set; }
        public long MaxSize { get; private set; }
        public DateTime? MinDate { get; private set; }
        public DateTime? MaxDate { get; private set; }

        public SearchCriteriaForm()
        {
            InitializeComponent();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            MinSize = (long)numMinSize.Value;
            MaxSize = (long)numMaxSize.Value;
            MinDate = dtpMinDate.Value;
            MaxDate = dtpMaxDate.Value;

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void InitializeComponent()
        {
            this.numMinSize = new System.Windows.Forms.NumericUpDown();
            this.numMaxSize = new System.Windows.Forms.NumericUpDown();
            this.dtpMinDate = new System.Windows.Forms.DateTimePicker();
            this.dtpMaxDate = new System.Windows.Forms.DateTimePicker();
            this.btnSearch = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.numMinSize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMaxSize)).BeginInit();
            this.SuspendLayout();
            // 
            // numMinSize
            // 
            this.numMinSize.Location = new System.Drawing.Point(12, 12);
            this.numMinSize.Maximum = new decimal(new int[] {
            2147483647,
            0,
            0,
            0});
            this.numMinSize.Name = "numMinSize";
            this.numMinSize.Size = new System.Drawing.Size(120, 20);
            this.numMinSize.TabIndex = 0;
            // 
            // numMaxSize
            // 
            this.numMaxSize.Location = new System.Drawing.Point(12, 38);
            this.numMaxSize.Maximum = new decimal(new int[] {
            2147483647,
            0,
            0,
            0});
            this.numMaxSize.Name = "numMaxSize";
            this.numMaxSize.Size = new System.Drawing.Size(120, 20);
            this.numMaxSize.TabIndex = 1;
            // 
            // dtpMinDate
            // 
            this.dtpMinDate.Location = new System.Drawing.Point(12, 64);
            this.dtpMinDate.Name = "dtpMinDate";
            this.dtpMinDate.Size = new System.Drawing.Size(200, 20);
            this.dtpMinDate.TabIndex = 2;
            // 
            // dtpMaxDate
            // 
            this.dtpMaxDate.Location = new System.Drawing.Point(12, 90);
            this.dtpMaxDate.Name = "dtpMaxDate";
            this.dtpMaxDate.Size = new System.Drawing.Size(200, 20);
            this.dtpMaxDate.TabIndex = 3;
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(12, 116);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 23);
            this.btnSearch.TabIndex = 4;
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // SearchCriteriaForm
            // 
            this.ClientSize = new System.Drawing.Size(224, 151);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.dtpMaxDate);
            this.Controls.Add(this.dtpMinDate);
            this.Controls.Add(this.numMaxSize);
            this.Controls.Add(this.numMinSize);
            this.Name = "SearchCriteriaForm";
            this.Text = "Search Criteria";
            ((System.ComponentModel.ISupportInitialize)(this.numMinSize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMaxSize)).EndInit();
            this.ResumeLayout(false);

        }

        private System.Windows.Forms.NumericUpDown numMinSize;
        private System.Windows.Forms.NumericUpDown numMaxSize;
        private System.Windows.Forms.DateTimePicker dtpMinDate;
        private System.Windows.Forms.DateTimePicker dtpMaxDate;
        private System.Windows.Forms.Button btnSearch;
    }
}
