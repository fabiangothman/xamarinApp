using Plugin.Permissions;
using SURA.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;
using Xamarin.Forms.Xaml;

namespace SURA.Views.AppPublico.AsistenciaVial
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AsistenciaVial2 : ContentView
    {
        private Location location;
        private List<Location> _location;
        Services.NetworkAvailabilityService networkAvailability = new Services.NetworkAvailabilityService();
        bool NetworkStatus;
        public AsistenciaVial2()
        {
            try
            {
                InitializeComponent();
                LocalizacionUsuario();
            }
            catch(Exception ex)
            {
                App.Current.MainPage.DisplayAlert("", ex.Message,"Ok"); ;
            }
        }

        private async void LocalizacionUsuario()
        {
            try
            {
                var permiso = await Permissions();
                if (permiso)
                {
                    btnSi.IsEnabled = false;
                    btnNo.IsEnabled = false;
                    btnBuscar.IsEnabled = false;
                    var gpsEnciende = await DependencyService.Get<IMyInterface>().OpenMyGPS();
                    map.MyLocationEnabled = true;
                    map.UiSettings.MyLocationButtonEnabled = true;

                    if (gpsEnciende)
                    {
                        var request = new GeolocationRequest(GeolocationAccuracy.Medium, TimeSpan.FromSeconds(5));
                        location = await Geolocation.GetLocationAsync(request);
                        Geocoder geoCoder = new Geocoder();
                        var revposition = new Position(location.Latitude, location.Longitude);
                        var possibleAddresses = await geoCoder.GetAddressesForPositionAsync(revposition);

                        App.AsistenciaVialCoordenadas.address = possibleAddresses.First().Replace(",", "") + " " + possibleAddresses.Last().Replace(",", "");
                        await frmGris.FadeTo(0,400);
                        frmGris.IsVisible = false;
                        btnSi.IsEnabled = true;
                        btnNo.IsEnabled = true;
                        btnBuscar.IsEnabled = true;

                        map.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(location.Latitude, location.Longitude), Distance.FromMiles(5)).WithZoom(20));
                        
                        
                        
                    }
                    else
                    {
                        await App.Current.MainPage.DisplayAlert("", "¿Podria especificar su ubicación manualmente?", "OK");
                        await frmGris.FadeTo(0, 500);
                        frmGris.IsVisible = false;
                        entryDireccion.Focus();
                    }
                }
                else
                {
                    await App.Current.MainPage.DisplayAlert("", "¿Podria especificar su ubicación manualmente?", "OK");
                    entryDireccion.Focus();
                }

            }
            catch (FeatureNotSupportedException fnsEx)
            {
                await App.Current.MainPage.DisplayAlert("", "Esta actividad no esta soportada en el dispositivo", "Ok");
            }
            catch (FeatureNotEnabledException fneEx)
            {
                //await App.Current.MainPage.DisplayAlert("SURA", "GPS no esta activado", "Ok");
            }
            catch (PermissionException pEx)
            {
                await App.Current.MainPage.DisplayAlert("SURA", "Los Permisos no estan activados", "Ok");
                entryDireccion.Focus();
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("SURA", "No se pudo encontrar su localizacion", "Ok");
            }
        }

        private async void BuscarLocalizacion()
        {
            try
            {
                btnSi.IsEnabled = true;
                string addressSegurosSuramericana = entryDireccion.Text + " " + "Panamá";
                var locationsString = await Geocoding.GetLocationsAsync(addressSegurosSuramericana);

                _location = locationsString.ToList();

                int ListIndex = 0;
                var locationAddress = await Geocoding.GetPlacemarksAsync(_location[ListIndex].Latitude, _location[ListIndex].Longitude);
                var placemark = locationAddress?.FirstOrDefault();
                if (_location != null)
                {

                    if (_location.Count > 1)
                    {
                        await App.Current.MainPage.DisplayAlert("SURA", "'Podria especificar su ubicación?", "OK");
                        foreach (var item in _location)
                        {
                            Pin pin = new Pin
                            {
                                Type = PinType.SearchResult,
                                Position = new Position(_location[ListIndex].Latitude, _location[ListIndex].Longitude),
                                Label = entryDireccion.Text.ToUpper()
                            };

                            map.Pins.Add(pin);
                            ListIndex = ListIndex + 1;
                        }
                    }
                    else
                    {
                        map.Pins.Clear();

                        Pin pin = new Pin
                        {
                            Type = PinType.SearchResult,
                            Position = new Position(_location[0].Latitude, _location[0].Longitude),
                            Label = entryDireccion.Text.ToUpper(),
                        };
                        map.Pins.Add(pin);
                        await map.MoveCamera(CameraUpdateFactory.NewPositionZoom(new Position(_location[0].Latitude, _location[0].Longitude), 16));

                    }
                }
            }
            catch (Exception ex)
            {

                await App.Current.MainPage.DisplayAlert("SURA","No pudimos ubicar su dirección. Intente con una localización cercana.","Ok");
            }

        }

        private async void btnSi_Clicked(object sender, EventArgs e)
        {
            try
            {
                NetworkStatus = networkAvailability.NetworkAvailabilityServiceMethod();
                btnSi.IsEnabled = false;
                if (NetworkStatus == true)
                {
                    if (location != null)
                    {
                        Geocoder geoCoder = new Geocoder();
                        var revposition = new Position(location.Latitude, location.Longitude);
                        var possibleAddresses = await geoCoder.GetAddressesForPositionAsync(revposition);
                        App.AsistenciaVialCoordenadas.latitude = location.Latitude;
                        App.AsistenciaVialCoordenadas.longitude = location.Longitude;
                    }
                    else
                    {
                        if (_location.Count == 1)
                        {
                            int ListIndex = 0;
                            foreach (var item in _location)
                            {
                                App.AsistenciaVialCoordenadas.latitude = _location[ListIndex].Latitude;
                                App.AsistenciaVialCoordenadas.longitude = _location[ListIndex].Longitude;
                                ListIndex = ListIndex + 1;
                            }
                        }
                    }
                    App.objContenedor.MostrarVista("AsistenciaVial3");
                }
                else
                {
                    await App.Current.MainPage.DisplayAlert("SURA", "Para el uso de esta función se requiere internet", "Ok");
                }

                btnSi.IsEnabled = true;

            }
            catch (Exception)
            {
                await App.Current.MainPage.DisplayAlert("","Por favor, especifique su ubicación manualmente.","Ok");
            }
            
        }
        private void btnNo_Clicked(object sender, EventArgs e)
        {
            try
            {
                entryDireccion.Focus();
            }
            catch (Exception)
            {

            }
        }

        private void btnBuscar_Clicked(object sender, EventArgs e)
        {
            try
            {
                if (entryDireccion.Text != null && entryCiudad.Text != null)
                {
                    BuscarLocalizacion();
                }
            }
            catch (Exception ex)
            {
                App.Current.MainPage.DisplayAlert("", ex.Message, "Ok");
            }
        }

        public async Task<bool> Permissions()
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
                }
                else if (status != Plugin.Permissions.Abstractions.PermissionStatus.Unknown)
                {
                    map.MyLocationEnabled = false;
                    await App.Current.MainPage.DisplayAlert("", "Localización denegada", "OK");
                    return false;
                }
               
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("", ex.Message, "OK");
            }
            return true;
        }
    }
}