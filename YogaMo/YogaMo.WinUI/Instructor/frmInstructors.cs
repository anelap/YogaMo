using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Flurl.Http;
using Flurl;
using YogaMo.Model.Requests;

namespace YogaMo.WinUI.Instructor
{
    public partial class frmInstructors : Form
    {
        private readonly APIService _apiService = new APIService("instruktori");
        public frmInstructors()
        {
            InitializeComponent();
        }

        private async void btnShow_Click(object sender, EventArgs e)
        {
            var search = new InstructorSearchRequest() { FirstName = txtSearch.Text };

            var result = await _apiService.Get<List<Model.Instructor>>(search);

            dgvInstructors.AutoGenerateColumns = false;
            dgvInstructors.DataSource = result;
        }

        private void frmInstructors_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            // selecting the first row and value of the first cell
          /*  var id = dgvInstructors.SelectedRows[0].Cells[0].Value;

            frmInstructorsDetails frm = new frmInstructorsDetails(int.Parse(id.ToString()));

            frm.Show();*/
        }

        private void dgvInstructors_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            // selecting the first row and value of the first cell
            var id = dgvInstructors.SelectedRows[0].Cells[0].Value;

            frmInstructorsDetails frm = new frmInstructorsDetails(int.Parse(id.ToString()));

            frm.Show();
        }
    }
}
