using System.Threading.Tasks;
using Xamarin.Essentials;

namespace MobileApp.Services.Credentials
{
    public class CredentialsService : ICredentialsService
    {
        public string Firstname
        {
            get => Preferences.Get("first_name", string.Empty);
            set => Preferences.Set("first_name", value);
        }

        public string Lastname
        {
            get => Preferences.Get("last_name", string.Empty);
            set => Preferences.Set("last_name", value);
        }

        public string Fullname
        {
            get => Preferences.Get("full_name", string.Empty);
            set => Preferences.Set("full_name", value);
        }

        public string Email
        {
            get => Preferences.Get("email", string.Empty);
            set => Preferences.Set("email", value);
        }

        public bool EmailConfirmed
        {
            get => Preferences.Get("email_confirmed", false);
            set => Preferences.Set("email_confirmed", value);
        }

        public string Role
        {
            get => Preferences.Get("role", string.Empty);
            set => Preferences.Set("role", value);
        }

        public bool IsAdmin
        {
            get => Preferences.Get("is_admin", false);
            set => Preferences.Set("is_admin", value);
        }

        public bool IsAuthenticated
        {
            get => Preferences.Get("is_authenticated", false);
            set => Preferences.Set("is_authenticated", value);
        }

        public string ExternalProvider
        {
            get => Preferences.Get("external_provider", string.Empty);
            set => Preferences.Set("external_provider", value);
        }

        public bool IsExternal
        {
            get => Preferences.Get("is_external", false);
            set => Preferences.Set("is_external", value);
        }



        public string AccessToken { get; set; }

        public string IdentityToken { get; set; }

        public string RefreshToken { get; set; }

        public string AccessTokenExpiration { get; set; }



        public async Task SetCredentialsFromSecureStorage()
        {
            AccessToken = await SecureStorage.GetAsync("access_token");
            IdentityToken = await SecureStorage.GetAsync("identity_token");
            RefreshToken = await SecureStorage.GetAsync("refresh_token");
            AccessTokenExpiration = await SecureStorage.GetAsync("access_token_expiration");
        }

        public void ClearProfileCredentials()
        {
            Firstname = string.Empty;
            Lastname = string.Empty;
            Fullname = string.Empty;
            Email = string.Empty;
            EmailConfirmed = false;
            Role = string.Empty;
            IsAdmin = false;
            IsAuthenticated = false;
            ExternalProvider = string.Empty;
            IsExternal = false;
        }
    }
}