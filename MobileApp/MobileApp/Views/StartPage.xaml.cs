using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using MobileApp.ViewModels;

namespace MobileApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StartPage : ContentPage
    {
        StartViewModel _viewModel;
        public StartPage()
        {
            InitializeComponent();
            BindingContext = _viewModel = new StartViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }
    }
}