using KormoranMobile.Maui.Services;
using CommunityToolkit.Maui;
#if __ANDROID__
using KormoranMobile.Maui.Platforms.Android.Impl;
#elif WINDOWS
using System;
#endif

namespace KormoranMobile.Maui
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
                });

            //builder.Services.AddSingleton<IToastMessageService, ToastMessageService>();

            return builder.Build();
        }
    }
}