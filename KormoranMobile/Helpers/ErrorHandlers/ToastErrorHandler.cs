using CommunityToolkit.Maui.Alerts;

namespace KormoranMobile.Helpers.ErrorHandlers
{
	public class ToastErrorHandler : IErrorHandler
	{
		public Task HandleAsync(string message)
		{
			return Toast.Make(message).Show();
		}
	}
}
