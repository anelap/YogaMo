using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using YogaMo.Mobile.Views;
using YogaMo.Model;

namespace YogaMo.Mobile.ViewModels
{
    public class YogaClassDetailsViewModel : BaseViewModel
    {
        private Model.YogaClass _yogaClass;
        private readonly INavigation navigation;

        public Model.YogaClass yogaClass
        {
            get { return _yogaClass; }
            set { SetProperty(ref _yogaClass, value); }
        }

        public ICommand CloseCommand { get; set; }

        public YogaClassDetailsViewModel(YogaClass yogaClass, INavigation navigation)
        {
            this.yogaClass = yogaClass;
            this.navigation = navigation;
            Title = yogaClass.Yoga.Name;

            CloseCommand = new Command(async () => await CloseForm());
        }

        private async Task CloseForm()
        {
            await navigation.PopAsync();
        }
    }
}
