using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace YogaMo.WinUI.Instructor
{
    public partial class frmInstructorsDetails : Form
    {
        private readonly APIService _service = new APIService("instruktori");
        private int? _id = null;

        public frmInstructorsDetails(int? instructorId = null)
        {
            InitializeComponent();
            _id = instructorId;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {

        }

        private async void frmInstructorsDetails_Load(object sender, EventArgs e)
        {
            if (_id.HasValue)
            {
                var instructor = await _service.GetById<Model.Instructor>(_id);

                txtEmail.Text = instructor.Email;
                txtFirstName.Text = instructor.FirstName;
                txtLastName.Text = instructor.LastName;
                txtUsername.Text = instructor.Username;
                txtPhone.Text = instructor.Phone;

            }
        }
    }
}
