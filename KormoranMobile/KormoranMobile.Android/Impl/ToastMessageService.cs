using Android.Widget;
using KormoranMobile.Droid.Impl;
using KormoranMobile.Services;
using Xamarin.Essentials;
using Xamarin.Forms;

[assembly: Dependency(typeof(ToastMessageService))]
namespace KormoranMobile.Droid.Impl
{
    public class ToastMessageService : IToastMessageService
    {
        public void ShowToast(string message)
        {
            Toast.MakeText(Platform.AppContext, message, ToastLength.Long).Show();
        }
    }
}