using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
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
            if(client != null)
            {
                MessageBox.Show("Client profile has been successfully updated.", "Update profile", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = DialogResult.OK;
            }
        }
    }
}
