using System.Windows.Input;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Essentials;

using MobileApp.Services.Credentials;
using MobileApp.Services.Identity;
using MobileApp.Services.OpenUrl;
using MobileApp.Models.Identity;
using MobileApp.Settings;

namespace MobileApp.ViewModels.Account
{
    public class LoginViewModel : BaseViewModel
    {
        #region Services

        private readonly ICredentialsService _credentialsService;
        private readonly IIdentityService _identityService;
        private readonly IOpenUrlService _openUrlService;

        #endregion Services

        #region Commands

        public ICommand LoginCommand { get; set; }

        public ICommand RegisterCommand { get; set; }

        #endregion Commands

        #region Properties

        private IdentityResult _identityResult;

        #endregion Properties

        #region Constructor

        public LoginViewModel()
        {
            _credentialsService = DependencyService.Get<ICredentialsService>();
            _identityService = DependencyService.Get<IIdentityService>();
            _openUrlService = DependencyService.Get<IOpenUrlService>();

            LoginCommand = new Command(async () => await ExecuteLoginAsync());
            RegisterCommand = new Command(async () => await ExecuteRegisterAsync());

            IsConnected = Connectivity.NetworkAccess != NetworkAccess.Internet;
            IsButtonClickable = Connectivity.NetworkAccess == NetworkAccess.Internet;
            Connectivity.ConnectivityChanged += OnConnectivityChanged;
        }

        #endregion Constructor

        async Task ExecuteLoginAsync()
        {
            IsButtonVisible = false;
            IsBusy = true;

            try
            {
                _identityResult = await _identityService.LoginAsync();
                if (!_identityResult.Succeeded && !_identityResult.Aborted)
                {
                    await Application.Current.MainPage.DisplayAlert("Fejl", _identityResult.ErrorDescription, "OK");
                }
            }
            finally
            {
                if (_identityResult.Succeeded)
                {
                    switch (_credentialsService.IsAdmin)
                    {
                        case true:
                            Application.Current.MainPage = new AdminShell();
                            break;

                        case false:
                            Application.Current.MainPage = new EmployeeShell();
                            break;
                    }
                }
                else if (!_identityResult.Succeeded || _identityResult.Aborted)
                {
                    IsButtonVisible = true;
                }

                IsBusy = false;
            }
        }

        async Task ExecuteRegisterAsync()
        {
            await _openUrlService.OpenUrl($"{GlobalSettings.Instance.RegisterUri}");
        }

        #region Events

        private void OnConnectivityChanged(object sender, ConnectivityChangedEventArgs e)
        {
            IsConnected = e.NetworkAccess != NetworkAccess.Internet;
            IsButtonClickable = e.NetworkAccess == NetworkAccess.Internet;
        }

        #endregion Events
    }
}