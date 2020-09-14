using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Flurl.Http;
using Flurl;
using YogaMo.Model;
using System.Windows.Forms;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace YogaMo.WinUI
{
    public class APIService
    {
        public static string Username { get; set; }
        public static string Password { get; set; }
        public static Model.Instructor CurrentInstructor { get; set; }

        private string _route = null;
        public APIService(string route)
        {
            _route = route;
        }

        public async Task<T> Get<T>(object search = null, string action = null)
        {
            var url = $"{Properties.Settings.Default.APIUrl}/{_route}";
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
                    MessageBox.Show("You are not logged in.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                if (ex.Call.HttpStatus == System.Net.HttpStatusCode.Forbidden)
                {
                    MessageBox.Show("You are not authorized", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return default(T);
                }
                throw;
            }
        }

        public static async Task<Model.Instructor> GetCurrentInstructor()
        {
            if (CurrentInstructor != null)
                return CurrentInstructor;

            if (!string.IsNullOrEmpty(APIService.Username) && !string.IsNullOrEmpty(APIService.Password))
            {
                APIService _service = new APIService("Instructors");
                CurrentInstructor = await _service.Get<Model.Instructor>(null, "MyProfile");

                if (CurrentInstructor != null)
                {
                    return CurrentInstructor;
                }
            }

            MessageBox.Show("Failure", "Failed to retrieve user profile.", MessageBoxButtons.OK, MessageBoxIcon.Error);

            return default;
        }

        public async Task<T> GetById<T>(object id, string action = null)
        {
            var url = $"{Properties.Settings.Default.APIUrl}/{_route}";
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
                    MessageBox.Show("You are not logged in.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                if (ex.Call.HttpStatus == System.Net.HttpStatusCode.Forbidden)
                {
                    MessageBox.Show("You are not authorized", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return default(T);
                }
                throw;
            }
        }

        public async Task<T> Insert<T>(object request, string action = null)
        {
            var url = $"{Properties.Settings.Default.APIUrl}/{ _route}";
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
                    MessageBox.Show("You are not logged in.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    throw;
                }
                if (ex.Call.HttpStatus == System.Net.HttpStatusCode.Forbidden)
                {
                    MessageBox.Show("You are not authorized", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return default(T);
                }

                var response = await ex.GetResponseJsonAsync<ValidationProblemDetails>();

                var stringBuilder = new StringBuilder();
                foreach (var error in response.Errors)
                {
                    stringBuilder.AppendLine(string.Join(",", error.Value));
                }

                MessageBox.Show(stringBuilder.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                var url = $"{Properties.Settings.Default.APIUrl}/{ _route}";
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
                    MessageBox.Show("You are not logged in.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    throw;
                }
                if (ex.Call.HttpStatus == System.Net.HttpStatusCode.Forbidden)
                {
                    MessageBox.Show("You are not authorized", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return default(T);
                }

                var response = await ex.GetResponseJsonAsync<ValidationProblemDetails>();

                var stringBuilder = new StringBuilder();
                if(response.Errors.Count > 0)
                {
                    foreach (var error in response.Errors)
                    {
                        stringBuilder.AppendLine(string.Join(",", error.Value));
                    }
                }
                else
                {
                    foreach (KeyValuePair<string, object> kvpair in response.Extensions)
                    {
                        if(kvpair.Key == "ERROR")
                        {
                            var jobject = kvpair.Value as JObject;
                            foreach (JProperty jprop in jobject.Properties())
                            {
                                if(jprop.Name == "errors")
                                {
                                    foreach (JToken jtoken1 in jprop.Value)
                                    {
                                        string message = jtoken1.Value<string>("errorMessage");
                                        stringBuilder.AppendLine(message);
                                    }
                                }
                            }
                        }
                    }
                }

                MessageBox.Show(stringBuilder.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return default(T);
            }

        }
        public async Task<T> Delete<T>(int id)
        {
            var url = $"{Properties.Settings.Default.APIUrl}/{_route}/{id}";

            try
            {
                return await url.WithBasicAuth(Username, Password).DeleteAsync().ReceiveJson<T>();
            }
            catch (FlurlHttpException ex)
            {
                if (ex.Call.HttpStatus == System.Net.HttpStatusCode.Unauthorized)
                {
                    MessageBox.Show("You are not logged in.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                if (ex.Call.HttpStatus == System.Net.HttpStatusCode.Forbidden)
                {
                    MessageBox.Show("You are not authorized", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                return default(T);
            }
        }

    }
}
