using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using YogaMo.Mobile.Models;
using YogaMo.Mobile.Views;
using YogaMo.Model;

namespace YogaMo.Mobile.ViewModels
{
    public class ProductDetailsViewModel : BaseViewModel
    {
        public readonly APIService _serviceProducts = new APIService("Products");
        public readonly APIService _serviceOrderItems = new APIService("OrderItems");
        private readonly APIService _serviceRatings = new APIService("Ratings");
        private readonly APIService _serviceRecommender = new APIService("Recommender");
        private readonly int _productId;
        private readonly INavigation Navigation;

        public ProductDetailsViewModel(int ProductId, INavigation navigation)
        {
            Title = "Product details";
            _productId = ProductId;
            this.Navigation = navigation;

            InitializeStars();

            RateStarCommand = new Command<string>(async (Rating) => await RateStar(Rating));
            GoToProductCommand = new Command<Model.Product>(async (Product) => await GoToProduct(Product));
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

        #region Recommended product definitions
        private Model.Product _recommendedProduct1;
        public Model.Product RecommendedProduct1
        {
            get { return _recommendedProduct1; }
            set { SetProperty(ref _recommendedProduct1, value); }
        }

        private Model.Product _recommendedProduct2;
        public Model.Product RecommendedProduct2
        {
            get { return _recommendedProduct2; }
            set { SetProperty(ref _recommendedProduct2, value); }
        }

        private Model.Product _recommendedProduct3;
        public Model.Product RecommendedProduct3
        {
            get { return _recommendedProduct3; }
            set { SetProperty(ref _recommendedProduct3, value); }
        }
        #endregion

        #region Recommended product ratings

        private Star _recommendedProduct1Star1;
        public Star RecommendedProduct1Star1
        {
            get { return _recommendedProduct1Star1; }
            set { SetProperty(ref _recommendedProduct1Star1, value); }
        }
        private Star _recommendedProduct1Star2;
        public Star RecommendedProduct1Star2
        {
            get { return _recommendedProduct1Star2; }
            set { SetProperty(ref _recommendedProduct1Star2, value); }
        }
        private Star _recommendedProduct1Star3;
        public Star RecommendedProduct1Star3
        {
            get { return _recommendedProduct1Star3; }
            set { SetProperty(ref _recommendedProduct1Star3, value); }
        }
        private Star _recommendedProduct1Star4;
        public Star RecommendedProduct1Star4
        {
            get { return _recommendedProduct1Star4; }
            set { SetProperty(ref _recommendedProduct1Star4, value); }
        }
        private Star _recommendedProduct1Star5;
        public Star RecommendedProduct1Star5
        {
            get { return _recommendedProduct1Star5; }
            set { SetProperty(ref _recommendedProduct1Star5, value); }
        }


        private Star _recommendedProduct2Star1;
        public Star RecommendedProduct2Star1
        {
            get { return _recommendedProduct2Star1; }
            set { SetProperty(ref _recommendedProduct2Star1, value); }
        }
        private Star _recommendedProduct2Star2;
        public Star RecommendedProduct2Star2
        {
            get { return _recommendedProduct2Star2; }
            set { SetProperty(ref _recommendedProduct2Star2, value); }
        }
        private Star _recommendedProduct2Star3;
        public Star RecommendedProduct2Star3
        {
            get { return _recommendedProduct2Star3; }
            set { SetProperty(ref _recommendedProduct2Star3, value); }
        }
        private Star _recommendedProduct2Star4;
        public Star RecommendedProduct2Star4
        {
            get { return _recommendedProduct2Star4; }
            set { SetProperty(ref _recommendedProduct2Star4, value); }
        }
        private Star _recommendedProduct2Star5;
        public Star RecommendedProduct2Star5
        {
            get { return _recommendedProduct2Star5; }
            set { SetProperty(ref _recommendedProduct2Star5, value); }
        }
        private Star _recommendedProduct3Star1;
        public Star RecommendedProduct3Star1
        {
            get { return _recommendedProduct3Star1; }
            set { SetProperty(ref _recommendedProduct3Star1, value); }
        }
        private Star _recommendedProduct3Star2;
        public Star RecommendedProduct3Star2
        {
            get { return _recommendedProduct3Star2; }
            set { SetProperty(ref _recommendedProduct3Star2, value); }
        }
        private Star _recommendedProduct3Star3;
        public Star RecommendedProduct3Star3
        {
            get { return _recommendedProduct3Star3; }
            set { SetProperty(ref _recommendedProduct3Star3, value); }
        }
        private Star _recommendedProduct3Star4;
        public Star RecommendedProduct3Star4
        {
            get { return _recommendedProduct3Star4; }
            set { SetProperty(ref _recommendedProduct3Star4, value); }
        }
        private Star _recommendedProduct3Star5;
        public Star RecommendedProduct3Star5
        {
            get { return _recommendedProduct3Star5; }
            set { SetProperty(ref _recommendedProduct3Star5, value); }
        }
        #endregion

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


        public ICommand RateStarCommand { get; set; }
        public ICommand GoToProductCommand { get; set; }

        public int Rating { get; set; }

        #region stars

        private Star _star1;
        public Star Star1
        {
            get { return _star1; }
            set { SetProperty(ref _star1, value); }
        }
        private Star _star2;
        public Star Star2
        {
            get { return _star2; }
            set { SetProperty(ref _star2, value); }
        }
        private Star _star3;
        public Star Star3
        {
            get { return _star3; }
            set { SetProperty(ref _star3, value); }
        }
        private Star _star4;
        public Star Star4
        {
            get { return _star4; }
            set { SetProperty(ref _star4, value); }
        }
        private Star _star5;
        private List<Product> RecommendedProducts;

        public Star Star5
        {
            get { return _star5; }
            set { SetProperty(ref _star5, value); }
        }

        #endregion

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
            await LoadExistingRating();
            UpdateRatingStars();
            await LoadRecommendations();
        }

        private async Task LoadRecommendations()
        {
            RecommendedProducts = await _serviceRecommender.GetById<List<Model.Product>>(_productId);
            if (RecommendedProducts.Count > 0)
            {
                if (RecommendedProducts.Count == 3)
                {
                    if (RecommendedProducts[2].Photo == null || RecommendedProducts[2].Photo.Length == 0)
                    {
                        RecommendedProducts[2].Photo = File.ReadAllBytes("default_profile.png");
                    }

                    RecommendedProduct3 = RecommendedProducts[2];
                    ThreeProductsShown = true;
                }
                else
                    RecommendedProduct3 = RecommendedProducts[0];

                if (RecommendedProducts.Count >= 2)
                {
                    if (RecommendedProducts[1].Photo == null || RecommendedProducts[1].Photo.Length == 0)
                    {
                        RecommendedProducts[1].Photo = File.ReadAllBytes("default_profile.png");
                    }
                    RecommendedProduct2 = RecommendedProducts[1];
                    TwoProductsShown = true;
                }
                else
                    RecommendedProduct2 = RecommendedProducts[0];

                if (RecommendedProducts[0].Photo == null || RecommendedProducts[0].Photo.Length == 0)
                {
                    RecommendedProducts[0].Photo = File.ReadAllBytes("default_profile.png");
                }
                RecommendedProduct1 = RecommendedProducts[0];

                UpdateRecommendationeStars();

            }

        }

        private bool _threeProductsShown = false;

        public bool ThreeProductsShown
        {
            get { return _threeProductsShown; }
            set { SetProperty(ref _threeProductsShown, value); }
        }

        private bool _twoProductsShown = false;

        public bool TwoProductsShown
        {
            get { return _twoProductsShown; }
            set { SetProperty(ref _twoProductsShown, value); }
        }


        private void UpdateRecommendationeStars()
        {
            var Star_Empty = new Star { Image = "star_empty.png" };
            var Star_Filled = new Star { Image = "star_filled.png" };
            var Star_Half = new Star { Image = "star_half.png" };

            RecommendedProduct1Star1 =
            RecommendedProduct1Star2 =
            RecommendedProduct1Star3 =
            RecommendedProduct1Star4 =
            RecommendedProduct1Star5 =
            RecommendedProduct2Star1 =
            RecommendedProduct2Star2 =
            RecommendedProduct2Star3 =
            RecommendedProduct2Star4 =
            RecommendedProduct2Star5 =
            RecommendedProduct3Star1 =
            RecommendedProduct3Star2 =
            RecommendedProduct3Star3 =
            RecommendedProduct3Star4 =
            RecommendedProduct3Star5 = Star_Empty;

            if (RecommendedProduct1.AverageRating >= 4.25)
            {
                if (RecommendedProduct1.AverageRating >= 4.75)
                    RecommendedProduct1Star5 = Star_Filled;
                else
                    RecommendedProduct1Star5 = Star_Half;
            }
            if (RecommendedProduct1.AverageRating >= 3.25)
            {
                if (RecommendedProduct1.AverageRating >= 3.75)
                    RecommendedProduct1Star4 = Star_Filled;
                else
                    RecommendedProduct1Star4 = Star_Half;
            }
            if (RecommendedProduct1.AverageRating >= 2.25)
            {
                if (RecommendedProduct1.AverageRating >= 2.75)
                    RecommendedProduct1Star3 = Star_Filled;
                else
                    RecommendedProduct1Star3 = Star_Half;
            }
            if (RecommendedProduct1.AverageRating >= 1.50)
            {
                if (RecommendedProduct1.AverageRating >= 1.75)
                    RecommendedProduct1Star2 = Star_Filled;
                else
                    RecommendedProduct1Star2 = Star_Half;
            }
            if (RecommendedProduct1.AverageRating >= 1.00)
            {
                RecommendedProduct1Star1 = Star_Filled;
            }

            if (RecommendedProduct2.AverageRating >= 4.25)
            {
                if (RecommendedProduct2.AverageRating >= 4.75)
                    RecommendedProduct2Star5 = Star_Filled;
                else
                    RecommendedProduct2Star5 = Star_Half;
            }
            if (RecommendedProduct2.AverageRating >= 3.25)
            {
                if (RecommendedProduct2.AverageRating >= 3.75)
                    RecommendedProduct2Star4 = Star_Filled;
                else
                    RecommendedProduct2Star4 = Star_Half;
            }
            if (RecommendedProduct2.AverageRating >= 2.25)
            {
                if (RecommendedProduct2.AverageRating >= 2.75)
                    RecommendedProduct2Star3 = Star_Filled;
                else
                    RecommendedProduct2Star3 = Star_Half;
            }
            if (RecommendedProduct2.AverageRating >= 1.50)
            {
                if (RecommendedProduct2.AverageRating >= 1.75)
                    RecommendedProduct2Star2 = Star_Filled;
                else
                    RecommendedProduct2Star2 = Star_Half;
            }
            if (RecommendedProduct2.AverageRating >= 1.00)
            {
                RecommendedProduct2Star1 = Star_Filled;
            }

            if (RecommendedProduct3.AverageRating >= 4.25)
            {
                if (RecommendedProduct3.AverageRating >= 4.75)
                    RecommendedProduct3Star5 = Star_Filled;
                else
                    RecommendedProduct3Star5 = Star_Half;
            }
            if (RecommendedProduct3.AverageRating >= 3.25)
            {
                if (RecommendedProduct3.AverageRating >= 3.75)
                    RecommendedProduct3Star4 = Star_Filled;
                else
                    RecommendedProduct3Star4 = Star_Half;
            }
            if (RecommendedProduct3.AverageRating >= 2.25)
            {
                if (RecommendedProduct3.AverageRating >= 2.75)
                    RecommendedProduct3Star3 = Star_Filled;
                else
                    RecommendedProduct3Star3 = Star_Half;
            }
            if (RecommendedProduct3.AverageRating >= 1.50)
            {
                if (RecommendedProduct3.AverageRating >= 1.75)
                    RecommendedProduct3Star2 = Star_Filled;
                else
                    RecommendedProduct3Star2 = Star_Half;
            }
            if (RecommendedProduct3.AverageRating >= 1.00)
            {
                RecommendedProduct3Star1 = Star_Filled;
            }

        }


        #region star rating
        private async Task LoadExistingRating()
        {
            var request = new Model.Requests.RatingSearchRequest
            {
                ProductId = _productId
            };
            var ExistingRating = await _serviceRatings.Get<List<Model.Rating>>(request);
            if (ExistingRating.Count > 0)
            {
                Rating = ExistingRating[0].Rating1;
            }
        }
        private void UpdateRatingStars()
        {
            var Star_Empty = new Star { Image = "star_empty.png" };
            var Star_Filled = new Star { Image = "star_filled.png" };

            Star1 = Star_Empty;
            Star2 = Star_Empty;
            Star3 = Star_Empty;
            Star4 = Star_Empty;
            Star5 = Star_Empty;

            if (Rating >= 1)
                Star1 = Star_Filled;
            if (Rating >= 2)
                Star2 = Star_Filled;
            if (Rating >= 3)
                Star3 = Star_Filled;
            if (Rating >= 4)
                Star4 = Star_Filled;
            if (Rating == 5)
                Star5 = Star_Filled;
        }


        private async Task RateStar(string NewRating)
        {
            int RatingNum = int.TryParse(NewRating, out int value) ? value : 0;
            if (RatingNum >= 1 && RatingNum <= 5)
            {

                var request = new Model.Requests.RatingInsertRequest
                {
                    ProductId = _productId,
                    Rating = RatingNum
                };

                Rating = RatingNum;

                UpdateRatingStars();

                await _serviceRatings.Insert<Model.Rating>(request, "RateProduct");

            }
        }

        private void InitializeStars()
        {
            Star1 = new Star();
            Star2 = new Star();
            Star3 = new Star();
            Star4 = new Star();
            Star5 = new Star();
        }
        #endregion
        private async Task GoToProduct(Product product)
        {
            await Navigation.PushAsync(new ProductDetailsPage(product.ProductId));
        }
    }

}
