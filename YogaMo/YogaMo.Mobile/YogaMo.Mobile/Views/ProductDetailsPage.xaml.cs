using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using YogaMo.Mobile.ViewModels;

namespace YogaMo.Mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProductDetailsPage : ContentPage
    {
        private ProductDetailsViewModel VM;

        public ProductDetailsPage(int productId)
        {
            InitializeComponent();
            BindingContext = VM = new ProductDetailsViewModel(productId, Navigation);
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            await VM.Init();
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            if (VM.SizesEnabled && (VM.SelectedSize == null || VM.SelectedSize == "Size"))
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Please select the size of your product.", "OK");
                return;
            }

            if (!int.TryParse(VM.Quantity, out int QuantityInt) || QuantityInt < 1)
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Please enter a valid quantity.", "OK");
                return;
            }

            var request = new Model.Requests.OrderItemInsertRequest
            {
                ProductId = VM.Product.ProductId,
                Quantity = QuantityInt
            };
            if (VM.SizesEnabled)
                request.Size = VM.SelectedSize;

            var orderItem = await VM._serviceOrderItems.Insert<Model.OrderItem>(request);
            if (orderItem != null)
            {
                await Application.Current.MainPage.DisplayAlert("Success", "Item successfully added to cart", "OK");
                //var masterPage = this.Parent.Parent as MainPage;
                await Navigation.PopAsync();


                //foreach (var childPage in masterPage.Children)
                //{
                //    if(childPage.Title == "Cart")
                //    {
                //        masterPage.CurrentPage = childPage;
                //        break;
                //    }
                //}
            }

        }
    }
}