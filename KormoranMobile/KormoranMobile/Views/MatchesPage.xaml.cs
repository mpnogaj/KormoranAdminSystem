using KormoranMobile.ViewModels;
using KormoranShared.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace KormoranMobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MatchesPage : ContentPage
    {
        public MatchesPage()
        {
            InitializeComponent();
        }

        private async void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item == null)
                return;
            ((ListView)sender).SelectedItem = null;

            var match = (Match)e.Item;
            await Navigation.PushAsync(new EditScorePage
            {
                BindingContext = new EditScorePageViewModel
                {
                    Match = match
                }
            });
        }
    }
}