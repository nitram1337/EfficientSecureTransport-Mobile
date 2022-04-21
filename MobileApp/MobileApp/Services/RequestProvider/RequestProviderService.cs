using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Globalization;
using Xamarin.Forms;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json.Converters;

using MobileApp.Models.Identity;
using MobileApp.Exceptions;
using MobileApp.Services.Credentials;
using MobileApp.Services.Identity;

namespace MobileApp.Services.RequestProvider
{
    public class RequestProviderService : IRequestProviderService
    {
        private HttpClient _httpClient;
        private readonly ICredentialsService _credentialsService;
        private readonly IIdentityService _identityService;
        private readonly JsonSerializerSettings _serializerSettings;

        private IdentityResult _identityResult;

        public RequestProviderService()
        {
            _httpClient = new HttpClient(new HttpClientHandler() { ServerCertificateCustomValidationCallback = (message, certificate, chain, sslPolicyErrors) => true });

            _credentialsService = DependencyService.Get<ICredentialsService>();
            _identityService = DependencyService.Get<IIdentityService>();

            _serializerSettings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                DateTimeZoneHandling = DateTimeZoneHandling.Utc,
                NullValueHandling = NullValueHandling.Ignore
            };

            _serializerSettings.Converters.Add(new StringEnumConverter());
        }

        #region GET

        public async Task<TResult> GetAsync<TResult>(string uri)
        {
            await HandleRefreshToken();
            ConfigureHttpClient(_credentialsService.AccessToken);

            HttpResponseMessage response = await _httpClient.GetAsync(uri);

            await HandleResponse(response);
            string serialized = await response.Content.ReadAsStringAsync();

            TResult result = await Task.Run(() =>
                JsonConvert.DeserializeObject<TResult>(serialized, _serializerSettings));

            return result;
        }

        #endregion GET

        #region POST

        public async Task<TResult> PostAsync<TResult>(string uri, TResult data, string header = "")
        {
            await HandleRefreshToken();
            ConfigureHttpClient(_credentialsService.AccessToken);

            if (!string.IsNullOrEmpty(header))
            {
                AddHeaderParameter(_httpClient, header);
            }

            var content = new StringContent(JsonConvert.SerializeObject(data));
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            HttpResponseMessage response = await _httpClient.PostAsync(uri, content);

            await HandleResponse(response);
            string serialized = await response.Content.ReadAsStringAsync();

            TResult result = await Task.Run(() =>
                JsonConvert.DeserializeObject<TResult>(serialized, _serializerSettings));

            return result;
        }

        public async Task<TResult> PostAsync<TInput, TResult>(string uri, TInput data, string header = "")
        {
            await HandleRefreshToken();
            ConfigureHttpClient(_credentialsService.AccessToken);


            if (!string.IsNullOrEmpty(header))
            {
                AddHeaderParameter(_httpClient, header);
            }

            var content = new StringContent(JsonConvert.SerializeObject(data));
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            HttpResponseMessage response = await _httpClient.PostAsync(uri, content);

            await HandleResponse(response);
            string serialized = await response.Content.ReadAsStringAsync();

            TResult result = await Task.Run(() =>
                JsonConvert.DeserializeObject<TResult>(serialized, _serializerSettings));

            return result;
        }

        #endregion POST

        #region PUT

        public async Task<TResult> PutAsync<TResult>(string uri, TResult data, string header = "")
        {
            await HandleRefreshToken();
            ConfigureHttpClient(_credentialsService.AccessToken);

            if (!string.IsNullOrEmpty(header))
            {
                AddHeaderParameter(_httpClient, header);
            }

            var content = new StringContent(JsonConvert.SerializeObject(data));
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            HttpResponseMessage response = await _httpClient.PutAsync(uri, content);

            await HandleResponse(response);
            string serialized = await response.Content.ReadAsStringAsync();

            TResult result = await Task.Run(() =>
                JsonConvert.DeserializeObject<TResult>(serialized, _serializerSettings));

            return result;
        }

        #endregion PUT

        #region DELETE

        public async Task DeleteAsync(string uri)
        {
            await HandleRefreshToken();
            ConfigureHttpClient(_credentialsService.AccessToken);

            await _httpClient.DeleteAsync(uri);
        }

        #endregion DELETE

        #region Helper

        private void ConfigureHttpClient(string accessToken)
        {
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            if (!string.IsNullOrEmpty(accessToken))
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            }
            else
            {
                _httpClient.DefaultRequestHeaders.Authorization = null;
            }
        }

        private void AddHeaderParameter(HttpClient httpClient, string parameter)
        {
            if (httpClient == null)
                return;

            if (string.IsNullOrEmpty(parameter))
                return;

            _httpClient.DefaultRequestHeaders.Add(parameter, Guid.NewGuid().ToString());
        }

        private async Task HandleResponse(HttpResponseMessage response)
        {
            if (!response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                if (response.StatusCode == HttpStatusCode.Forbidden ||
                    response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    throw new ServiceAuthenticationException(content);
                }

                throw new HttpRequestExceptionEx(response.StatusCode, content);
            }
        }

        private async Task HandleRefreshToken()
        {
            CultureInfo culture = new CultureInfo("en-US");
            CultureInfo culture2 = new CultureInfo("en-US");

            DateTime accessTokenExpiration = Convert.ToDateTime(_credentialsService.AccessTokenExpiration, culture);
            DateTime dateTimeNow = Convert.ToDateTime(DateTime.Now, culture2);
            if (accessTokenExpiration < dateTimeNow)
            {
                _identityResult = await _identityService.RefreshTokenAsync();
                if (!_identityResult.Succeeded)
                {
                    try
                    {
                        await _identityService.LogoutAsync();
                    }
                    finally
                    {
                        await Shell.Current.GoToAsync("//LoginPage");
                    }
                }
            }
        }

        #endregion Helper

    }
}