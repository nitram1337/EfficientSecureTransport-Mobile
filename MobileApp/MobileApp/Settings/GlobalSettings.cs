namespace MobileApp.Settings
{
    public class GlobalSettings
    {
        public static GlobalSettings Instance { get; set; } = new GlobalSettings();

        // TODO: Settings for Microservices (e.g. Uri, Port)
        // Identity and Api Gateway has the same Uri.
        #region Default values for Microservices and DataScheme for MobileApp

        private const string DefaultUri = "127.0.0.1";                   // Should match with the launchUrl from                 -> (any Microservice) / Properties / launchSettings.json - !! Without Port !!
        private const int DefaultApiGatewayPort = 7101;                  // Should match with the port of the launchUrl from     -> EST.API.Gateway    / Properties / launchsettings.json
        private const int DefaultIdentityPort = 7103;                    // Should match with the port of the launchUrl from     -> EST.API.IdentityMS / Properties / launchSettings.json
        
        public const string DefaultDataScheme = "com.mrsolutions.est";   // Should match with the Package name from              -> MobileApp.Android  / Properties / AndroidManifest /  package=""

        #endregion Default values for Microservices and DataScheme for MobileApp 

        private string _baseApiGatewayUri;
        private string _baseIdentityUri;
        private string _baseClientUri;

        public string BaseApiGatewayUri
        {
            get => _baseApiGatewayUri;
            set
            {
                _baseApiGatewayUri = value;
                UpdateApiGatewayEndpoint(_baseApiGatewayUri);
            }
        }

        public string BaseIdentityUri
        {
            get => _baseIdentityUri;
            set
            {
                _baseIdentityUri = value;
                UpdateIdentityEndpoint(_baseIdentityUri);
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
            BaseIdentityUri = $"https://{DefaultUri}:{DefaultIdentityPort}";
            BaseApiGatewayUri = $"https://{DefaultUri}:{DefaultApiGatewayPort}";
            BaseClientUri = DefaultDataScheme;
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


        private void UpdateApiGatewayEndpoint(string baseUri)
        {
            RouteApiEndpoint = $"{baseUri}/api/Routes";
        }

        private void UpdateIdentityEndpoint(string baseUri)
        {
            RegisterUri = $"{baseUri}/Account/Register";
            ChangePasswordUri = $"{baseUri}/Account/Manage/ChangePassword";
            ForgotPasswordUri = $"{baseUri}/Account/ForgotPassword";
        }

        private void UpdateClientEndpoint(string baseUri)
        {
            ClientUri = baseUri;
            RedirectUri = $"{baseUri}:/signin-oidc";                                                 // Should match with the RedirectUris from                 -> EST.API.IdentityMS / Config / Clients() { Xamarin Forms Client } 
            PostLogoutRedirectUri = $"{baseUri}:/signout-oidc";                                      // Should match with the PostLogoutRedirectUris from       -> EST.API.IdentityMS / Config / Clients() { Xamarin Forms Client } 
        }
    }
}