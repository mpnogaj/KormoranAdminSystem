using KormoranMobile.Maui.Views.Pages;

namespace KormoranMobile.Maui
{
	public partial class AppShell
	{
		public AppShell()
		{
			InitializeComponent();
			Routing.RegisterRoute(
				string.Join('/',
					nameof(TournamentsPage),
					nameof(MatchesPage)), typeof(MatchesPage));
		}
	}
}