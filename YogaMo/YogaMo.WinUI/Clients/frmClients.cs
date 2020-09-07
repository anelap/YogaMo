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

namespace YogaMo.WinUI.Clients
{
    public partial class frmClients : Form
    {
        private readonly APIService _apiService = new APIService("Clients");
        public frmClients()
        {
            InitializeComponent();
        }

        private async void btnSearch_Click(object sender, EventArgs e)
        {
            var search = new ClientSearchRequest()
            {
                FirstName = txtSearchName.Text,
                City = txtSearchCity.Text
            };


            var result = await _apiService.Get<List<Model.Client>>(search);
            dgvClients.AutoGenerateColumns = false;
            dgvClients.DataSource = result;
        }

        private void dgvClients_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
