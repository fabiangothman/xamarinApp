using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Views.InputMethods;
using Android.Widget;
using Plugin.CurrentActivity;
using SURA.Helpers;
using Xamarin.Forms;

[assembly: Xamarin.Forms.Dependency(typeof(SURA.Droid.Helpers.TecladoHelper))]
namespace SURA.Droid.Helpers
{
    class TecladoHelper : IAjustarTeclado
    {
        public void EsconderTeclado()
        {
            InputMethodManager imm = InputMethodManager.FromContext(CrossCurrentActivity.Current.Activity.ApplicationContext);

            imm.HideSoftInputFromWindow(
                CrossCurrentActivity.Current.Activity.Window.DecorView.WindowToken, HideSoftInputFlags.NotAlways);
        }
    }
}