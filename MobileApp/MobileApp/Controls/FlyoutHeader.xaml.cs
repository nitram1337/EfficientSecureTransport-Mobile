using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using MobileApp.ViewModels.FlyoutMenu;

namespace MobileApp.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FlyoutHeader : ContentView
    {
        FlyoutHeaderViewModel _viewModel;
        public FlyoutHeader()
        {
            InitializeComponent();
            BindingContext = _viewModel = new FlyoutHeaderViewModel();
        }
    }
}