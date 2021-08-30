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
            BindingContext = VM = new InstructorsViewModel(Navigation);
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            await VM.Init();

        }

    }
}