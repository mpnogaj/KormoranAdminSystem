using Plugin.Settings;
using Plugin.Settings.Abstractions;

namespace KormoranMobile.Util
{
    public class Constants
    {
        private static ISettings _settings
            => CrossSettings.Current;

        public static string API_ADDRESS => $"http://{_settings.GetValueOrDefault("ServerAddress", string.Empty)}/api";
    }
}