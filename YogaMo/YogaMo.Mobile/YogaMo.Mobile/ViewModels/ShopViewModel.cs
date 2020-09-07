using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace YogaMo.Mobile.ViewModels
{
    public class ShopViewModel : BaseViewModel
    {
        private readonly APIService _serviceCategories = new APIService("Categories");
        private readonly APIService _serviceProducts = new APIService("Products");

        public ShopViewModel()
        {
            Title = "Products";
        }

        public ObservableCollection<Model.Product> Products { get; set; } = new ObservableCollection<Model.Product>();
        public ObservableCollection<Model.Category> Categories { get; set; } = new ObservableCollection<Model.Category>();

        private Model.Category _selectedCategory;

        public Model.Category SelectedCategory
        {
            get { return _selectedCategory; }
            set { SetProperty(ref _selectedCategory, value); }
        }


        public async Task LoadCategories()
        {
            if (Categories.Count != 0)
                return;

            var list = await _serviceCategories.Get<List<Model.Category>>(null);
            foreach (var item in list)
            {
                Categories.Add(item);
            }
            if (Categories.Count != 0)
                SelectedCategory = Categories[0];
        }

        public async Task LoadProducts()
        {
            if (SelectedCategory == null)
                return;

            var request = new Model.Requests.ProductSearchRequest
            {
                CategoryId = SelectedCategory.CategoryId
            };

            var list = await _serviceProducts.Get<List<Model.Product>>(request);
            Products.Clear();
            foreach (var item in list)
            {
                if (item.Photo == null || item.Photo.Length == 0)
                {
                    item.Photo = File.ReadAllBytes("default_profile.png");
                }
                Products.Add(item);
            }
        }

        public async Task Init()
        {
            await LoadCategories();
            await LoadProducts();
        }
    }
}
