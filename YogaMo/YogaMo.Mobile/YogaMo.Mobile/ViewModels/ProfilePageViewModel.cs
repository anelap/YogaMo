using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;


namespace YogaMo.Mobile.ViewModels
{
    public class ProfilePageViewModel : BaseViewModel
    {
        private readonly APIService _serviceClients = new APIService("Clients");

        private int _userId;
        private Model.Client _user;
        public Model.Client User
        {
            get { return _user; }
            set { SetProperty(ref _user, value); }
        }
        public int UserId { get; set; }


        public ProfilePageViewModel()
        {
        }

        public async Task Init()
        {
            _userId = (await APIService.GetCurrentUser()).ClientId;
            await LoadUser();
        }

        private async Task LoadUser()
        {
            var TmpUser = await _serviceClients.GetById<Model.Client>(_userId);
            if (TmpUser.ProfilePicture == null || TmpUser.ProfilePicture.Length == 0)
            {
                TmpUser.ProfilePicture = File.ReadAllBytes("default_profile.png");
            }

            Title = "User Profile - " + TmpUser.Username;

            User = TmpUser;
 
        }


    }
}
