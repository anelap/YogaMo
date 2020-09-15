using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using YogaMo.Model;

namespace YogaMo.WinUI.Clients
{
    public partial class frmClientDetails : Form
    {
        private Client _client;
        private int _clientId;

        private readonly APIService _serviceClients = new APIService("Clients");
        private readonly APIService _serviceCities = new APIService("Cities");

        public frmClientDetails(int ClientId)
        {
            InitializeComponent();
            this._clientId = ClientId;
        }

        private async void frmClientDetails_Load(object sender, EventArgs e)
        {
            _client = await _serviceClients.GetById<Model.Client>(_clientId);

            txtFirstName.Text = _client.FirstName;
            txtLastName.Text = _client.LastName;
            txtEmail.Text = _client.Email;
            txtPhone.Text = _client.Phone;
            txtUsername.Text = _client.Username;
            txtGender.Text = _client.Gender;

            cmbCity.DataSource = await _serviceCities.Get<List<Model.City>>();
            cmbCity.DisplayMember = "Name";

            foreach (Model.City city in cmbCity.Items)
            {
                if (city.CityId == _client.CityId)
                    cmbCity.SelectedItem = city;
            }
        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
                return;

            var request = new Model.Requests.ClientUpdateRequest
            {
                FirstName = txtFirstName.Text,
                LastName = txtLastName.Text,
                Email = txtEmail.Text,
                Gender = txtGender.Text,
                Phone = txtPhone.Text,
                Username = txtUsername.Text,
                CityId = (cmbCity.SelectedItem as Model.City).CityId,
                ProfilePicture = _client.ProfilePicture
            };
            if (!string.IsNullOrEmpty(txtPassword.Text))
            {
                request.Password = txtPassword.Text;
            }

            var client = await _serviceClients.Update<Model.Client>(_clientId, request);
            if (client != null)
            {
                MessageBox.Show("Client profile has been successfully updated.", "Update profile", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = DialogResult.OK;
            }
        }

        private void txtFirstName_Validating(object sender, CancelEventArgs e)
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
            if (_clientId == 0 && string.IsNullOrEmpty(element.Text))
            {
                errorProvider1.SetError(element, Properties.Resources.Validation_Required);
            }
            else if ((_clientId == 0 || !string.IsNullOrEmpty(element.Text)) && element.Text.Length < 4)
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
            if ((_clientId == 0 || !string.IsNullOrEmpty(txtPassword.Text)) && string.IsNullOrEmpty(element.Text))
            {
                errorProvider1.SetError(element, Properties.Resources.Validation_Required);
            }
            else if ((_clientId == 0 || !string.IsNullOrEmpty(txtPassword.Text)) && txtPassword.Text != element.Text)
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

        private void cmbCity_Validating(object sender, CancelEventArgs e)
        {
            var element = (sender as ComboBox);
            if (element.SelectedItem == null)
            {
                errorProvider1.SetError(element, Properties.Resources.Validation_Required);
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
