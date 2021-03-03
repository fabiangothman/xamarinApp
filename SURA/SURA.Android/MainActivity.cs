using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Plugin.CurrentActivity;
using Plugin.Fingerprint;
using Plugin.Permissions;
using Android.Gms.Common;
using Android.Content.Res;
using System.Runtime.Remoting.Contexts;
using Android.Util;
using Android.Support.V4.Content;
using Android.Support.V4.App;
using Android;
using Android.Graphics.Drawables;
using Xamarin.Forms.GoogleMaps.Android;
using Xamarin.Forms;
using Android.Content;

namespace SURA.Droid
{
    [Activity(Label = "SURA", Icon = "@mipmap/logoapp", RoundIcon ="@mipmap/logoapp_round" ,Theme = "@style/splashscreen", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        TextView msgText;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            try
            {
                Xamarin.Forms.Forms.SetFlags(new string[] { "CarouselView_Experimental", "IndicatorView_Experimental", "Expander_Experimental", "SwipeView_Experimental" , "Brush_Experimental" });
                CrossCurrentActivity.Current.Init(this, savedInstanceState);
                CrossFingerprint.SetCurrentActivityResolver(() => CrossCurrentActivity.Current.Activity);

                TabLayoutResource = Resource.Layout.Tabbar;
                ToolbarResource = Resource.Layout.Toolbar;

                base.OnCreate(savedInstanceState);

                Rg.Plugins.Popup.Popup.Init(this, savedInstanceState);

                var platformConfig = new PlatformConfig
                {
                    BitmapDescriptorFactory = new Renderers.AccessNativeBitmapConfig()
                };

                Xamarin.FormsGoogleMaps.Init(this, savedInstanceState);

                Xamarin.Essentials.Platform.Init(this, savedInstanceState);
                global::Xamarin.Forms.Forms.Init(this, savedInstanceState);

                this.Window.AddFlags(WindowManagerFlags.ForceNotFullscreen);
                this.Window.ClearFlags(WindowManagerFlags.Fullscreen);

                LoadApplication(new App());
                Window.SetStatusBarColor(Android.Graphics.Color.Rgb(0, 51, 160));
            }catch(Exception ex)
            {
                App.Current.MainPage.DisplayAlert("",ex.ToString(),"Ok");
            }
            
        }


        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Permission[] grantResults)
        {
            PermissionsImplementation.Current.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            //base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }


        public bool IsPlayServicesAvailable()
        {
            int resultCode = GoogleApiAvailability.Instance.IsGooglePlayServicesAvailable(this);
            if (resultCode != ConnectionResult.Success)
            {
                if (GoogleApiAvailability.Instance.IsUserResolvableError(resultCode))
                {
                    msgText.Text = GoogleApiAvailability.Instance.GetErrorString(resultCode);
                }
                else
                {
                    msgText.Text = "This device is not supported";
                    Finish();
                }
                return false;
            }
            else
            {
                msgText.Text = "Google Play Services is available.";
                return true;
            }
        }

        protected override void OnResume()
        {
            base.OnResume();
            System.Globalization.CultureInfo.DefaultThreadCurrentCulture = System.Globalization.CultureInfo.CreateSpecificCulture("es-PA");
        }

        protected override void AttachBaseContext(Android.Content.Context @base)
        {
            // base.AttachBaseContext(@base);
            Configuration config = new Configuration();
            config.SetToDefaults();
            config.FontScale = 1.0f;
            Android.Content.Context context = @base.CreateConfigurationContext(config);
            base.AttachBaseContext(context);
        }

        public override void OnBackPressed()
        {
            if (Rg.Plugins.Popup.Popup.SendBackPressed(base.OnBackPressed))
            {
                // Do nothing if there are some pages in the `PopupStack`
            }
            else
            {
                // Do something if there are not any pages in the `PopupStack`
            }
        }

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            try
            {
                MessagingCenter.Send(
                new ActivityResultMessage { RequestCode = requestCode, ResultCode = resultCode, Data = data }, ActivityResultMessage.Key);
            }
            catch(Exception ex)
            {
                App.Current.MainPage.DisplayAlert("",ex.Message,"Ok");
            }
            
        }

    }
}