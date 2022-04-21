using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobileApp.Style
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Shell : ResourceDictionary
    {
        public Shell()
        {
            InitializeComponent();
        }
    }
}