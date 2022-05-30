using KormoranMobile.Maui.Helpers;
using KormoranMobile.Maui.Views;

namespace KormoranMobile.Maui
{
	public partial class AppShell : Shell
	{
		public AppShell()
		{
			InitializeComponent();

			Routing.RegisterRoute(
				string.Join('/', 
					nameof(TournamentsPage), 
					nameof(LoginPage)), typeof(LoginPage));

			Routing.RegisterRoute(
				string.Join('/',
					nameof(TournamentsPage),
					nameof(MatchesPage)), typeof(MatchesPage));
			
			Routing.RegisterRoute(
				string.Join('/', 
					nameof(TournamentsPage), 
					nameof(MatchesPage), 
					nameof(EditScoresPage)), typeof(EditScoresPage));
		}
	}
}