using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

using SURA.Droid.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(Entry), typeof(MyEntryRenderer))]
namespace SURA.Droid.Renderers
{
    class MyEntryRenderer : EntryRenderer
    {
        public MyEntryRenderer(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            try
            {
                base.OnElementChanged(e);

                if (Control != null)
                {
                    var nativeEditText = (global::Android.Widget.EditText)Control;
                    nativeEditText.Bottom = 0;
                    var shape = new ShapeDrawable(new Android.Graphics.Drawables.Shapes.RoundRectShape(new[] { 12f, 12f, 12f, 12f, 12f, 10f, 12f, 12f }, null, null));
                    shape.Paint.Color = Xamarin.Forms.Color.White.ToAndroid();
                    shape.Paint.StrokeWidth = 9;
                    nativeEditText.Background = shape;
                    Control.SetBackgroundColor(color: Android.Graphics.Color.Transparent);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.ToString());
                //TO DO
            }
        }
    }
}