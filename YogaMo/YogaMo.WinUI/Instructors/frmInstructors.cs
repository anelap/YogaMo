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
using YogaMo.Model;

namespace YogaMo.WinUI.Instructors
{
    public partial class frmInstructors : Form
    {
        private readonly APIService _serviceInstructors = new APIService("Instructors");
        public frmInstructors()
        {
            InitializeComponent();
        }

        private async Task LoadInstructors()
        {
            var request = new InstructorsSearchRequest()
            {
                Name = txtSearchName.Text
            };
            var result = await _serviceInstructors.Get<List<Model.Instructor>>(request);
            dgvInstructors.AutoGenerateColumns = false;
            dgvInstructors.DataSource = result;
        }

        private async void frmInstructors_Load(object sender, EventArgs e)
        {
            await LoadInstructors();
        }

        private async void txtSearchName_TextChanged(object sender, EventArgs e)
        {
            await LoadInstructors();
        }

        private async void dgvInstructors_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            var instructor = dgvInstructors.SelectedRows[0].DataBoundItem as Model.Instructor;
            if(instructor != null)
            {
                var frm = new frmInstructorDetails(instructor.InstructorId);
                if(frm.ShowDialog() == DialogResult.OK)
                {
                    await LoadInstructors();
                }
            }
        }

        private async void btnNewInstructor_Click(object sender, EventArgs e)
        {
            frmInstructorDetails frm = new frmInstructorDetails();
            if (frm.ShowDialog() == DialogResult.OK)
                await LoadInstructors();
        }
    }
}
