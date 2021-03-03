using Android.App;

namespace SURA.Droid
{
    internal class ActivityResultMessage
    {
        public static string Key = "arm";
        public int RequestCode { get; set; }
        public Result ResultCode { get; set; }
        public object Data { get; set; }
    }
}