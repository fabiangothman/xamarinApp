using System;

using Android.App;
using Android.Content;
using Android.Util;
using SURA.Helpers;
using Android.Locations;
using SURA.Droid.Helpers;
using Android.Gms.Common.Apis;
using Android.Gms.Location;
using Xamarin.Forms;
using Android.Telecom;
using Plugin.CurrentActivity;
using Android.Gms.Tasks;
using System.Threading.Tasks;
using Android.Widget;
using System.Threading;

[assembly: Xamarin.Forms.Dependency(
          typeof(PlatformDetails))]
namespace SURA.Droid.Helpers
{
    public class PlatformDetails : IMyInterface
    {
        Context context = Android.App.Application.Context;
        public const int REQUEST_CHECK_SETTINGS = 0x1;

        public async Task<bool> OpenMyGPS()
        {
            Int64 interval = 1000 * 60 * 1, fastestInterval = 1000 * 50;
            var taskCompletionSource = new TaskCompletionSource<bool>();

            try
            {
                
                GoogleApiClient googleApiClient = new GoogleApiClient.Builder(context).AddApi(LocationServices.API).Build();

                googleApiClient.Connect();

                LocationRequest locationRequest = LocationRequest.Create()
                        .SetPriority(LocationRequest.PriorityBalancedPowerAccuracy)
                        .SetInterval(interval)
                        .SetFastestInterval(fastestInterval)
                        .SetNumUpdates(3);

                var locationSettingsRequestBuilder = new LocationSettingsRequest.Builder()
                        .AddLocationRequest(locationRequest);

                locationSettingsRequestBuilder.SetAlwaysShow(true);

                var result = await LocationServices.SettingsApi.CheckLocationSettingsAsync(googleApiClient, locationSettingsRequestBuilder.Build());

                if (!result.Status.IsSuccess)
                {
                    if (result.Status.StatusCode == LocationSettingsStatusCodes.ResolutionRequired)
                    {
                        result.Status.StartResolutionForResult(CrossCurrentActivity.Current.Activity, 101);
                        // Subscribe to the onactivityresult-message
                        MessagingCenter.Subscribe<ActivityResultMessage>(this, ActivityResultMessage.Key, (sender) =>
                        {
                            var CodigoResultado = sender.ResultCode;

                                switch (CodigoResultado)
                                {
                                    case Result.Ok:
                                        taskCompletionSource.TrySetResult(true);
                                        break;
                                    case Result.Canceled:
                                        taskCompletionSource.TrySetResult(false);
                                        break;
                                    default:
                                        taskCompletionSource.TrySetResult(true);
                                        break;
                                }

                        });
                    }
                    else
                        Toast.MakeText(CrossCurrentActivity.Current.Activity, "No está disponible los servicios de GPS", ToastLength.Long).Show();
                }
                else
                {
                    taskCompletionSource.TrySetResult(true);
                }
            }
            catch (Exception exception)
            {
                Log.Info("Settings Client", exception.Message);
            }

            return await taskCompletionSource.Task;
        }
    }
}