using MobileApp.Views.Account;
using MobileApp.Views.Route;
using MobileApp.Views.Admin;

namespace MobileApp
{
    public partial class AdminShell : Xamarin.Forms.Shell
    {
        public AdminShell()
        {
            InitializeComponent();
            RegisterRoutes();
        }

        private void RegisterRoutes()
        {
            Xamarin.Forms.Routing.RegisterRoute(nameof(CurrentRoutePage), typeof(CurrentRoutePage));
            Xamarin.Forms.Routing.RegisterRoute(nameof(ListRoutesPage), typeof(ListRoutesPage));
            Xamarin.Forms.Routing.RegisterRoute(nameof(ProfilePage), typeof(ProfilePage));
            Xamarin.Forms.Routing.RegisterRoute(nameof(EditProfilePage), typeof(EditProfilePage));
            Xamarin.Forms.Routing.RegisterRoute(nameof(AdminPage), typeof(AdminPage));
        }
    }
}