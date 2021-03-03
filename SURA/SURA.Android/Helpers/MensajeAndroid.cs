using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using SURA.Helpers;

[assembly: Xamarin.Forms.Dependency(typeof(SURA.Droid.Helpers.MensajeAndroid))]
namespace SURA.Droid.Helpers
{
    class MensajeAndroid : IMensajes
    {
        public void LongAlert(string mensaje)
        {
            Toast.MakeText(Application.Context, mensaje, ToastLength.Long).Show();
        }
    }
}