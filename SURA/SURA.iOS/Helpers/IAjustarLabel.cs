using System;
using SURA.iOS.Helpers;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ResolutionGroupName("efectoPersonalizado")]
[assembly:ExportEffect(typeof(IAjustarLabel),nameof(IAjustarLabel))]
namespace SURA.iOS.Helpers
{
    public class IAjustarLabel: PlatformEffect
    {

        protected override void OnAttached()
        {
            var label = Control as UILabel;
            label.AdjustsFontSizeToFitWidth = true;
            //label.Lines = 4;
            label.MinimumScaleFactor = 0.33f;
        }

        protected override void OnDetached()
        {

        }
    }
}
