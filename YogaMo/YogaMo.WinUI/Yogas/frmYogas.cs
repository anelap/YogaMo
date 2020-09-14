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

namespace YogaMo.WinUI.Yogas
{
    public partial class frmYogas : Form
    {
        private readonly APIService _serviceYogas = new APIService("Yogas");
        public frmYogas()
        {
            InitializeComponent();
        }

        private async Task LoadYogas()
        {
            var request = new YogaSearchRequest()
            {
                Name = txtSearchName.Text
            };
            var result = await _serviceYogas.Get<List<Model.Yoga>>(request);
            dgvYogas.AutoGenerateColumns = false;
            dgvYogas.DataSource = result;
        }

        private async void frmYogas_Load(object sender, EventArgs e)
        {
            await LoadYogas();
        }

        private async void txtSearchName_TextChanged(object sender, EventArgs e)
        {
            await LoadYogas();
        }

        private async void dgvYogas_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            var yoga = dgvYogas.SelectedRows[0].DataBoundItem as Model.Yoga;
            if(yoga != null)
            {
                var frm = new frmYogaDetails(yoga.YogaId);
                if(frm.ShowDialog() == DialogResult.OK)
                {
                    await LoadYogas();
                }
            }
        }

        private async void btnNewYoga_Click(object sender, EventArgs e)
        {
            frmYogaDetails frm = new frmYogaDetails();
            if (frm.ShowDialog() == DialogResult.OK)
                await LoadYogas();
        }
    }
}
