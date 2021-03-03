using System;
using System.Threading.Tasks;
using SURA.Helpers;
using SURA.iOS.Helpers;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: Xamarin.Forms.Dependency(typeof(ScreenshotManager))]
namespace SURA.iOS.Helpers
{
    public class ScreenshotManager : IScreenshotManager
    {
        

        public Task<byte[]> CaptureAsync(View v)
        {
            var vRenderer = RendererFactory.GetRenderer(v);
            var view = vRenderer.NativeView;
            UIGraphics.BeginImageContext(view.Frame.Size);
            view.DrawViewHierarchy(view.Frame, true);
            var image = UIGraphics.GetImageFromCurrentImageContext();
            UIGraphics.EndImageContext();
            var bytes = new byte[0];
            using (var imageData = image.AsPNG())
            {
                bytes = new byte[imageData.Length];
                System.Runtime.InteropServices.Marshal.Copy(imageData.Bytes, bytes, 0, Convert.ToInt32(imageData.Length));
            }

            return Task.FromResult(bytes);
        }
    }
}
