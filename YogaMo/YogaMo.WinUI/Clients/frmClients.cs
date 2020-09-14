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

namespace YogaMo.WinUI.Clients
{
    public partial class frmClients : Form
    {
        private readonly APIService _serviceClients = new APIService("Clients");
        private readonly APIService _serviceCities = new APIService("Cities");
        public frmClients()
        {
            InitializeComponent();
        }

        private async Task LoadClients()
        {
            var request = new ClientSearchRequest()
            {
                Name = txtSearchName.Text
            };
            if (cmbSearchCity.SelectedItem != null)
                request.CityId = (cmbSearchCity.SelectedItem as City).CityId;

            var result = await _serviceClients.Get<List<Model.Client>>(request);
            dgvClients.AutoGenerateColumns = false;
            dgvClients.DataSource = result;
        }

        private async void frmClients_Load(object sender, EventArgs e)
        {
            await LoadCities();
        }

        private async Task LoadCities()
        {
            var list = await _serviceCities.Get<List<Model.City>>();
            list.Insert(0, new Model.City
            {
                CityId = 0,
                Name = "All"
            });
            cmbSearchCity.DataSource = list;
            cmbSearchCity.DisplayMember = "Name";
        }

        private async void cmbSearchCity_SelectedIndexChanged(object sender, EventArgs e)
        {
            await LoadClients();
        }

        private async void txtSearchName_TextChanged(object sender, EventArgs e)
        {
            await LoadClients();
        }

        private async void dgvClients_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            var client = dgvClients.SelectedRows[0].DataBoundItem as Model.Client;
            if(client != null)
            {
                var frm = new frmClientDetails(client.ClientId);
                if(frm.ShowDialog() == DialogResult.OK)
                {
                    await LoadClients();
                }
            }
        }
    }
}
