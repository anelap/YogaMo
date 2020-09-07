using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using YogaMo.Mobile.ViewModels;

namespace YogaMo.Mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ShopPage : ContentPage
    {
        private ShopViewModel VM;

        public ShopPage()
        {
            InitializeComponent();
            BindingContext = VM = new ShopViewModel();
        }

        private void ToolbarItem_Clicked(object sender, EventArgs e)
        {

            APIService.CurrentUser = null;
            APIService.Username = null;
            APIService.Password = null;

            SecureStorage.Remove("username");
            SecureStorage.Remove("password");
            Application.Current.MainPage = new LoginPage();
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            await VM.Init();
        }

        private async void CategoryPicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            await VM.LoadProducts();
        }

        private async void OnItemSelectedAsync(object sender, EventArgs e)
        {
            var element = sender as StackLayout;
            var product = element.BindingContext as Model.Product;

            await Navigation.PushAsync(new ProductDetailsPage(product.ProductId));
        }
    }
}