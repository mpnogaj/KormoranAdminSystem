using KormoranMobile.ViewModels;
using KormoranShared.Models;
using System.Collections.ObjectModel;
using System.Diagnostics;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace KormoranMobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TournamentsPage : ContentPage
    {
        public TournamentsPage()
        {
            InitializeComponent();
        }

        async void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item == null)
                return;
            ((ListView)sender).SelectedItem = null;
            var tournament = (Tournament)e.Item;
            var matchesPage = new MatchesPage
            {
                BindingContext = new MatchesPageViewModel
                {
                    Matches = new ObservableCollection<Match>(tournament.Matches),
                    TournamentId = tournament.TournamentId
                }
            };
            await Navigation.PushAsync(matchesPage);
        }

        private async void NavigateSettings(object sender, System.EventArgs e)
        {
            await Navigation.PushAsync(new SettingsPage());
        }
    }
}
