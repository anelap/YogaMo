using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using YogaMo.Mobile.Views;

namespace YogaMo.Mobile.ViewModels
{
    public class ProductDetailsViewModel : BaseViewModel
    {
        public readonly APIService _serviceProducts = new APIService("Products");
        public readonly APIService _serviceOrderItems = new APIService("OrderItems");
        private readonly int _productId;
        private readonly INavigation Navigation;

        public ProductDetailsViewModel(int ProductId, INavigation navigation)
        {
            Title = "Product details";
            _productId = ProductId;
            this.Navigation = navigation;
        }

 
        public ObservableCollection<string> Sizes { get; set; } = new ObservableCollection<string>();

        private string _selectedSize;

        public string SelectedSize
        {
            get { return _selectedSize; }
            set { SetProperty(ref _selectedSize, value); }
        }

        private Model.Product _product;

        public Model.Product Product
        {
            get { return _product; }
            set { SetProperty(ref _product, value); }
        }

        private bool _sizesEnabled = false;

        public bool SizesEnabled
        {
            get { return _sizesEnabled; }
            set { SetProperty(ref _sizesEnabled, value); }
        }

        private string _quantity = "1";

        public string Quantity
        {
            get { return _quantity; }
            set { SetProperty(ref _quantity, value); }
        }


        public async Task LoadProduct()
        {
            var TmpProduct = await _serviceProducts.GetById<Model.Product>(_productId);
            if (TmpProduct.Photo == null || TmpProduct.Photo.Length == 0)
            {
                TmpProduct.Photo = File.ReadAllBytes("default_profile.png");
            }
            Product = TmpProduct;
        }
        public void LoadSizes()
        {
            if (Product == null)
                return;

            if (Product.CategoryId == 1)
            {
                SizesEnabled = true;

                var list = new List<string> { "Size", "XS", "S", "M", "L", "XL", "XXL" };
                Sizes.Clear();
                foreach (var item in list)
                {
                    Sizes.Add(item);
                }
                SelectedSize = Sizes[0];
            }
        }
        public async Task Init()
        {
            await LoadProduct();
            LoadSizes();
        }
    }

}
