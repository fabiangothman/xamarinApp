using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.Graphics;
using SURA;
using SURA.Droid;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: Xamarin.Forms.Dependency(typeof(SURA.Droid.ItemShadowEffect))]
[assembly: ExportRenderer(typeof(CustomFrame), typeof(CustomFrameShadowRenderer))]
namespace SURA.Droid
{
    [Obsolete]
    public class CustomFrameShadowRenderer : Xamarin.Forms.Platform.Android.AppCompat.FrameRenderer
    {
        public CustomFrameShadowRenderer(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Frame> e)
        {
            base.OnElementChanged(e);
            var element = e.NewElement as CustomFrame;


            if (element == null) return;

            if (element.HasShadow)
            {
                Elevation = 10.0f;
                TranslationZ = 10.0f;
                SetZ(10f);


            }

        }

    }
}