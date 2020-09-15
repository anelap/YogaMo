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
    public partial class frmOrderDetails : Form
    {
        private readonly APIService _serviceOrders = new APIService("Orders");
        private readonly APIService _serviceOrderItems = new APIService("OrderItems");
        private readonly int _orderId;
        private Order Order;

        public frmOrderDetails(int OrderId)
        {
            InitializeComponent();
            _orderId = OrderId;
        }

        private async Task LoadOrder()
        {
            Order = await _serviceOrders.GetById<Model.Order>(_orderId);
            if (Order == null)
            {
                MessageBox.Show("Order not found.", "Error", MessageBoxButtons.OK);
                Close();
            }

            if (Order.OrderStatus == OrderStatus.Confirmed)
            {
                btnComplete.Enabled = true;
                btnCancel.Enabled = true;
            }
            else if (Order.OrderStatus == OrderStatus.Processing)
            {
                btnCancel.Enabled = true;
            }
            lblTotal.Text = Order.TotalPrice.ToString("0.00");
        }
        private async Task LoadOrderDetails()
        {
            var request = new OrderItemSearchRequest()
            {
                OrderId = _orderId
            };

            var result = await _serviceOrderItems.Get<List<Model.OrderItem>>(request);
            dgvOrderItems.AutoGenerateColumns = false;
            dgvOrderItems.DataSource = result;
        }

        private async void frmOrderDetails_Load(object sender, EventArgs e)
        {
            await LoadOrder();
            await LoadOrderDetails().ConfigureAwait(false);
        }

        private async void btnComplete_Click(object sender, EventArgs e)
        {
            var request = new Model.Requests.OrderInsertRequest
            {
                ClientId = Order.ClientId,
                Date = Order.Date,
                TotalPrice = Order.TotalPrice,
                OrderStatus = OrderStatus.Completed
            };
            var updated = await _serviceOrders.Update<Model.Order>(_orderId, request);
            if(updated != null)
            {
                DialogResult = DialogResult.OK;
            }
        }

        private async void btnCancel_Click(object sender, EventArgs e)
        {
            var request = new Model.Requests.OrderInsertRequest
            {
                ClientId = Order.ClientId,
                Date = Order.Date,
                TotalPrice = Order.TotalPrice,
                OrderStatus = OrderStatus.Canceled
            };
            var updated = await _serviceOrders.Update<Model.Order>(_orderId, request);
            if (updated != null)
            {
                DialogResult = DialogResult.OK;
            }
        }
    }
}
