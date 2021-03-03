using Android.Support.V4.Widget;
using Android.Widget;
using SURA.Droid.Helpers;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ResolutionGroupName("efectoPersonalizado")]
[assembly: ExportEffect(typeof(IAjustarLabel), nameof(IAjustarLabel))]
namespace SURA.Droid.Helpers
{
    public class IAjustarLabel:PlatformEffect
    {
        protected override void OnAttached()
        {
            var textView = Control as TextView;
            if (textView == null)
                return;
            TextViewCompat.SetAutoSizeTextTypeWithDefaults(textView, TextViewCompat.AutoSizeTextTypeNone);
        }

        protected override void OnDetached()
        {

        }
    }
}
