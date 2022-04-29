using System;
using System.Windows.Input;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Essentials;

using MobileApp.Models.Route;
using MobileApp.Services.Route;

namespace MobileApp.ViewModels.Route
{
    public class CurrentRouteViewModel : BaseViewModel
    {
        #region Services

        private readonly IRouteService _routeService;

        #endregion Services

        #region Commands

        public ICommand TakeBreakCommand { get; set; }

        #endregion Commands

        #region Properties

        RouteCoordinate routeCoordinate;
        public RouteCoordinate RouteCoordinate
        {
            get { return routeCoordinate; }
            set { SetProperty(ref routeCoordinate, value); }
        }

        #endregion Properties

        #region Constructor

        public CurrentRouteViewModel()
        {
            _routeService = DependencyService.Get<IRouteService>();

            TakeBreakCommand = new Command(async () => await ExecuteTakeBreakAsync());

            IsConnected = Connectivity.NetworkAccess != NetworkAccess.Internet;
            IsButtonClickable = Connectivity.NetworkAccess == NetworkAccess.Internet;
            Connectivity.ConnectivityChanged += OnConnectivityChanged;
        }

        #endregion Constructor

        async Task ExecuteTakeBreakAsync()
        {
            IsBusy = true;

            try
            {
                var currentPosition = await Geolocation.GetLocationAsync(new GeolocationRequest(GeolocationAccuracy.Best, TimeSpan.FromSeconds(5000)));
                RouteCoordinate.Coordinate.Latitude = currentPosition.Latitude;
                RouteCoordinate.Coordinate.Longitude = currentPosition.Longitude;

                await _routeService.UpdateDriverLocationByCoordinates(RouteCoordinate);
            }
            finally
            {
                IsBusy = false;
            }
        }

        public void OnAppearing()
        {
            IsBusy = true;

            try
            {
                RouteCoordinate = new RouteCoordinate
                {
                    RouteId = 3,
                    Coordinate = new Coordinate()
                };
            }
            finally
            {
                IsBusy = false;
            }
        }

        #region Events

        private void OnConnectivityChanged(object sender, ConnectivityChangedEventArgs e)
        {
            IsConnected = e.NetworkAccess != NetworkAccess.Internet;
            IsButtonClickable = e.NetworkAccess == NetworkAccess.Internet;
        }

        #endregion Events
    }
}