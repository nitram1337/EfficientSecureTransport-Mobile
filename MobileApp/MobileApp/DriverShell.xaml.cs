using System;

using MobileApp.Models.Identity;
using MobileApp.Services.Credentials;
using MobileApp.Services.Identity;
using MobileApp.Views.Account;

namespace MobileApp
{
    public partial class DriverShell : Xamarin.Forms.Shell
    {
        private readonly ICredentialsService _credentialsService;
        private readonly IIdentityService _identityService;

        private IdentityResult _identityResult;

        public DriverShell()
        {
            InitializeComponent();
            RegisterRoutes();

            _credentialsService = Xamarin.Forms.DependencyService.Get<ICredentialsService>();
            _identityService = Xamarin.Forms.DependencyService.Get<IIdentityService>();
        }

        private void RegisterRoutes()
        {
            Xamarin.Forms.Routing.RegisterRoute(nameof(ProfilePage), typeof(ProfilePage));
            Xamarin.Forms.Routing.RegisterRoute(nameof(EditProfilePage), typeof(EditProfilePage));
        }

        #region Events

        private async void OnSignoutClicked(object sender, EventArgs e)
        {
            try
            {
                if (_credentialsService.IsAuthenticated)
                {
                    _identityResult = await _identityService.LogoutAsync();
                    if (!_identityResult.Succeeded)
                    {
                        await DisplayAlert("Fejl", _identityResult.ErrorDescription, "OK");
                    }
                }
            }
            finally
            {
                if (_identityResult.Succeeded)
                {
                    await Current.GoToAsync("//LoginPage");
                }
            }
        }

        #endregion Events
    }
}