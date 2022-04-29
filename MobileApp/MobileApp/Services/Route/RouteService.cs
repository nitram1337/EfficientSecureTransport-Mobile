using System.Threading.Tasks;
using System.Collections.ObjectModel;
using Xamarin.Forms;

using MobileApp.Models.Route;
using MobileApp.Helpers;
using MobileApp.Services.RequestProvider;
using MobileApp.Settings;

namespace MobileApp.Services.Route
{
    public class RouteService : IRouteService
    {
        private readonly IRequestProviderService _requestProviderService;

        public RouteService()
        {
            _requestProviderService = DependencyService.Get<IRequestProviderService>();
        }

        public Task<ObservableCollection<RouteForList>> GetRoutesAsync()
        {
            throw new System.NotImplementedException();
        }

        public async Task<bool> UpdateDriverLocationByCoordinates(RouteCoordinate data)
        {
            var uri = UriHelper.CombineUri(GlobalSettings.Instance.RouteApiEndpoint, "/update/driverlocation");

            return await _requestProviderService.PostAsync<RouteCoordinate, bool>(uri, data);
        }
    }
}