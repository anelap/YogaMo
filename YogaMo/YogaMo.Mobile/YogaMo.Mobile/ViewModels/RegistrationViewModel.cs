using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;
using YogaMo.Mobile.Views;

namespace YogaMo.Mobile.ViewModels
{
    public class RegistrationViewModel : BaseViewModel
    {
        private readonly APIService _serviceClients = new APIService("Clients");
        private readonly APIService _serviceCities = new APIService("Cities");
        private readonly APIService _serviceCountries = new APIService("Countries");

        public ObservableCollection<Model.Country> Countries { get; set; } = new ObservableCollection<Model.Country>();
        public ObservableCollection<Model.City> Cities { get; set; } = new ObservableCollection<Model.City>();



        public RegistrationViewModel()
        {
            RegistrationCommand = new Command(async () => await Register());
            Title = "Registration";
            _user = new Model.Requests.ClientInsertRequest();
        }

        private bool _isButtonEnabled = true;



        public async Task Init()
        {
            LoadCountries();
        }

        private async void LoadCountries()
        {
            var list = await _serviceCountries.Get<List<Model.Country>>(null);
            Countries.Clear();
            foreach (var item in list)
            {
                Countries.Add(item);
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
        }

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



        public bool IsButtonEnabled
        {
            get { return _isButtonEnabled; }
            set { SetProperty(ref _isButtonEnabled, value); }
        }

        private Model.Requests.ClientInsertRequest _user;
        public Model.Requests.ClientInsertRequest User
        {
            get { return _user; }
            set { SetProperty(ref _user, value); }
        }


        public ICommand RegistrationCommand { get; set; }

        async Task Register()
        {
            if (Country == null)
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Please select a country.", "OK");
                return;
            }

            if (City == null)
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Please select a city.", "OK");
                return;
            }

            IsButtonEnabled = false;

            try
            {
                User.CityId = City.CityId;

                var entity = await _serviceClients.Insert<Model.Client>(User);
                if (entity == null)
                    throw new Exception();

                APIService.Username = User.Username;
                APIService.Password = User.Password;
                APIService.CurrentUser = await _serviceClients.Get<Model.Client>(null, "MyProfile");

                await SecureStorage.SetAsync("username", APIService.Username);
                await SecureStorage.SetAsync("password", APIService.Password);

#pragma warning disable CS0612 // Type or member is obsolete
                Application.Current.MainPage = new MainPage();
#pragma warning restore CS0612 // Type or member is obsolete
            }
            catch (Exception)
            {

            }
            finally
            {
                IsButtonEnabled = true;
            }
        }
    }
}
