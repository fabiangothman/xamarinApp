using System;
using SURA.iOS.Helpers;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly:ExportRenderer(typeof(Entry), typeof(CustomEntryRenderer))]
namespace SURA.iOS.Helpers
{
    public class CustomEntryRenderer:EntryRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if(Control != null)
            {
                this.Control.BorderStyle = UITextBorderStyle.None;
            }
            
        }
    }
}
