using Flurl.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using YogaMo.Model;

namespace YogaMo.Mobile
{
    public class APIService
    {
        public static string Username { get; set; }
        public static string Password { get; set; }
        public static Model.Client CurrentUser { get; set; }

        private string APIUrl;
        private readonly string _route;
        public APIService(string route)
        {
            _route = route;
            APIUrl = getApiURL();
        }

        public static string getApiURL()
        {
            int port = 60997;
            string local = $"http://localhost:{port}/api";

            return local;
        }


        public async Task<T> Get<T>(object search, string action = null)
        {
            var url = $"{APIUrl}/{_route}";
            try
            {
                if (action != null)
                {
                    url += "/" + action;
                }

                if (search != null)
                {
                    url += "?";
                    url += await search.ToQueryString();
                }

                return await url.WithBasicAuth(Username, Password).GetJsonAsync<T>();
            }
            catch (FlurlHttpException ex)
            {
                if (ex.Call.HttpStatus == System.Net.HttpStatusCode.Unauthorized)
                {
                    await Application.Current.MainPage.DisplayAlert("Error", "You are not logged in.", "OK");
                }
                if (ex.Call.HttpStatus == System.Net.HttpStatusCode.Forbidden)
                {
                    await Application.Current.MainPage.DisplayAlert("Error", "You are not authorized", "OK");
                    return default(T);
                }
                throw;
            }
        }

        public static async Task<Model.Client> GetCurrentUser()
        {
            if (CurrentUser != null)
                return CurrentUser;

            if (!string.IsNullOrEmpty(APIService.Username) && !string.IsNullOrEmpty(APIService.Password))
            {
                APIService _service = new APIService("Clients");
                CurrentUser = await _service.Get<Model.Client>(null, "MyProfile");

                if (CurrentUser != null)
                {
                    return CurrentUser;
                }
            }

            await Application.Current.MainPage.DisplayAlert("Failure", "Failed to retrieve user profile.", "OK");

            return default;
        }

        public async Task<T> GetById<T>(object id, string action = null)
        {
            var url = $"{APIUrl}/{_route}";
            try
            {
                if (action != null)
                {
                    url += $"/{action}";
                }
                url += $"/{id}";
                return await url.WithBasicAuth(Username, Password).GetJsonAsync<T>();
            }
            catch (FlurlHttpException ex)
            {
                if (ex.Call.HttpStatus == System.Net.HttpStatusCode.Unauthorized)
                {
                    await Application.Current.MainPage.DisplayAlert("Error", "You are not logged in.", "OK");
                }
                if (ex.Call.HttpStatus == System.Net.HttpStatusCode.Forbidden)
                {
                    await Application.Current.MainPage.DisplayAlert("Error", "You are not authorized", "OK");
                    return default(T);
                }
                throw;
            }
        }

        public async Task<T> Insert<T>(object request, string action = null)
        {
            var url = $"{APIUrl}/{ _route}";
            if (action != null)
            {
                url += $"/{action}";
            }
            try
            {
                return await url.WithBasicAuth(Username, Password).PostJsonAsync(request).ReceiveJson<T>();
            }
            catch (FlurlHttpException ex)
            {
                if (ex.Call.HttpStatus == System.Net.HttpStatusCode.Unauthorized)
                {
                    await Application.Current.MainPage.DisplayAlert("Error", "You are not logged in.", "OK");
                    throw;
                }
                if (ex.Call.HttpStatus == System.Net.HttpStatusCode.Forbidden)
                {
                    await Application.Current.MainPage.DisplayAlert("Error", "You are not authorized", "OK");
                    return default(T);
                }

                var response = await ex.GetResponseJsonAsync<ValidationProblemDetails>();

                var stringBuilder = new StringBuilder();
                foreach (var error in response.Errors)
                {
                    stringBuilder.AppendLine(string.Join(",", error.Value));
                }

                await Application.Current.MainPage.DisplayAlert("Error", stringBuilder.ToString(), "OK");
                return default(T);
            }
            catch (Exception)
            {
                return default(T);

            }

        }

        public async Task<T> Update<T>(int id, object request, string action = null)
        {
            try
            {
                var url = $"{APIUrl}/{ _route}";
                if (action != null)
                {
                    url += $"/{action}";
                }
                url += $"/{id}";

                return await url.WithBasicAuth(Username, Password).PutJsonAsync(request).ReceiveJson<T>();
            }
            catch (FlurlHttpException ex)
            {
                if (ex.Call.HttpStatus == System.Net.HttpStatusCode.Unauthorized)
                {
                    await Application.Current.MainPage.DisplayAlert("Error", "You are not logged in.", "OK");
                    throw;
                }
                if (ex.Call.HttpStatus == System.Net.HttpStatusCode.Forbidden)
                {
                    await Application.Current.MainPage.DisplayAlert("Error", "You are not authorized", "OK");
                    return default(T);
                }

                var response = await ex.GetResponseJsonAsync<ValidationProblemDetails>();

                var stringBuilder = new StringBuilder();
                foreach (var error in response.Errors)
                {
                    stringBuilder.AppendLine(string.Join(",", error.Value));
                }

                await Application.Current.MainPage.DisplayAlert("Error", stringBuilder.ToString(), "OK");
                return default(T);
            }

        }
        public async Task<T> Delete<T>(int id)
        {
            var url = $"{APIUrl}/{_route}/{id}";

            try
            {
                return await url.WithBasicAuth(Username, Password).DeleteAsync().ReceiveJson<T>();
            }
            catch (FlurlHttpException ex)
            {
                if (ex.Call.HttpStatus == System.Net.HttpStatusCode.Unauthorized)
                {
                    await Application.Current.MainPage.DisplayAlert("Error", "You are not logged in.", "OK");
                }
                if (ex.Call.HttpStatus == System.Net.HttpStatusCode.Forbidden)
                {
                    await Application.Current.MainPage.DisplayAlert("Error", "You are not authorized", "OK");
                }
                return default(T);
            }
        }

    }
}
