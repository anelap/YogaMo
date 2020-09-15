using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace YogaMo.WinUI
{
    public partial class frmLogin : Form
    {
        APIService _serviceInstructors = new APIService("Instructors");

        public frmLogin()
        {
            InitializeComponent();
        }

        private async void btnLogin_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
                return;

            APIService.Username = txtUsername.Text;
            APIService.Password = txtPassword.Text;
            try
            {
                APIService.CurrentInstructor = await _serviceInstructors.Get<Model.Instructor>(null, "MyProfile");

                if (APIService.CurrentInstructor == null)
                    return;

                DialogResult = DialogResult.OK;
            }
            catch (Exception)
            {

            }

        }

        private void txtUsername_Validating(object sender, CancelEventArgs e)
        {
            ValidateRequired(sender, e);
        }

        private void txtPassword_Validating(object sender, CancelEventArgs e)
        {
            ValidateRequired(sender, e);
        }

        private void ValidateRequired(object sender, CancelEventArgs e)
        {
            var element = (sender as TextBox);
            if (string.IsNullOrEmpty(element.Text))
            {
                errorProvider1.SetError(element, Properties.Resources.Validation_Required);
                e.Cancel = true;
            }
            else
                errorProvider1.SetError(element, null);
        }

        private void txtUsername_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
            {
                btnLogin.PerformClick();
            }
        }

        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
            {
                btnLogin.PerformClick();
            }
        }
    }
}
