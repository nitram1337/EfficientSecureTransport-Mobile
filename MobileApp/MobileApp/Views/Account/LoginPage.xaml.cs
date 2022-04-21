using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using MobileApp.ViewModels.Account;

namespace MobileApp.Views.Account
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        LoginViewModel _viewModel;
        public LoginPage()
        {
            InitializeComponent();
            BindingContext = _viewModel = new LoginViewModel();
        }
    }
}