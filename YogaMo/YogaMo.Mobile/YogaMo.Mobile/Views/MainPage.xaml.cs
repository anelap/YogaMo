using System;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using YogaMo.Mobile.ViewModels;

namespace YogaMo.Mobile.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : TabbedPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        //override protected void OnCurrentPageChanged()
        //{
        //    if (this.CurrentPage.Title == "Cart")
        //    {
        //        var page = ((CartPage)this.CurrentPage);
        //        var VM = page.BindingContext as CartViewModel;
        //        if (VM != null)
        //        {
        //            VM.Init();
        //        }
        //    }

        //}
    }
}