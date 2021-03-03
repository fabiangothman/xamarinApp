using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;

namespace SURA.Views.PublicViews.MapsView
{
	public class MapView : ContentView
    {
        Map map;
        Plugin.Geolocator.Abstractions.Position position;
        List<Xamarin.Essentials.Location> location;
        public MapView ()
		{

            try
            {
                this.HorizontalOptions = LayoutOptions.FillAndExpand;
                this.VerticalOptions = LayoutOptions.FillAndExpand;
                map = new Map
                {
                    MyLocationEnabled = true,
                };

                map.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(9.025039, -79.533702), Distance.FromMiles(50)));
                

                Grid grid = new Grid();
                grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(5,GridUnitType.Star) });
                map.MapType = MapType.Street;
                grid.Children.Add(map, 0, 0);
                Content = grid;
               
                map.PropertyChanged += (sender, e) =>
                {
                    Debug.WriteLine(e.PropertyName + " just changed!");
                    if (e.PropertyName == "VisibleRegion" && map.Region != null)
                        CalculateBoundingCoordinates(map.VisibleRegion);
                };
            }
            catch (Exception ex)
            {
                 App.Current.MainPage.DisplayAlert("SURA", "Hubo un error, vuelva a intentarlo", "OK");
            }
            
        }

        private async void Location()
        {
            
            string addressSegurosSuramericana = "Seguros Suramericana SA Panama";
            var locations = await Xamarin.Essentials.Geocoding.GetLocationsAsync(addressSegurosSuramericana);

            location = locations.ToList();

            int ListIndex = 0;
            if (location != null)
            {
                foreach (var item in location)
                {
                    Pin pin = new Pin
                    {
                        Type = PinType.Place,
                        Position = new Position(location[ListIndex].Latitude, location[ListIndex].Longitude),
                        Label = "Seguros Sura"
                    };
                    ListIndex = ListIndex + 1;
                    map.Pins.Add(pin);
                }
            }


        }
        
        static void CalculateBoundingCoordinates(MapSpan region)
        {
            Xamarin.Forms.GoogleMaps.Position center = region.Center;
            double halfheightDegrees = region.LatitudeDegrees / 2;
            double halfwidthDegrees = region.LongitudeDegrees / 2;

            double left = center.Longitude - halfwidthDegrees;
            double right = center.Longitude + halfwidthDegrees;
            double top = center.Latitude + halfheightDegrees;
            double bottom = center.Latitude - halfheightDegrees;
            
        }
    }
    
}