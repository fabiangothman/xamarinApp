using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using Rg.Plugins.Popup.Animations;
using Rg.Plugins.Popup.Enums;
using Rg.Plugins.Popup.Services;
using SURA.Views.AppUbicaciones;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;


namespace SURA.Views.AppUbicaciones
{
    public class SucursalesView : ContentView
    {
        Map map;
        Grid grid = new Grid();


        public SucursalesView()
        {
            try
            {
                this.HorizontalOptions = LayoutOptions.FillAndExpand;
                this.VerticalOptions = LayoutOptions.FillAndExpand;

                map = new Map();

                Permisos();

                map.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(9.025039, -79.533702), Distance.FromMiles(10)));

                int ListIndex = 0;
                foreach (var item in App.Sucursales)
                {

                    Pin pin = new Pin
                    {
                        Type = PinType.Place,
                        Position = new Position(App.Sucursales[ListIndex].latitude, App.Sucursales[ListIndex].longitude),
                        Label = "Seguros SURA",
                        Address = App.Sucursales[ListIndex].address,
                        Icon = BitmapDescriptorFactory.FromBundle("pinazul.png")
                    };
                    
                    ListIndex = ListIndex + 1;
                    map.Pins.Add(pin);
                }

                map.PinClicked += AparecerDetalle;
                grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(5, GridUnitType.Star) });
                map.MapType = MapType.Street;
                grid.Children.Add(map, 0, 0);



                Content = grid;
            }
            catch (Exception ex)
            {
                App.Current.MainPage.DisplayAlert("ERROR", ex.Message, "OK");
            }
        }

        private async void AparecerDetalle(object sender, PinClickedEventArgs e)
        {
            try
            {
                var SucursalSeleccionado = e.Pin.Address;

                var animationPopup = new PopUpDetalleUbicacion(SucursalSeleccionado);

                await PopupNavigation.Instance.PushAsync(animationPopup);
            }
            catch(Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("",ex.Message,"Ok");
            }
            
        }

        private async void Permisos()
        {
            await Permissions();
        }

        public async Task Permissions()
        {
            try
            {
                var status = await Plugin.Permissions.CrossPermissions.Current.CheckPermissionStatusAsync<Plugin.Permissions.LocationPermission>();
                if (status != Plugin.Permissions.Abstractions.PermissionStatus.Granted)
                {
                    var results = await Plugin.Permissions.CrossPermissions.Current.RequestPermissionAsync<Plugin.Permissions.LocationPermission>();
                    status = results;

                }

                if (status == Plugin.Permissions.Abstractions.PermissionStatus.Granted)
                {
                    map.MyLocationEnabled = true;
                    map.UiSettings.MyLocationButtonEnabled = true;
                }
                else if (status != Plugin.Permissions.Abstractions.PermissionStatus.Unknown)
                {
                    map.MyLocationEnabled = false;
                    map.UiSettings.MyLocationButtonEnabled = false;
                    await App.Current.MainPage.DisplayAlert("", "Localización denegada", "OK");
                }
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Ocurrió un problema", ex.ToString(), "OK");
            }
        }


    }
}