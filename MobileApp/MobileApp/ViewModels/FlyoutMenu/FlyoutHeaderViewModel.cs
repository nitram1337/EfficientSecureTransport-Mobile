using Xamarin.Forms;

using MobileApp.Services.Credentials;

namespace MobileApp.ViewModels.FlyoutMenu
{
    public class FlyoutHeaderViewModel : BaseViewModel
    {
        #region Services

        private readonly ICredentialsService _credentialsService;

        #endregion Services

        #region Properties

        string fullname;
        public string Fullname
        {
            get { return fullname; }
            set { SetProperty(ref fullname, value); }
        }

        #endregion Properties

        #region Constructor

        public FlyoutHeaderViewModel()
        {
            _credentialsService = DependencyService.Get<ICredentialsService>();

            // Calling OnAppearing method here because the ContentView doesn't support base.OnAppearing().
            OnAppearing();
        }

        #endregion Constructor

        public void OnAppearing()
        {
            Fullname = _credentialsService.Fullname;
        }
    }
}