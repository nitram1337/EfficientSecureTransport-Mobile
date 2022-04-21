using Xamarin.Forms;

using MobileApp.Views;
using MobileApp.Services.Credentials;
using MobileApp.Services.Identity;
using MobileApp.Services.RequestProvider;
using MobileApp.Services.OpenUrl;

namespace MobileApp
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();
            RegisterDependencies();

            // MonkeyCache
            MonkeyCache.FileStore.Barrel.ApplicationId = "XamarinForms-App";

            MainPage = new StartPage();
        }

        private void RegisterDependencies()
        {
            DependencyService.Register<ICredentialsService, CredentialsService>();
            DependencyService.Register<IIdentityService, IdentityService>();
            DependencyService.Register<IRequestProviderService, RequestProviderService>();
            DependencyService.Register<IOpenUrlService, OpenUrlService>();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}