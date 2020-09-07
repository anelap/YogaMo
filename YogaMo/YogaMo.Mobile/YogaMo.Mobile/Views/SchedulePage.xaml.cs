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
    public partial class SchedulePage : ContentPage
    {
        private ScheduleViewModel VM;

        public SchedulePage()
        {
            InitializeComponent();
            BindingContext = VM = new ScheduleViewModel();
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

        private async void DayPicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            await VM.LoadSchedule();
        }

        private async void OnItemSelectedAsync(object sender, EventArgs e)
        {
            var element = sender as StackLayout;
            var yogaclass = element.BindingContext as Model.YogaClass;

            await Navigation.PushAsync(new YogaClassDetailsPage(yogaclass));
        }

    }
}