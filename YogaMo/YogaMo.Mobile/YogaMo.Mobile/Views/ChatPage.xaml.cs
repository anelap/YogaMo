using YogaMo.Mobile.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace YogaMo.Mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ChatPage : ContentPage
    {

        private ChatViewModel model;

        public ChatPage(int instructorId)
        {
            InitializeComponent();
            BindingContext = model = new ChatViewModel(instructorId);
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            await model.Init();
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            model.SendMessage();
        }

        private void Entry_Completed(object sender, EventArgs e)
        {
            model.SendMessage();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            model.PN.RemoveListener(model.SubscribeListener);

            model.PN.Unsubscribe<string>().Channels(new string[] { model.channel_name }).Execute();
        }
    }
}