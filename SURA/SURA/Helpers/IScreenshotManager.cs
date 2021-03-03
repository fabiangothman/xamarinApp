using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SURA.Helpers
{
    public interface IScreenshotManager
    {
        Task<byte[]> CaptureAsync(View v);
    }
}
