using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using YogaMo.WinUI.Clients;
using YogaMo.WinUI.Instructors;
using YogaMo.WinUI.Orders;
using YogaMo.WinUI.Products;
using YogaMo.WinUI.YogaClasses;

namespace YogaMo.WinUI
{
    public partial class frmIndex : Form
    {
        public frmIndex()
        {
            InitializeComponent();
        }

        private void clientsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmClients frm = new frmClients();
            frm.MdiParent = this;
            frm.WindowState = FormWindowState.Maximized;
            frm.Show();
        }

        private void searchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmInstructors frm = new frmInstructors();
            frm.MdiParent = this;
            frm.WindowState = FormWindowState.Maximized;
            frm.Show();
        }

        private void newInstructorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmInstructorDetails frm = new frmInstructorDetails();
            frm.ShowDialog();
        }

        private void scheduleToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmYogaClasses frm = new frmYogaClasses();
            frm.MdiParent = this;
            frm.WindowState = FormWindowState.Maximized;
            frm.Show();
        }

        private void newClassToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmYogaClassDetails frm = new frmYogaClassDetails();
            frm.ShowDialog();
        }

        private void productSearchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmProducts frm = new frmProducts();
            frm.MdiParent = this;
            frm.WindowState = FormWindowState.Maximized;
            frm.Show();
        }

        private void newProductsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmProductDetails frm = new frmProductDetails();
            frm.ShowDialog();
        }

        private void ordersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmOrders frm = new frmOrders();
            frm.MdiParent = this;
            frm.WindowState = FormWindowState.Maximized;
            frm.Show();
        }
    }
}
