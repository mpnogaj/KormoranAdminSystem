using KormoranMobile.Maui.ViewModels;
using KormoranMobile.Maui.ViewModels.Pages;

namespace KormoranMobile.Maui.Views.Pages;

public partial class MatchesPage : ContentPage
{
	public MatchesPage()
	{
		InitializeComponent();
		this.BindingContext = new MatchesPageViewModel();
	}
}