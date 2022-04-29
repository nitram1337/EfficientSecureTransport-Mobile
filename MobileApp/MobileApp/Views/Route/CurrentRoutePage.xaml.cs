using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using MobileApp.ViewModels.Route;

namespace MobileApp.Views.Route
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CurrentRoutePage : ContentPage
    {
        CurrentRouteViewModel _viewModel;
        public CurrentRoutePage()
        {
            InitializeComponent();
            BindingContext = _viewModel = new CurrentRouteViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }
    }
}