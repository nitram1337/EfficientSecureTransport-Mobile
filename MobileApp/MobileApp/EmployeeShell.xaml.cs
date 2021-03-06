using MobileApp.Views.Account;
using MobileApp.Views.Route;

namespace MobileApp
{
    public partial class EmployeeShell : Xamarin.Forms.Shell
    {
        public EmployeeShell()
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
        }
    }
}