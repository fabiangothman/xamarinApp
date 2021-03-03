using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using UIKit;
using SURA.Helpers;
using CoreLocation;
using SURA.iOS.Helpers;
using System.Threading.Tasks;

[assembly: Xamarin.Forms.Dependency(
          typeof(PlatformDetails))]
namespace SURA.iOS.Helpers
{
    public class PlatformDetails : IMyInterface
    {

        public async Task<bool> OpenMyGPS()
        {
            try
            {
                if (CLLocationManager.Status == CLAuthorizationStatus.Denied || CLLocationManager.Status == CLAuthorizationStatus.NotDetermined || CLLocationManager.Status == CLAuthorizationStatus.Restricted)
                {
                    //await App.Current.MainPage.DisplayAlert("","Para hacer uso de ","Ok");
                    //var url = new NSUrl("App-Prefs:root=LOCATION_SERVICES");

                    //if (UIApplication.SharedApplication.CanOpenUrl(url))
                    //{
                    //    UIApplication.SharedApplication.OpenUrl(url);
                    //}
                    return false;
                }
            }
            catch (Exception ex)
            {

            }
            return true;
        }
    }


}