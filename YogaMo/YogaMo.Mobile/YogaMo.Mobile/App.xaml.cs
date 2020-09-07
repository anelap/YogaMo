using System;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using YogaMo.Mobile.Views;

namespace YogaMo.Mobile
{
    public partial class App : Application
    {
        public APIService _serviceClients = new APIService("Clients");
        public App()
        {
            InitializeComponent();

        }

        protected async override void OnStart()
        {

#pragma warning disable CS0612 // Type or member is obsolete
            try
            {
                string username = await SecureStorage.GetAsync("username");
                string password = await SecureStorage.GetAsync("password");

                if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password))
                {
                    APIService.Username = username;
                    APIService.Password = password;

                    try
                    {
                        APIService.CurrentUser = await _serviceClients.Get<Model.Client>(null, "MyProfile");

                        Current.MainPage = new MainPage();
                        return;
                    }
                    catch (Exception)
                    {
                        await Current.MainPage.DisplayAlert("Error", "Session has expired. Please log in again.", "OK");
                    }
                }

            }
            catch (Exception)
            {
                // Possible that device doesn't support secure storage on device.
            }

            Current.MainPage = new LoginPage();
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
