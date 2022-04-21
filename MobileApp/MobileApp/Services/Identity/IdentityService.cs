using System;
using System.Net.Http;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Essentials;
using IdentityModel.OidcClient;
using IdentityModel.OidcClient.Results;

using MobileApp.MethodExtensions;
using MobileApp.Services.Browser;
using MobileApp.Services.Credentials;
using MobileApp.Settings;
using MobileApp.Models.Identity;

namespace MobileApp.Services.Identity
{
    public class IdentityService : IIdentityService
    {
        private OidcClient _oidcClient;
        private readonly ICredentialsService _credentialsService;

        public IdentityService()
        {
            _oidcClient = CreateOidcClient();
            _credentialsService = DependencyService.Get<ICredentialsService>();
        }

        public async Task<IdentityResult> LoginAsync()
        {
            try
            {
                LoginResult loginResult = await _oidcClient.LoginAsync(new LoginRequest());
                if (loginResult.User != null)
                {
                    SetSecureStorageValues(loginResult.AccessToken, loginResult.IdentityToken, loginResult.RefreshToken, loginResult.AccessTokenExpiration.ToString());

                    return loginResult.SetProfileCredentials(_credentialsService);
                }

                return new IdentityResult { Aborted = true };
            }
            catch (Exception ex)
            {
                return new IdentityResult { ErrorDescription = ex.Message };
            }

            //catch (HttpRequestExceptionEx ex) when (ex.HttpCode == System.Net.HttpStatusCode.InternalServerError)
            //{
            //    return new IdentityResult { ErrorDescription = ex.Message };
            //}
        }

        public async Task<IdentityResult> LogoutAsync()
        {
            try
            {
                LogoutResult logoutResult = await _oidcClient.LogoutAsync(new LogoutRequest { IdTokenHint = _credentialsService.IdentityToken });
                if (logoutResult.IsError)
                {
                    return new IdentityResult { ErrorDescription = logoutResult.Error };
                }

                _credentialsService.ClearProfileCredentials();
                RemoveSecureStorageValues();

                return new IdentityResult();
            }
            catch
            {
                return new IdentityResult { ErrorDescription = "Server kan ikke nåes i øjeblikket. Prøv igen senere." };
            }
        }

        public async Task<IdentityResult> RefreshTokenAsync()
        {
            try
            {
                RefreshTokenResult refreshTokenResult = await _oidcClient.RefreshTokenAsync(_credentialsService.RefreshToken);
                if (refreshTokenResult.IsError)
                {
                    return new IdentityResult { ErrorDescription = refreshTokenResult.Error };
                }

                SetSecureStorageValues(
                    refreshTokenResult.AccessToken,
                    refreshTokenResult.AccessTokenExpiration.ToString(),
                    refreshTokenResult.IdentityToken,
                    refreshTokenResult.RefreshToken);

                return new IdentityResult();
            }
            catch
            {
                return new IdentityResult { ErrorDescription = "Server kan ikke nåes i øjeblikket. Prøv igen senere!" };
            }
        }

        public async Task<IdentityResult> GetUserInfoUpdatePreferenceAsync()
        {
            try
            {
                UserInfoResult userInfoResult = await _oidcClient.GetUserInfoAsync(_credentialsService.AccessToken);
                if (userInfoResult.IsError)
                {
                    return new IdentityResult { ErrorDescription = userInfoResult.Error };
                }

                if (userInfoResult.Claims != null)
                {
                    return userInfoResult.SetProfileCredentials(_credentialsService);

                }

                return new IdentityResult { ErrorDescription = "Kunne ikke får brugens oplysninger." };
            }
            catch
            {
                return new IdentityResult { ErrorDescription = "Server kan ikke nåes i øjeblikket. Prøv igen senere!" };
            }
        }

        #region Helper

        private OidcClient CreateOidcClient()
        {
            return new OidcClient(
                new OidcClientOptions
                {
                    Authority = GlobalSettings.Instance.BaseIdentityUri,
                    ClientId = GlobalSettings.Instance.ClientId,
                    ClientSecret = GlobalSettings.Instance.ClientSecret,
                    RedirectUri = GlobalSettings.Instance.RedirectUri,
                    PostLogoutRedirectUri = GlobalSettings.Instance.PostLogoutRedirectUri,
                    Scope = GlobalSettings.Instance.Scope,
                    Flow = OidcClientOptions.AuthenticationFlow.Hybrid,

                    Browser = new BrowserService(),

                    // TODO: Not recommended for production. Remove this line and then use the networks settings file from resources.
                    // Add this line android:networkSecurityConfig="@xml/network_security_config" to the Android Manifest.
                    BackchannelHandler = new HttpClientHandler() { ServerCertificateCustomValidationCallback = (message, certificate, chain, sslPolicyErrors) => true }
                });
        }

        private async void SetSecureStorageValues(string accessToken, string identityToken, string refreshToken, string accessTokenExpiration)
        {
            await SecureStorage.SetAsync("access_token", accessToken);
            await SecureStorage.SetAsync("identity_token", identityToken);
            await SecureStorage.SetAsync("refresh_token", refreshToken);
            await SecureStorage.SetAsync("access_token_expiration", accessTokenExpiration);

            await _credentialsService.SetCredentialsFromSecureStorage();
        }

        private void RemoveSecureStorageValues()
        {
            SecureStorage.RemoveAll();
        }

        #endregion Helper
    }
}
