using KormoranMobile.ViewModels.Pages;

namespace KormoranMobile.Views.Pages;

public partial class MatchesPage : ContentPage
{
	public MatchesPage()
	{
		InitializeComponent();
		this.BindingContext = new MatchesPageViewModel();
	}
}