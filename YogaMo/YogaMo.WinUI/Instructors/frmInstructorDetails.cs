using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
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
            if (!this.ValidateChildren())
                return;

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
                    if (_instructorId == APIService.CurrentInstructor.InstructorId)
                    {
                        APIService.Username = request.Username;
                        if (!string.IsNullOrEmpty(request.Password))
                            APIService.Password = request.Password;
                    }

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

        private void txtLastName_Validating(object sender, CancelEventArgs e)
        {
            var element = (sender as TextBox);
            if (string.IsNullOrEmpty(element.Text))
            {
                errorProvider1.SetError(element, Properties.Resources.Validation_Required);
            }
            else if (element.Text.Length > 50)
            {
                errorProvider1.SetError(element, String.Format(Properties.Resources.Validation_MaximumLength, 50));
            }
            else
            {
                errorProvider1.SetError(element, null);
                return;
            }
            e.Cancel = true;
        }

        private void txtPhone_Validating(object sender, CancelEventArgs e)
        {
            Regex phoneRegex1 = new Regex(@"[0-9]{9,10}");
            Regex phoneRegex2 = new Regex(@"\+[0-9]{10,15}");

            var element = (sender as TextBox);
            if (string.IsNullOrEmpty(element.Text))
            {
                errorProvider1.SetError(element, Properties.Resources.Validation_Required);
            }
            else if (element.Text.Length > 16)
            {
                errorProvider1.SetError(element, String.Format(Properties.Resources.Validation_MaximumLength, 16));
            }
            else if (!phoneRegex1.IsMatch(element.Text) && !phoneRegex2.IsMatch(element.Text))
            {
                errorProvider1.SetError(element, Properties.Resources.Validation_PhoneNotValid);
            }
            else
            {
                errorProvider1.SetError(element, null);
                return;
            }
            e.Cancel = true;
        }

        private void txtEmail_Validating(object sender, CancelEventArgs e)
        {
            Regex emailRegex = new Regex(@"(?:[a-zA-Z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-zA-Z0-9!#$%&'*+/=?^_`{|}~-]+)*|""(?:[\x01-\x08\x0b\x0c\x0e-\x1f\x21\x23-\x5b\x5d-\x7f]|\\[\x01-\x09\x0b\x0c\x0e-\x7f])*"")@(?:(?:[a-zA-Z0-9](?:[a-zA-Z0-9-]*[a-zA-Z0-9])?\.)+[a-zA-Z0-9](?:[a-zA-Z0-9-]*[a-zA-Z0-9])?|\[(?:(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?|[a-zA-Z0-9-]*[a-zA-Z0-9]:(?:[\x01-\x08\x0b\x0c\x0e-\x1f\x21-\x5a\x53-\x7f]|\\[\x01-\x09\x0b\x0c\x0e-\x7f])+)\])");

            var element = (sender as TextBox);
            if (string.IsNullOrEmpty(element.Text))
            {
                errorProvider1.SetError(element, Properties.Resources.Validation_Required);
            }
            else if (element.Text.Length > 50)
            {
                errorProvider1.SetError(element, String.Format(Properties.Resources.Validation_MaximumLength, 50));
            }
            else if (!emailRegex.IsMatch(element.Text))
            {
                errorProvider1.SetError(element, Properties.Resources.Validation_EmailNotValid);
            }
            else
            {
                errorProvider1.SetError(element, null);
                return;
            }
            e.Cancel = true;
        }

        private void txtUsername_Validating(object sender, CancelEventArgs e)
        {
            var element = (sender as TextBox);
            if (string.IsNullOrEmpty(element.Text))
            {
                errorProvider1.SetError(element, Properties.Resources.Validation_Required);
            }
            else if (element.Text.Length > 15)
            {
                errorProvider1.SetError(element, String.Format(Properties.Resources.Validation_MaximumLength, 15));
            }
            else if (element.Text.Length < 4)
            {
                errorProvider1.SetError(element, String.Format(Properties.Resources.Validation_MinimumLength, 4));
            }
            else
            {
                errorProvider1.SetError(element, null);
                return;
            }
            e.Cancel = true;
        }

        private void txtPassword_Validating(object sender, CancelEventArgs e)
        {
            var element = (sender as TextBox);
            if (_instructorId == 0 && string.IsNullOrEmpty(element.Text))
            {
                errorProvider1.SetError(element, Properties.Resources.Validation_Required);
            }
            else if ((_instructorId == 0 || !string.IsNullOrEmpty(element.Text)) && element.Text.Length < 4)
            {
                errorProvider1.SetError(element, String.Format(Properties.Resources.Validation_MinimumLength, 4));
            }
            else
            {
                errorProvider1.SetError(element, null);
                return;
            }
            e.Cancel = true;
        }

        private void txtPassConfirm_Validating(object sender, CancelEventArgs e)
        {
            var element = (sender as TextBox);
            if ((_instructorId == 0 || !string.IsNullOrEmpty(txtPassword.Text)) && string.IsNullOrEmpty(element.Text))
            {
                errorProvider1.SetError(element, Properties.Resources.Validation_Required);
            }
            else if ((_instructorId == 0 || !string.IsNullOrEmpty(txtPassword.Text)) && txtPassword.Text != element.Text)
            {
                errorProvider1.SetError(element, Properties.Resources.Validation_PasswordNotMatching);
            }
            else
            {
                errorProvider1.SetError(element, null);
                return;
            }
            e.Cancel = true;
        }
    }
}
