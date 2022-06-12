using KormoranMobile.Views.Pages;

namespace KormoranMobile
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