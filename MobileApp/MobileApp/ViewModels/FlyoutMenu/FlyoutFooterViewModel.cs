using System.Windows.Input;
using System.Threading.Tasks;
using Xamarin.Forms;

using MobileApp.Models.Identity;
using MobileApp.Services.Credentials;
using MobileApp.Services.Identity;

namespace MobileApp.ViewModels.FlyoutMenu
{
    public class FlyoutFooterViewModel : BaseViewModel
    {
        #region Services

        private readonly ICredentialsService _credentialsService;
        private readonly IIdentityService _identityService;

        #endregion Services

        #region Commands

        public ICommand LogoutCommand { get; }

        #endregion Commands

        #region Properties

        private IdentityResult _identityResult;

        #endregion Properties

        #region Constructor

        public FlyoutFooterViewModel()
        {
            _credentialsService = DependencyService.Get<ICredentialsService>();
            _identityService = DependencyService.Get<IIdentityService>();

            LogoutCommand = new Command(async () => await ExecuteLogoutAsync());
        }

        #endregion Constructor

        async Task ExecuteLogoutAsync()
        {
            IsButtonVisible = false;
            IsBusy = true;

            try
            {
                if (_credentialsService.IsAuthenticated)
                {
                    _identityResult = await _identityService.LogoutAsync();
                    if (!_identityResult.Succeeded)
                    {
                        await Application.Current.MainPage.DisplayAlert("Fejl", _identityResult.ErrorDescription, "OK");
                    }
                }
            }
            finally
            {
                if (_identityResult.Succeeded)
                {
                    IsButtonVisible = true;
                    await Shell.Current.GoToAsync("//LoginPage");
                }

                IsBusy = false;
            }
        }
    }
}