using MobileApp.Models.Identity;
using MobileApp.Services.Credentials;
using MobileApp.Services.Identity;
using MobileApp.Views.Account;
using MobileApp.Views.Route;

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
            Xamarin.Forms.Routing.RegisterRoute(nameof(CurrentRoutePage), typeof(CurrentRoutePage));
            Xamarin.Forms.Routing.RegisterRoute(nameof(ListRoutesPage), typeof(ListRoutesPage));
            Xamarin.Forms.Routing.RegisterRoute(nameof(ProfilePage), typeof(ProfilePage));
            Xamarin.Forms.Routing.RegisterRoute(nameof(EditProfilePage), typeof(EditProfilePage));
        }
    }
}