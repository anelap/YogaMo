using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using YogaMo.Model;
using YogaMo.WinUI.Helper;

namespace YogaMo.WinUI.Products
{
    public partial class frmProductDetails : Form
    {
        private Product _product;
        private int _productId;

        private readonly APIService _serviceProducts = new APIService("Products");
        private readonly APIService _serviceCategories = new APIService("Categories");
        private Model.Requests.ProductInsertRequest request = new Model.Requests.ProductInsertRequest();

        public frmProductDetails(int ProductId = 0)
        {
            InitializeComponent();
            this._productId = ProductId;
        }

        private async void frmProductDetails_Load(object sender, EventArgs e)
        {
            await LoadCategories();
            await LoadProduct();
        }

        private async Task LoadCategories()
        {
            cmbCategory.DataSource = await _serviceCategories.Get<List<Model.Category>>();
            cmbCategory.DisplayMember = "Name";
        }

        private async Task LoadProduct()
        {
            if (_productId == 0) // inserting a new record 
            {
                pictureBox1.Image = Properties.Resources.default_profile;

                cbActive.Checked = true;
            }
            else // editing an existing product
            {
                _product = await _serviceProducts.GetById<Model.Product>(_productId);

                txtName.Text = _product.Name;
                txtPrice.Text = _product.Price.ToString();
                txtQuantity.Text = _product.QuantityStock.ToString();

                cbActive.Checked = _product.Status;

                if (_product.Photo != null && _product.Photo.Length > 0)
                {
                    var img = ImageHelper.ByteToImage(_product.Photo);
                    pictureBox1.Image = img;
                    request.Photo = _product.Photo;
                }
                else
                {
                    pictureBox1.Image = Properties.Resources.default_profile;
                }
            }
        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
            request.Name = txtName.Text;
            request.Price = decimal.Parse(txtPrice.Text);
            request.QuantityStock = int.Parse(txtQuantity.Text);
            request.Status = cbActive.Checked;
            request.CategoryId = (cmbCategory.SelectedItem as Model.Category).CategoryId;

            if (_productId == 0) // inserting
            {
                var product = await _serviceProducts.Insert<Model.Product>(request);
                if (product != null)
                {
                    MessageBox.Show("Product has been successfully created.", "Create product", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    DialogResult = DialogResult.OK;
                }
            }
            else // updating
            {
                var product = await _serviceProducts.Update<Model.Product>(_productId, request);
                if (product != null)
                {
                    MessageBox.Show("Product has been successfully updated.", "Update product", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                    request.Photo = file;
                    pictureBox1.Image = Image.FromFile(openFileDialog1.FileName);
                }
            }
        }
    }
}
