using KormoranMobile.Maui.ViewModels;

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
		Task.Run(() =>
			((TournamentsPageViewModel)this.BindingContext).RefreshTournamentsListCommand.ExecuteAsync()
		);
	}
}