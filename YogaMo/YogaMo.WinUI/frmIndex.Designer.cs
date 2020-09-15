﻿namespace YogaMo.WinUI
{
    partial class frmIndex
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
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.yogaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.searchYogaToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.newYogaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.yogaClassesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.searchToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.newClassToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.instructorsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.searchToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newInstructorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.productsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.serachToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newProductsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ordersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.usersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip.SuspendLayout();
            this.menuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip
            // 
            this.statusStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel});
            this.statusStrip.Location = new System.Drawing.Point(0, 532);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Padding = new System.Windows.Forms.Padding(1, 0, 19, 0);
            this.statusStrip.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.statusStrip.Size = new System.Drawing.Size(857, 41);
            this.statusStrip.SizingGrip = false;
            this.statusStrip.TabIndex = 2;
            this.statusStrip.Text = "StatusStrip";
            // 
            // toolStripStatusLabel
            // 
            this.toolStripStatusLabel.ActiveLinkColor = System.Drawing.Color.Blue;
            this.toolStripStatusLabel.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.toolStripStatusLabel.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) 
            | System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom)));
            this.toolStripStatusLabel.BorderStyle = System.Windows.Forms.Border3DStyle.Raised;
            this.toolStripStatusLabel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripStatusLabel.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.toolStripStatusLabel.IsLink = true;
            this.toolStripStatusLabel.LinkBehavior = System.Windows.Forms.LinkBehavior.NeverUnderline;
            this.toolStripStatusLabel.LinkColor = System.Drawing.Color.Black;
            this.toolStripStatusLabel.Name = "toolStripStatusLabel";
            this.toolStripStatusLabel.Padding = new System.Windows.Forms.Padding(3);
            this.toolStripStatusLabel.Size = new System.Drawing.Size(81, 35);
            this.toolStripStatusLabel.Text = "Logout";
            this.toolStripStatusLabel.VisitedLinkColor = System.Drawing.Color.Black;
            this.toolStripStatusLabel.Click += new System.EventHandler(this.logoutLabel_Click);
            // 
            // menuStrip
            // 
            this.menuStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.yogaToolStripMenuItem,
            this.yogaClassesToolStripMenuItem,
            this.instructorsToolStripMenuItem,
            this.productsToolStripMenuItem,
            this.ordersToolStripMenuItem,
            this.usersToolStripMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(857, 28);
            this.menuStrip.TabIndex = 0;
            this.menuStrip.Text = "MenuStrip";
            // 
            // yogaToolStripMenuItem
            // 
            this.yogaToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.searchYogaToolStripMenuItem2,
            this.newYogaToolStripMenuItem});
            this.yogaToolStripMenuItem.Name = "yogaToolStripMenuItem";
            this.yogaToolStripMenuItem.Size = new System.Drawing.Size(56, 24);
            this.yogaToolStripMenuItem.Text = "Yoga";
            // 
            // searchYogaToolStripMenuItem2
            // 
            this.searchYogaToolStripMenuItem2.Name = "searchYogaToolStripMenuItem2";
            this.searchYogaToolStripMenuItem2.Size = new System.Drawing.Size(224, 26);
            this.searchYogaToolStripMenuItem2.Text = "Search";
            this.searchYogaToolStripMenuItem2.Click += new System.EventHandler(this.searchYogaToolStripMenuItem2_Click);
            // 
            // newYogaToolStripMenuItem
            // 
            this.newYogaToolStripMenuItem.Name = "newYogaToolStripMenuItem";
            this.newYogaToolStripMenuItem.Size = new System.Drawing.Size(224, 26);
            this.newYogaToolStripMenuItem.Text = "New yoga";
            this.newYogaToolStripMenuItem.Click += new System.EventHandler(this.newYogaToolStripMenuItem_Click);
            // 
            // yogaClassesToolStripMenuItem
            // 
            this.yogaClassesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.searchToolStripMenuItem1,
            this.newClassToolStripMenuItem});
            this.yogaClassesToolStripMenuItem.Name = "yogaClassesToolStripMenuItem";
            this.yogaClassesToolStripMenuItem.Size = new System.Drawing.Size(105, 24);
            this.yogaClassesToolStripMenuItem.Text = "Yoga classes";
            // 
            // searchToolStripMenuItem1
            // 
            this.searchToolStripMenuItem1.Name = "searchToolStripMenuItem1";
            this.searchToolStripMenuItem1.Size = new System.Drawing.Size(157, 26);
            this.searchToolStripMenuItem1.Text = "Schedule";
            this.searchToolStripMenuItem1.Click += new System.EventHandler(this.scheduleToolStripMenuItem1_Click);
            // 
            // newClassToolStripMenuItem
            // 
            this.newClassToolStripMenuItem.Name = "newClassToolStripMenuItem";
            this.newClassToolStripMenuItem.Size = new System.Drawing.Size(157, 26);
            this.newClassToolStripMenuItem.Text = "New class";
            this.newClassToolStripMenuItem.Click += new System.EventHandler(this.newClassToolStripMenuItem_Click);
            // 
            // instructorsToolStripMenuItem
            // 
            this.instructorsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.searchToolStripMenuItem,
            this.newInstructorToolStripMenuItem});
            this.instructorsToolStripMenuItem.Name = "instructorsToolStripMenuItem";
            this.instructorsToolStripMenuItem.Size = new System.Drawing.Size(91, 24);
            this.instructorsToolStripMenuItem.Text = "Instructors";
            // 
            // searchToolStripMenuItem
            // 
            this.searchToolStripMenuItem.Name = "searchToolStripMenuItem";
            this.searchToolStripMenuItem.Size = new System.Drawing.Size(188, 26);
            this.searchToolStripMenuItem.Text = "Search";
            this.searchToolStripMenuItem.Click += new System.EventHandler(this.searchToolStripMenuItem_Click);
            // 
            // newInstructorToolStripMenuItem
            // 
            this.newInstructorToolStripMenuItem.Name = "newInstructorToolStripMenuItem";
            this.newInstructorToolStripMenuItem.Size = new System.Drawing.Size(188, 26);
            this.newInstructorToolStripMenuItem.Text = "New instructor";
            this.newInstructorToolStripMenuItem.Click += new System.EventHandler(this.newInstructorToolStripMenuItem_Click);
            // 
            // productsToolStripMenuItem
            // 
            this.productsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.serachToolStripMenuItem,
            this.newProductsToolStripMenuItem});
            this.productsToolStripMenuItem.Name = "productsToolStripMenuItem";
            this.productsToolStripMenuItem.Size = new System.Drawing.Size(80, 24);
            this.productsToolStripMenuItem.Text = "Products";
            // 
            // serachToolStripMenuItem
            // 
            this.serachToolStripMenuItem.Name = "serachToolStripMenuItem";
            this.serachToolStripMenuItem.Size = new System.Drawing.Size(184, 26);
            this.serachToolStripMenuItem.Text = "Search";
            this.serachToolStripMenuItem.Click += new System.EventHandler(this.productSearchToolStripMenuItem_Click);
            // 
            // newProductsToolStripMenuItem
            // 
            this.newProductsToolStripMenuItem.Name = "newProductsToolStripMenuItem";
            this.newProductsToolStripMenuItem.Size = new System.Drawing.Size(184, 26);
            this.newProductsToolStripMenuItem.Text = "New products";
            this.newProductsToolStripMenuItem.Click += new System.EventHandler(this.newProductsToolStripMenuItem_Click);
            // 
            // ordersToolStripMenuItem
            // 
            this.ordersToolStripMenuItem.Name = "ordersToolStripMenuItem";
            this.ordersToolStripMenuItem.Size = new System.Drawing.Size(67, 24);
            this.ordersToolStripMenuItem.Text = "Orders";
            this.ordersToolStripMenuItem.Click += new System.EventHandler(this.ordersToolStripMenuItem_Click);
            // 
            // usersToolStripMenuItem
            // 
            this.usersToolStripMenuItem.Name = "usersToolStripMenuItem";
            this.usersToolStripMenuItem.Size = new System.Drawing.Size(67, 24);
            this.usersToolStripMenuItem.Text = "Clients";
            this.usersToolStripMenuItem.Click += new System.EventHandler(this.clientsToolStripMenuItem_Click);
            // 
            // frmIndex
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(857, 573);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.menuStrip);
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "frmIndex";
            this.Text = "YogaMo - Become One";
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion

        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem usersToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem instructorsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem searchToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newInstructorToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem yogaClassesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem searchToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem newClassToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem productsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem serachToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newProductsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ordersToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem yogaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem searchYogaToolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem newYogaToolStripMenuItem;
    }
}



