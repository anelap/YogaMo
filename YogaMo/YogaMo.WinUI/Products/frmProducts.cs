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

namespace YogaMo.WinUI.Products
{
    public partial class frmProducts : Form
    {
        private readonly APIService _serviceProducts = new APIService("Products");
        private readonly APIService _serviceCategories = new APIService("Categories");
        public frmProducts()
        {
            InitializeComponent();
        }

        private async Task LoadProducts()
        {
            var request = new ProductSearchRequest()
            {
                Name = txtSearchName.Text
            };
            if(cmbCategory.SelectedItem != null)
            {
                request.CategoryId = (cmbCategory.SelectedItem as Model.Category).CategoryId;
            }

            var result = await _serviceProducts.Get<List<Model.Product>>(request);
            foreach (var product in result)
            {
                if(product.Photo == null || product.Photo.Length == 0)
                {
                    product.Photo = ImageHelper.ImageToByte(Properties.Resources.default_profile);
                }
            }
            dgvProducts.AutoGenerateColumns = false;
            dgvProducts.DataSource = result;
        }

        private async void frmProducts_Load(object sender, EventArgs e)
        {
            await LoadCategories();
            await LoadProducts().ConfigureAwait(false);
        }

        private async Task LoadCategories()
        {
            var list = await _serviceCategories.Get<List<Model.Category>>(null);
            list.Insert(0, new Model.Category
            {
                CategoryId = 0,
                Name = "All"
            });
            cmbCategory.DataSource = list;
            cmbCategory.DisplayMember = "Name";
        }

        private async void txtSearchName_TextChanged(object sender, EventArgs e)
        {
            await LoadProducts();
        }

        private async void cmbCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            await LoadProducts();
        }
        private async void dgvProducts_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            var Product = dgvProducts.SelectedRows[0].DataBoundItem as Model.Product;
            if(Product != null)
            {
                var frm = new frmProductDetails(Product.ProductId);
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    await LoadProducts();
                }
            }
        }

        private async void btnNewProduct_Click(object sender, EventArgs e)
        {
            frmProductDetails frm = new frmProductDetails();
            if (frm.ShowDialog() == DialogResult.OK)
                await LoadProducts();
        }

    }
}
