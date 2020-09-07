using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace YogaMo.Mobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CheckoutPage : ContentPage
    {
        public CheckoutPage(int OrderId)
        {
            InitializeComponent();

            var htmlSource = new HtmlWebViewSource();
            htmlSource.BaseUrl = DependencyService.Get<IBaseUrl>().Get();

            string content = File.ReadAllText("Stripe_Payment.html"); ;
            content = content.Replace("{APIURL}", APIService.getApiURL());
            content = content.Replace("{ORDER_ID}", OrderId.ToString());

            htmlSource.Html = content;

            PaymentWebview.Source = htmlSource;

			PaymentWebview.Navigating += (s, e) =>
			{
				if (e.Url.Contains("StripePaymentSuccess.html"))
				{
                    DisplayAlert("Success", "Congratulations, your order is now being processed.", "OK");
                    Application.Current.MainPage = new MainPage();
					e.Cancel = true;
				}
			};
		}
    }
}