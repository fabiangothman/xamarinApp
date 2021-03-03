using Foundation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UIKit;
using CoreGraphics;
using SURA;
using SURA.iOS;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

//[assembly:ResolutionGroupName ("MyCompany")]
//[assembly: Xamarin.Forms.Dependency(typeof(ItemShadowEffect))]
[assembly: Xamarin.Forms.Dependency(typeof(SURA.iOS.ItemShadowEffect))]
[assembly:ExportEffect (typeof(ItemShadowEffect), "ItemShadowEffect")]
namespace SURA.iOS
{
	public class ItemShadowEffect : PlatformEffect
	{
		protected override void OnAttached()
		{
			try
			{
				var effect = (ShadowEffect)Element.Effects.FirstOrDefault(e => e is ShadowEffect);
				if (effect != null)
				{
					Control.Layer.CornerRadius = effect.Radius;
					Control.Layer.ShadowColor = effect.Color.ToCGColor();
					Control.Layer.ShadowOffset = new CGSize(effect.DistanceX, effect.DistanceY);
					Control.Layer.ShadowOpacity = 1.0f;
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