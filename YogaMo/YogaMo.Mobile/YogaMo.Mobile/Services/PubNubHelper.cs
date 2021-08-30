using PubnubApi;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Xamarin.Forms;

namespace YogaMo.Mobile.Services
{
    public static class PubNubHelper
    {
        private static Pubnub Pubnub { get; set; }

        public static Pubnub GetPubnub()
        {
            if (Pubnub == null) {
                PNConfiguration pnConfiguration = new PNConfiguration();

                pnConfiguration.SubscribeKey = "sub-c-f2a39c18-0779-11ec-ad72-221653618fb7";
                pnConfiguration.PublishKey = "pub-c-7b434a26-e372-45cc-9b3e-166ff64fa5c3";
                pnConfiguration.Uuid = APIService.CurrentUser.ClientId.ToString();
                Pubnub = new Pubnub(pnConfiguration);
            }
            return Pubnub;
        }


    }
}
