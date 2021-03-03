using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using SURA.Helpers;
using UIKit;

[assembly: Xamarin.Forms.Dependency(
          typeof(SURA.iOS.Helpers.TecladoHelper))]
namespace SURA.iOS.Helpers
{
    class TecladoHelper : IAjustarTeclado
    {
        public void EsconderTeclado()
        {
            UIApplication.SharedApplication.InvokeOnMainThread(() =>
            {
                var window = UIApplication.SharedApplication.KeyWindow;
                var vc = window.RootViewController;
                while (vc.PresentedViewController != null)
                {
                    vc = vc.PresentedViewController;
                }

                vc.View.EndEditing(true);
            });

        }
    }
}