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
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
            BindingContext = new LoginViewModel();
        }

        private void RegisterLabel_Tapped(object sender, EventArgs e)
        {
            Application.Current.MainPage = new RegistrationPage();
        }
    }
}