using KormoranMobile.Maui.ViewModels.Pages;

namespace KormoranMobile.Maui.Views.Pages;

public partial class TournamentsPage : ContentPage
{
	public TournamentsPage()
	{
		InitializeComponent();
		this.BindingContext = new TournamentsPageViewModel();
	}

	protected override void OnAppearing()
	{
		base.OnAppearing();
		((TournamentsPageViewModel)this.BindingContext).OnAppearing();
	}
}