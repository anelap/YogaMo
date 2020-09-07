using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace YogaMo.Mobile.ViewModels
{
    public class EditProfilePageViewModel : BaseViewModel
    {
        private readonly APIService _serviceClients = new APIService("Clients");
        private readonly APIService _serviceCities = new APIService("Cities");
        private readonly APIService _serviceCountries = new APIService("Countries");

        public ObservableCollection<Model.Country> Countries { get; set; } = new ObservableCollection<Model.Country>();
        public ObservableCollection<Model.City> Cities { get; set; } = new ObservableCollection<Model.City>();

        private int _userId;

        private Model.Requests.ClientUpdateRequest _user;

        public Model.Requests.ClientUpdateRequest User
        {
            get { return _user; }
            set { SetProperty(ref _user, value); }
        }

        public ICommand SaveProfileCommand { get; set; }


        private Model.Country _country;
        public Model.Country Country
        {
            get { return _country; }
            set { SetProperty(ref _country, value); }
        }

        private Model.City _city;
        public Model.City City
        {
            get { return _city; }
            set { SetProperty(ref _city, value); }
        }



        public EditProfilePageViewModel()
        {
            SaveProfileCommand = new Command(async () => await SaveProfile());
        }

        public async Task Init()
        {
            _userId = (await APIService.GetCurrentUser()).ClientId;
            await LoadUser();
        }


        private async Task LoadUser()
        {
            if (User == null)
            {
                var TmpUser = await _serviceClients.Get<Model.Requests.ClientUpdateRequest>(null, "MyProfile");

                if (TmpUser.ProfilePicture == null || TmpUser.ProfilePicture.Length == 0)
                {
                    TmpUser.ProfilePicture = File.ReadAllBytes("default_profile.png");
                }
                Title = "Edit Profile - " + TmpUser.Username;

                User = TmpUser;

                await LoadUserCity();
            }

        }
        private async Task LoadUserCity()
        {
            if(User.CityId != null)
            {
                var UserCity = await _serviceCities.GetById<Model.City>(User.CityId);
                if (UserCity != null)
                {
                    await LoadCountries(UserCity.CountryId);
                    return;
                }
            }

            await LoadCountries();

        }

        private async Task LoadCountries(int? countryId = null)
        {
            var list = await _serviceCountries.Get<List<Model.Country>>(null);
            Countries.Clear();
            foreach (var item in list)
            {
                Countries.Add(item);
            }

            if(countryId.HasValue)
            {
                foreach (var item in Countries)
                {
                    if (item.CountryId == countryId.Value)
                    {
                        Country = item;
                        await LoadCities();
                        break;
                    }
                }
            }
        }

        public async Task LoadCities()
        {
            if (Country == null)
                return;

            var request = new Model.Requests.CitiesSearchRequest
            {
                CountryId = Country.CountryId
            };
            var list = await _serviceCities.Get<List<Model.City>>(request);
            Cities.Clear();
            foreach (var item in list)
            {
                Cities.Add(item);
            }

            if(User.CityId != null)
            {
                foreach (var item in Cities)
                {
                    if(item.CityId == User.CityId.Value)
                    {
                        City = item;
                        break;
                    }
                }
            }
        }

        private async Task SaveProfile()
        {
            if (City != null)
            {
                User.CityId = City.CityId;
            }
            var entity = await _serviceClients.Update<Model.Client>((await APIService.GetCurrentUser()).ClientId, User);
            if (entity != null)
            {

                APIService.Username = User.Username;
                if (!string.IsNullOrWhiteSpace(User.Password))
                {
                    APIService.Password = User.Password;
                }

                await SecureStorage.SetAsync("username", APIService.Username);
                await SecureStorage.SetAsync("password", APIService.Password);

                await Application.Current.MainPage.DisplayAlert("Success", "The profile was successfully saved.", "OK");
            }
        }


    }
}
