using KormoranMobile.Maui.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KormoranMobile.Maui.Platforms.Windows.Impl
{
    internal class ToastMessageService : IToastMessageService
    {
        public void ShowToast(string message)
        {
            /*var toastBuilder = new ToastContentBuilder()
                .AddText(message)
                .AddAudio(new Uri("ms-appx:///Sound.mp3"))
                .SetToastDuration(ToastDuration.Long);*/
            //toastBuilder.Show();
        }
    }
}
