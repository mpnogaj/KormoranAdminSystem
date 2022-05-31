using KormoranMobile.Maui.Services;

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
