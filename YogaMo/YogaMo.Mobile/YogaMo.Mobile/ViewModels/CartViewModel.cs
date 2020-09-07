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
    public class CartViewModel : BaseViewModel
    {
        private readonly APIService _serviceOrderItems = new APIService("OrderItems");
        private readonly APIService _serviceOrders = new APIService("Orders");
        private readonly INavigation Navigation;

        public CartViewModel(INavigation navigation)
        {
            Title = "Cart Items";
            CheckoutCommand = new Command(async () => await GoToCheckout());
            EmptyCartCommand = new Command(async () => await EmptyCart());
            this.Navigation = navigation;
        }

        private async Task EmptyCart()
        {
            if (Order == null)
                return;

            var deletedOrder = await _serviceOrders.Delete<Model.Order>(Order.OrderId);
            if (deletedOrder != null)
            {
                await Application.Current.MainPage.DisplayAlert("Success", "Your cart has been emptied.", "OK");
                await Init();
            }
        }

        private async Task GoToCheckout()
        {
            if (Order == null)
                return;

            var request = new Model.Requests.OrderInsertRequest
            {
                OrderStatus = Model.OrderStatus.Processing,
                Date = Order.Date,
                ClientId = Order.ClientId,
                TotalPrice = Order.TotalPrice
            };

            var updatedOrder = await _serviceOrders.Update<Model.Order>(Order.OrderId, request);
            if (updatedOrder != null)
            {
                await Navigation.PushAsync(new CheckoutPage(Order.OrderId));
            }
        }

        public ICommand CheckoutCommand { get; set; }
        public ICommand EmptyCartCommand { get; private set; }

        public ObservableCollection<Model.OrderItem> CartItems { get; set; } = new ObservableCollection<Model.OrderItem>();
        private Model.Order _order;
        public Model.Order Order
        {
            get { return _order; }
            set { SetProperty(ref _order, value); }
        }

        private bool _nothingToShow;
        public bool NothingToShow
        {
            get { return _nothingToShow; }
            set { SetProperty(ref _nothingToShow, value); }
        }
        private bool _somethingToShow = true;

        public bool SomethingToShow
        {
            get { return _somethingToShow; }
            set { SetProperty(ref _somethingToShow, value); }
        }


        private string _totalPrice = "$0.00";
        public string TotalPrice
        {
            get { return _totalPrice; }
            set { SetProperty(ref _totalPrice, value); }
        }



        public async Task LoadOrder()
        {
            var request = new Model.Requests.OrderSearchRequest
            {
                OrderStatus = Model.OrderStatus.New
            };

            var list = await _serviceOrders.Get<List<Model.Order>>(request);
            if (list.Count == 0)
            {
                NothingToShow = true;
                SomethingToShow = false;
                TotalPrice = "$0.00";
                Order = null;
            }
            else
            {
                NothingToShow = false;
                SomethingToShow = true;
                Order = list[0];
                TotalPrice = "$" + Order.TotalPrice.ToString("0.00");
            }
        }

        private async Task LoadCartItems()
        {
            if (Order == null)
                return;

            var request = new Model.Requests.OrderItemSearchRequest
            {
                OrderId = Order.OrderId
            };
            var list = await _serviceOrderItems.Get<List<Model.OrderItem>>(request);
            CartItems.Clear();
            foreach (var item in list)
            {
                if (item.Product.Photo == null || item.Product.Photo.Length == 0)
                {
                    item.Product.Photo = File.ReadAllBytes("default_profile.png");
                }
                CartItems.Add(item);
            }

            if (CartItems.Count == 0)
            {
                NothingToShow = true;
                SomethingToShow = false;
            }
            else
            {
                NothingToShow = false;
                SomethingToShow = true;
            }
        }

        public async Task Init()
        {
            await LoadOrder();
            await LoadCartItems();
        }

    }

}
