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
using YogaMo.WinUI.Helper;

namespace YogaMo.WinUI.Orders
{
    public partial class frmOrders : Form
    {
        private readonly APIService _serviceOrders = new APIService("Orders");
        private readonly APIService _serviceCategories = new APIService("Categories");
        public frmOrders()
        {
            InitializeComponent();
        }

        private async Task LoadOrders()
        {
            var request = new OrderSearchRequest();
            if (cmbOrderStatuses.Text != "All")
            {
                foreach (var status in Enum.GetValues(typeof(OrderStatus)))
                {
                    if (status.ToString() == cmbOrderStatuses.Text)
                    {
                        request.OrderStatus = (OrderStatus?)status;

                    }
                }
            }

            var result = await _serviceOrders.Get<List<Model.Order>>(request);
            dgvOrders.AutoGenerateColumns = false;
            dgvOrders.DataSource = result;
        }

        private async void frmOrders_Load(object sender, EventArgs e)
        {
            LoadOrderStatuses();
            await LoadOrders().ConfigureAwait(false);
        }

        private void LoadOrderStatuses()
        {
            var list = new List<string>();
            list.Add("All");
            foreach (OrderStatus item in Enum.GetValues(typeof(OrderStatus)))
            {
                list.Add(item.ToString());
            }

            cmbOrderStatuses.DataSource = list;
        }



        private async void cmbOrderStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            await LoadOrders();
        }
        private async void dgvOrders_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            var Order = dgvOrders.SelectedRows[0].DataBoundItem as Model.Order;
            if (Order != null)
            {
                var frm = new frmOrderDetails(Order.OrderId);
                frm.StartPosition = FormStartPosition.Manual;
                frm.Location = new Point(this.MdiParent.Location.X, this.MdiParent.Location.Y + 45);
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    await LoadOrders();
                }
            }
        }

    }
}
