using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using YogaMo.Mobile.UWP;

[assembly: Dependency(typeof(BaseUrl))]
namespace YogaMo.Mobile.UWP
{
    public class BaseUrl : IBaseUrl
    {
        public string Get()
        {
            return "ms-appx-web:///";
        }
    }
}
