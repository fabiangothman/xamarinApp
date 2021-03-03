using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using UIKit;
using SURA.Helpers;
using SURA.iOS.Helpers;

[assembly: Xamarin.Forms.Dependency(typeof(MensajeiOS))]
namespace SURA.iOS.Helpers
{
    class MensajeiOS : IMensajes
    {
        const double LONG_DELAY = 3.5;
        NSTimer alertDelay;
        UIAlertController alert;
        public void LongAlert(string mensaje)
        {
            alertDelay = NSTimer.CreateScheduledTimer(LONG_DELAY, (obj) =>
            {
                dismissMessage();
            });
            alert = UIAlertController.Create(null, mensaje, UIAlertControllerStyle.Alert);
            UIApplication.SharedApplication.KeyWindow.RootViewController.PresentViewController(alert, true, null);
        }

        void dismissMessage()
        {
            if (alert != null)
            {
                alert.DismissViewController(true, null);
            }
            if (alertDelay != null)
            {
                alertDelay.Dispose();
            }
        }
    }
}