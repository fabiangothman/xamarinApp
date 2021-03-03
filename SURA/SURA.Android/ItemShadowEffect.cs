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
using SURA;
using SURA.Droid;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;


//[assembly:ResolutionGroupName ("efectoPersonalizado")]
//[assembly: Xamarin.Forms.Dependency(typeof(ItemShadowEffect))]
[assembly: Xamarin.Forms.Dependency(typeof(SURA.Droid.ItemShadowEffect))]
[assembly:ExportEffect (typeof(ItemShadowEffect), "ItemShadowEffect")]
namespace SURA.Droid
{
	public class ItemShadowEffect : PlatformEffect
	{
		protected override void OnAttached()
		{
			try
			{
				var control = Control as Android.Widget.TextView;
				var effect = (ShadowEffect)Element.Effects.FirstOrDefault(e => e is ShadowEffect);
				if (effect != null)
				{
					float radius = effect.Radius;
					float distanceX = effect.DistanceX;
					float distanceY = effect.DistanceY;
					Android.Graphics.Color color = effect.Color.ToAndroid();
					control.SetShadowLayer(radius, distanceX, distanceY, color);
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine("Cannot set property on attached control. Error: ", ex.Message);
			}
		}

		protected override void OnDetached()
		{
		}
	}
}