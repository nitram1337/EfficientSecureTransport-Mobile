using System.Threading.Tasks;
using Xamarin.Essentials;

namespace MobileApp.Services.OpenUrl
{
    public class OpenUrlService : IOpenUrlService
    {
        // The Launcher class enables an application to open a URI by the system.
        // This is often used when deep linking into another application's custom URI schemes.
        // If you are looking to open the browser to a website then you should refer to the Browser API.

        public async Task OpenUrl(string url)
        {
            if (await Launcher.CanOpenAsync(url))
            {
                await Launcher.OpenAsync(url);
            }
        }
    }
}