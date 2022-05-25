namespace KormoranMobile.Maui.Helpers
{
    public static class ServerHelper
    {
        public const string AddressKey = "serverAddress";

        public static bool AddressSet =>
            Preferences.ContainsKey(AddressKey);
        public static string ServerAddress =>
            AddressSet ? $"http://{Preferences.Get(AddressKey, string.Empty)}/api" : string.Empty;
    }
}
