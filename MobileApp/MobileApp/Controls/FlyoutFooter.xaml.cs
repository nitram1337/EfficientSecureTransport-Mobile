using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using MobileApp.ViewModels.FlyoutMenu;

namespace MobileApp.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FlyoutFooter : ContentView
    {
        FlyoutFooterViewModel _viewModel;

        public FlyoutFooter()
        {
            InitializeComponent();
            BindingContext = _viewModel = new FlyoutFooterViewModel();
        }
    }
}