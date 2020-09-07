using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using YogaMo.Mobile.ViewModels;
using YogaMo.Model;

namespace YogaMo.Mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class YogaClassDetailsPage : ContentPage
    {
        private YogaClassDetailsViewModel VM;

        public YogaClassDetailsPage(Model.YogaClass yogaclass)
        {
            InitializeComponent();

            BindingContext = VM = new YogaClassDetailsViewModel(yogaclass, Navigation);
        }


    }
}