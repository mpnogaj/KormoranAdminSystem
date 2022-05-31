using KormoranMobile.Maui.ViewModels;

namespace KormoranMobile.Maui.Views.Pages;

public partial class MatchesPage : ContentPage
{
	public MatchesPage()
	{
		InitializeComponent();
		this.BindingContext = new MatchesPageViewModel();
	}
}