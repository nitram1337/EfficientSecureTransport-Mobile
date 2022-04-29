using System.Threading.Tasks;
using System.Collections.ObjectModel;

using MobileApp.Models.Route;

namespace MobileApp.Services.Route
{
    public interface IRouteService
    {
        Task<ObservableCollection<RouteForList>> GetRoutesAsync();
        Task<bool> UpdateDriverLocationByCoordinates(RouteCoordinate routeCoordinate);
    }
}