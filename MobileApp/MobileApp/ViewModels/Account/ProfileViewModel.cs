using System.Windows.Input;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Essentials;

using MobileApp.Models.Identity;
using MobileApp.Services.Credentials;
using MobileApp.Services.Identity;
using MobileApp.Views.Account;

namespace MobileApp.ViewModels.Account
{
    public class ProfileViewModel : BaseViewModel
    {
        #region Services

        private readonly ICredentialsService _credentialsService;
        private readonly IIdentityService _identityService;

        #endregion Services

        #region Commands

        public ICommand EditProfileCommand { get; }

        public ICommand LogoutCommand { get; }

        #endregion Commands

        #region Properties

        string fullname;
        public string Fullname
        {
            get { return fullname; }
            set { SetProperty(ref fullname, value); }
        }

        string email;
        public string Email
        {
            get { return email; }
            set { SetProperty(ref email, value); }
        }

        bool emailConfirmed;
        public bool EmailConfirmed
        {
            get { return emailConfirmed; }
            set { SetProperty(ref emailConfirmed, value); }
        }

        string role;
        public string Role
        {
            get { return role; }
            set { SetProperty(ref role, value); }
        }

        bool isAdmin;
        public bool IsAdmin
        {
            get { return isAdmin; }
            set { SetProperty(ref isAdmin, value); }
        }

        string externalProvider;
        public string ExternalProvider
        {
            get { return externalProvider; }
            set { SetProperty(ref externalProvider, value); }
        }

        bool isExternal;
        public bool IsExternal
        {
            get { return isExternal; }
            set { SetProperty(ref isExternal, value); }
        }

        private IdentityResult _identityResult;

        #endregion Properties

        #region Constructor

        public ProfileViewModel()
        {
            _credentialsService = DependencyService.Get<ICredentialsService>();
            _identityService = DependencyService.Get<IIdentityService>();

            EditProfileCommand = new Command(async () => await ExecuteEditProfileAsync());
            LogoutCommand = new Command(async () => await ExecuteLogoutAsync());

            IsConnected = Connectivity.NetworkAccess != NetworkAccess.Internet;
            IsButtonClickable = Connectivity.NetworkAccess == NetworkAccess.Internet;
            Connectivity.ConnectivityChanged += OnConnectivityChanged;
        }

        #endregion Constructor

        async Task ExecuteEditProfileAsync()
        {
            await Shell.Current.Navigation.PushModalAsync(new EditProfilePage());
        }

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

        public void OnAppearing()
        {
            IsBusy = true;

            try
            {
                Fullname = _credentialsService.Fullname;
                Email = _credentialsService.Email;
                EmailConfirmed = _credentialsService.EmailConfirmed;
                Role = _credentialsService.Role;
                IsAdmin = _credentialsService.IsAdmin;
                ExternalProvider = _credentialsService.ExternalProvider;
                IsExternal = _credentialsService.IsExternal;
            }
            finally
            {
                IsBusy = false;
            }
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