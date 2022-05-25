using KormoranMobile.Maui.ViewModels;

namespace KormoranMobile.Maui.Views;

public partial class TournamentsPage : ContentPage
{
	public TournamentsPage()
	{
		InitializeComponent();
		this.BindingContext = new TournamentsPageViewModel();
	}
}