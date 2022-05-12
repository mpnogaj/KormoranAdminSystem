using Android.Widget;
using KormoranMobile.Maui.Services;

namespace KormoranMobile.Maui.Platforms.Android.Impl
{
    public class ToastMessageService : IToastMessageService
    {
        public void ShowToast(string message)
        {
            Toast.MakeText(Platform.AppContext, message, ToastLength.Long).Show();
        }
    }
}