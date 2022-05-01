using Xamarin.Forms;

using MobileApp.Models.Identity;
using MobileApp.Services.Credentials;
using MobileApp.Services.Identity;
using MobileApp.Views.Account;

namespace MobileApp.ViewModels
{
    public class StartViewModel : BaseViewModel
    {
        #region Services

        private readonly ICredentialsService _credentialsService;
        private readonly IIdentityService _identityService;

        #endregion Services

        #region Properties

        private IdentityResult _identityResult;

        #endregion Properties

        #region Constructor

        public StartViewModel()
        {
            _credentialsService = DependencyService.Get<ICredentialsService>();
            _identityService = DependencyService.Get<IIdentityService>();
        }

        #endregion Constructor

        public async void OnAppearing()
        {
            IsBusy = true;

            try
            {
                if (_credentialsService.IsAuthenticated)
                {
                    await _credentialsService.SetCredentialsFromSecureStorage();

                    _identityResult = await _identityService.GetUserInfoUpdatePreferenceAsync();
                    if (_identityResult.Succeeded)
                    {
                        RedirectAuthenticatedUser();
                    }
                }
                else
                {
                    RedirectUnauthenticatedUser();
                }
            }
            finally
            {
                IsBusy = false;
            }
        }

        void RedirectAuthenticatedUser()
        {
            switch (_credentialsService.IsAdmin)
            {
                case true:
                    Application.Current.MainPage = new AdminShell();
                    break;

                case false:
                    Application.Current.MainPage = new DriverShell();
                    break;
            }
        }

        void RedirectUnauthenticatedUser()
        {
            Application.Current.MainPage = new LoginPage();
        }
    }
}