using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using YogaMo.Model;
using YogaMo.WinUI.Helper;

namespace YogaMo.WinUI.Instructors
{
    public partial class frmInstructorDetails : Form
    {
        private Instructor _instructor;
        private int _instructorId;

        private readonly APIService _serviceInstructors = new APIService("Instructors");
        private Model.Requests.InstructorsInsertRequest request = new Model.Requests.InstructorsInsertRequest();

        public frmInstructorDetails(int InstructorId = 0)
        {
            InitializeComponent();
            this._instructorId = InstructorId;
        }

        private async void frmInstructorDetails_Load(object sender, EventArgs e)
        {
            if (_instructorId == 0)
            {
                pictureBox1.Image = Properties.Resources.default_profile;
                return;
            }

            _instructor = await _serviceInstructors.GetById<Model.Instructor>(_instructorId);

            txtFirstName.Text = _instructor.FirstName;
            txtLastName.Text = _instructor.LastName;
            txtEmail.Text = _instructor.Email;
            txtPhone.Text = _instructor.Phone;
            txtUsername.Text = _instructor.Username;
            txtTitle.Text = _instructor.Title;

            if (_instructor.ProfilePicture != null && _instructor.ProfilePicture.Length > 0)
            {
                var img = ImageHelper.ByteToImage(_instructor.ProfilePicture);
                pictureBox1.Image = img;
                request.ProfilePicture = _instructor.ProfilePicture;
            }
            else
            {
                pictureBox1.Image = Properties.Resources.default_profile;
            }
        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
            request.FirstName = txtFirstName.Text;
            request.LastName = txtLastName.Text;
            request.Email = txtEmail.Text;
            request.Title = txtTitle.Text;
            request.Phone = txtPhone.Text;
            request.Username = txtUsername.Text;

            if (!string.IsNullOrEmpty(txtPassword.Text) || _instructorId == 0)
            {
                request.Password = txtPassword.Text;
                request.PasswordConfirmation = txtPassConfirm.Text;
            }

            if (_instructorId == 0) // inserting
            {
                var instructor = await _serviceInstructors.Insert<Model.Instructor>(request);
                if (instructor != null)
                {
                    MessageBox.Show("Instructor profile has been successfully created.", "Create profile", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    DialogResult = DialogResult.OK;
                }
            }
            else // updating
            {
                var instructor = await _serviceInstructors.Update<Model.Instructor>(_instructorId, request);
                if (instructor != null)
                {
                    MessageBox.Show("Instructor profile has been successfully updated.", "Update profile", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    DialogResult = DialogResult.OK;
                }
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                var file = File.ReadAllBytes(openFileDialog1.FileName);
                if (file != null && file.Length > 0)
                {
                    request.ProfilePicture = file;
                    pictureBox1.Image = Image.FromFile(openFileDialog1.FileName);
                }
            }
        }
    }
}
