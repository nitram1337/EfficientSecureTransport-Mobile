namespace MobileApp.Settings
{
    public class GlobalSettings
    {
        public static GlobalSettings Instance { get; set; } = new GlobalSettings();

        // TODO: Settings for Identity, Client and Api (e.g. Uri, Port)
        // Identity and Api has the same Uri because they are running on local machine.
        #region Default values for Identity, Api and App DataScheme

        private const string DefaultIdentityUri = "127.0.0.1";                  // Should match with the launchUrl from                 -> EST.API.Identity / Properties / launchSettings.json
        private const int DefaultIdentityPort = 7102;                           // Should match with the port of the launchUrl from     -> EST.API.Identity / Properties / launchSettings.json
        private const int DefaultApiPort = 6001;                                // Should match with the port of the launchUrl from     -> Api              / Properties / launchsettings.json


        public const string DefaultClientUri = "com.companyname.est";           // Should match with the ClientUri from                 -> EST.API.Identity / appsettings.json / "Clients": { }  "XamarinForms": { } 

        #endregion Default values for Identity, Api and App DataScheme 

        private string _baseIdentityUri;
        private string _baseApiUri;
        private string _baseClientUri;

        public string BaseIdentityUri
        {
            get => _baseIdentityUri;
            set
            {
                _baseIdentityUri = value;
                UpdateIdentityEndpoint(_baseIdentityUri);
            }
        }

        public string BaseApiUri
        {
            get => _baseApiUri;
            set
            {
                _baseApiUri = value;
                UpdateApiEndpoint(_baseApiUri);
            }
        }

        public string BaseClientUri
        {
            get => _baseClientUri;
            set
            {
                _baseClientUri = value;
                UpdateClientEndpoint(_baseClientUri);
            }
        }

        public GlobalSettings()
        {
            BaseIdentityUri = $"https://{DefaultIdentityUri}:{DefaultIdentityPort}";
            BaseApiUri = $"https://{DefaultIdentityUri}:{DefaultApiPort}";
            BaseClientUri = DefaultClientUri;
        }

        public string ClientId { get { return "EST.MobileApp.XamarinForms"; } }
        public string ClientSecret { get { return "af874cd0-6cf6-4882-a460-e2890ebce7cd"; } }
        public string ClientUri { get; set; }
        public string Scope { get { return "openid profile email offline_access api1"; } }          // TODO: Add or remove client scopes here.
        public string RedirectUri { get; set; }                                                     // Where to redirect to after login.
        public string PostLogoutRedirectUri { get; set; }                                           // Where to redirect to after logout.


        public string RegisterUri { get; set; }
        public string ChangePasswordUri { get; set; }
        public string ForgotPasswordUri { get; set; }


        public string RouteApiEndpoint { get; set; }
        public string UserApiEndpoint { get; set; }


        private void UpdateIdentityEndpoint(string baseUri)
        {
            RegisterUri = $"{baseUri}/Account/Register";
            ChangePasswordUri = $"{baseUri}/Account/Manage/ChangePassword";
            ForgotPasswordUri = $"{baseUri}/Account/ForgotPassword";
        }

        private void UpdateApiEndpoint(string baseUri)
        {
            RouteApiEndpoint = $"{baseUri}/api/route";
            UserApiEndpoint = $"{baseUri}/api/user";
        }

        private void UpdateClientEndpoint(string baseUri)
        {
            ClientUri = baseUri;
            RedirectUri = $"{baseUri}:/signin-oidc";
            PostLogoutRedirectUri = $"{baseUri}:/signout-oidc";
        }
    }
}