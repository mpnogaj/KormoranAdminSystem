using CommunityToolkit.Maui.Views;

namespace KormoranMobile.Maui.Helpers
{
	public static class PopupHelper
	{
		public static async Task<object?> ShowPopupAsync(Popup popup)
		{
			if (Application.Current == null)
			{
				throw new Exception("Application.Current is null");
			}
			if (Application.Current.MainPage == null)
			{
				throw new Exception("MainPage is null");
			}
			return await Application.Current.MainPage.ShowPopupAsync(popup);
		}
	}
}
