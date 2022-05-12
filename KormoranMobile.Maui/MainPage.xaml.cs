namespace KormoranMobile.Maui
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        public MainPage()
        {
            InitializeComponent();
        }

        private void OnCounterClicked(object sender, EventArgs e)
        {
            /*Task.Run(() => Toast.Make("test123", ToastDuration.Long).Show());
            count++;
            CounterLabel.Text = $"Current count: {count}";

            SemanticScreenReader.Announce(CounterLabel.Text);*/

            Shell.Current.GoToAsync("//LoginPage");
        }
    }
}