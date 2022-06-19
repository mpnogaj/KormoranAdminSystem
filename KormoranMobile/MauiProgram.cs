using CommunityToolkit.Maui;
using KormoranMobile.Helpers;

namespace KormoranMobile
{
	public static class MauiProgram
	{
		public static MauiApp CreateMauiApp()
		{
			var builder = MauiApp.CreateBuilder();
			builder
				.UseMauiApp<App>()
				.UseMauiCommunityToolkit()
				.ConfigureFonts(fonts =>
				{
					fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
					fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
					fonts.AddFont("kormoran.ttf", "KormoranFont");
				});

			if(!ServerHelper.AddressSet)
			{
				Preferences.Set(ServerHelper.AddressKey, "kormoran.azurewebsites.net");
			}

			return builder.Build();
		}
	}
}