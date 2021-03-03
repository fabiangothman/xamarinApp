using System;
using System.IO;
using System.Threading.Tasks;
using Android.Graphics;
using SURA.Droid.Helpers;
using SURA.Helpers;
using Xamarin.Forms.Platform.Android;

[assembly: Xamarin.Forms.Dependency(typeof(ScreenshotManager))]
namespace SURA.Droid.Helpers
{
    public class ScreenshotManager : IScreenshotManager
    {
        public Task<byte[]> CaptureAsync(Xamarin.Forms.View v)
        {
            var activity = Xamarin.Forms.Forms.Context as MainActivity;
            if (activity == null)
            {
                return null;
            }

            var vRenderer = RendererFactory.GetRenderer(v);
            var viewGroup = vRenderer.ViewGroup;
            vRenderer.Tracker.UpdateLayout();

            viewGroup.DrawingCacheEnabled = true;

            Bitmap bitmap = viewGroup.GetDrawingCache(true);

            byte[] bitmapData;

            using (var stream = new MemoryStream())
            {
                bitmap.Compress(Bitmap.CompressFormat.Png, 0, stream);
                bitmapData = stream.ToArray();
            }

            return Task.FromResult(bitmapData);

        }

    }
}
