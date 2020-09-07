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
    public partial class InstructorsPage : ContentPage
    {
        private InstructorsViewModel VM;

        public InstructorsPage()
        {
            InitializeComponent();
            BindingContext = VM = new InstructorsViewModel();
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            await VM.Init();

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
    }
}