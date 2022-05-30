namespace KormoranMobile.Maui.Helpers
{
    public static class ServerHelper
    {
        public const string AddressKey = "serverAddress";

        public static bool AddressSet =>
            Preferences.ContainsKey(AddressKey);
        public static string ServerAddress =>
            AddressSet ? $"http://{Preferences.Get(AddressKey, string.Empty)}/api" : string.Empty;

        public static HttpClient DefaultHttpClient => AddressSet ? new HttpClient
        {
            BaseAddress = new Uri(ServerAddress),
            Timeout = TimeSpan.FromSeconds(10)
        } : throw new Exception("Server address was empty when creating HttpClient!");
    }
}
