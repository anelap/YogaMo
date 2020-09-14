namespace YogaMo.WinUI.YogaClasses
{
    partial class frmYogaClasses
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dgvYogaClasses = new System.Windows.Forms.DataGridView();
            this.Yoga = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Day = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Time = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnNewClass = new System.Windows.Forms.Button();
            this.cmbDay = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvYogaClasses)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dgvYogaClasses);
            this.groupBox1.Location = new System.Drawing.Point(15, 77);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(791, 328);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Yoga Classes";
            // 
            // dgvYogaClasses
            // 
            this.dgvYogaClasses.AllowUserToAddRows = false;
            this.dgvYogaClasses.AllowUserToDeleteRows = false;
            this.dgvYogaClasses.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvYogaClasses.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Yoga,
            this.Day,
            this.Time});
            this.dgvYogaClasses.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvYogaClasses.Location = new System.Drawing.Point(3, 18);
            this.dgvYogaClasses.MultiSelect = false;
            this.dgvYogaClasses.Name = "dgvYogaClasses";
            this.dgvYogaClasses.ReadOnly = true;
            this.dgvYogaClasses.RowHeadersWidth = 51;
            this.dgvYogaClasses.RowTemplate.Height = 24;
            this.dgvYogaClasses.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvYogaClasses.Size = new System.Drawing.Size(785, 307);
            this.dgvYogaClasses.TabIndex = 0;
            this.dgvYogaClasses.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvYogaClasses_CellMouseDoubleClick);
            // 
            // Yoga
            // 
            this.Yoga.DataPropertyName = "Yoga";
            this.Yoga.HeaderText = "Yoga";
            this.Yoga.MinimumWidth = 6;
            this.Yoga.Name = "Yoga";
            this.Yoga.ReadOnly = true;
            this.Yoga.Width = 105;
            // 
            // Day
            // 
            this.Day.DataPropertyName = "Day";
            this.Day.HeaderText = "Day";
            this.Day.MinimumWidth = 6;
            this.Day.Name = "Day";
            this.Day.ReadOnly = true;
            this.Day.Width = 105;
            // 
            // Time
            // 
            this.Time.DataPropertyName = "Time";
            this.Time.HeaderText = "Time";
            this.Time.MinimumWidth = 6;
            this.Time.Name = "Time";
            this.Time.ReadOnly = true;
            this.Time.Width = 125;
            // 
            // btnNewClass
            // 
            this.btnNewClass.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.btnNewClass.Location = new System.Drawing.Point(18, 412);
            this.btnNewClass.Name = "btnNewClass";
            this.btnNewClass.Size = new System.Drawing.Size(162, 29);
            this.btnNewClass.TabIndex = 9;
            this.btnNewClass.Text = "New Class";
            this.btnNewClass.UseVisualStyleBackColor = true;
            this.btnNewClass.Click += new System.EventHandler(this.btnNewYogaClasses_Click);
            // 
            // cmbDay
            // 
            this.cmbDay.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDay.FormattingEnabled = true;
            this.cmbDay.Location = new System.Drawing.Point(18, 32);
            this.cmbDay.Name = "cmbDay";
            this.cmbDay.Size = new System.Drawing.Size(126, 24);
            this.cmbDay.TabIndex = 10;
            this.cmbDay.SelectedIndexChanged += new System.EventHandler(this.cmbDay_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(18, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(85, 17);
            this.label1.TabIndex = 11;
            this.label1.Text = "Day of week";
            // 
            // frmYogaClasses
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(813, 446);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmbDay);
            this.Controls.Add(this.btnNewClass);
            this.Controls.Add(this.groupBox1);
            this.Name = "frmYogaClasses";
            this.Text = "Yoga Classes";
            this.Load += new System.EventHandler(this.frmYogaClasses_Load);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvYogaClasses)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView dgvYogaClasses;
        private System.Windows.Forms.Button btnNewClass;
        private System.Windows.Forms.DataGridViewTextBoxColumn Yoga;
        private System.Windows.Forms.DataGridViewTextBoxColumn Day;
        private System.Windows.Forms.DataGridViewTextBoxColumn Time;
        private System.Windows.Forms.ComboBox cmbDay;
        private System.Windows.Forms.Label label1;
    }
}