namespace YogaMo.WinUI.Yogas
{
    partial class frmYogas
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
            this.dgvYogas = new System.Windows.Forms.DataGridView();
            this.txtSearchName = new System.Windows.Forms.TextBox();
            this.lblFirstName = new System.Windows.Forms.Label();
            this.btnNewInstructor = new System.Windows.Forms.Button();
            this.YogaName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Instructor = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvYogas)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dgvYogas);
            this.groupBox1.Location = new System.Drawing.Point(15, 77);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(576, 328);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // dgvYogas
            // 
            this.dgvYogas.AllowUserToAddRows = false;
            this.dgvYogas.AllowUserToDeleteRows = false;
            this.dgvYogas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvYogas.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.YogaName,
            this.Instructor});
            this.dgvYogas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvYogas.Location = new System.Drawing.Point(3, 18);
            this.dgvYogas.MultiSelect = false;
            this.dgvYogas.Name = "dgvYogas";
            this.dgvYogas.ReadOnly = true;
            this.dgvYogas.RowHeadersWidth = 51;
            this.dgvYogas.RowTemplate.Height = 24;
            this.dgvYogas.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvYogas.Size = new System.Drawing.Size(570, 307);
            this.dgvYogas.TabIndex = 0;
            this.dgvYogas.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvYogas_CellMouseDoubleClick);
            // 
            // txtSearchName
            // 
            this.txtSearchName.Location = new System.Drawing.Point(15, 34);
            this.txtSearchName.Name = "txtSearchName";
            this.txtSearchName.Size = new System.Drawing.Size(165, 22);
            this.txtSearchName.TabIndex = 2;
            this.txtSearchName.TextChanged += new System.EventHandler(this.txtSearchName_TextChanged);
            // 
            // lblFirstName
            // 
            this.lblFirstName.AutoSize = true;
            this.lblFirstName.Location = new System.Drawing.Point(15, 13);
            this.lblFirstName.Name = "lblFirstName";
            this.lblFirstName.Size = new System.Drawing.Size(45, 17);
            this.lblFirstName.TabIndex = 5;
            this.lblFirstName.Text = "Name";
            // 
            // btnNewInstructor
            // 
            this.btnNewInstructor.Font = new System.Drawing.Font("Tahoma", 8.25F);
            this.btnNewInstructor.Location = new System.Drawing.Point(18, 412);
            this.btnNewInstructor.Name = "btnNewInstructor";
            this.btnNewInstructor.Size = new System.Drawing.Size(162, 29);
            this.btnNewInstructor.TabIndex = 9;
            this.btnNewInstructor.Text = "New Yoga";
            this.btnNewInstructor.UseVisualStyleBackColor = true;
            this.btnNewInstructor.Click += new System.EventHandler(this.btnNewYoga_Click);
            // 
            // YogaName
            // 
            this.YogaName.DataPropertyName = "Name";
            this.YogaName.HeaderText = "Name";
            this.YogaName.MinimumWidth = 6;
            this.YogaName.Name = "YogaName";
            this.YogaName.ReadOnly = true;
            this.YogaName.Width = 130;
            // 
            // Instructor
            // 
            this.Instructor.DataPropertyName = "Instructor";
            this.Instructor.HeaderText = "Instructor";
            this.Instructor.MinimumWidth = 6;
            this.Instructor.Name = "Instructor";
            this.Instructor.ReadOnly = true;
            this.Instructor.Width = 125;
            // 
            // frmYogas
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(603, 446);
            this.Controls.Add(this.btnNewInstructor);
            this.Controls.Add(this.lblFirstName);
            this.Controls.Add(this.txtSearchName);
            this.Controls.Add(this.groupBox1);
            this.Name = "frmYogas";
            this.Text = "Yogas";
            this.Load += new System.EventHandler(this.frmYogas_Load);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvYogas)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView dgvYogas;
        private System.Windows.Forms.TextBox txtSearchName;
        private System.Windows.Forms.Label lblFirstName;
        private System.Windows.Forms.Button btnNewInstructor;
        private System.Windows.Forms.DataGridViewTextBoxColumn YogaName;
        private System.Windows.Forms.DataGridViewTextBoxColumn Instructor;
    }
}