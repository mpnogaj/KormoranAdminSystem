using KormoranMobile.Services;
using KormoranMobile.UWP.Impl;
using Microsoft.Toolkit.Uwp.Notifications;
using System;
using Xamarin.Essentials;
using Xamarin.Forms;

[assembly: Dependency(typeof(ToastMessageService))]
namespace KormoranMobile.UWP.Impl
{
    public class ToastMessageService : IToastMessageService
    {
        public void ShowToast(string message)
        {
            var toastBuilder = new ToastContentBuilder()
                .AddText(message)
                .AddAudio(new Uri("ms-appx:///Sound.mp3"))
                .SetToastDuration(ToastDuration.Long);
            toastBuilder.Show();
        }
    }
}