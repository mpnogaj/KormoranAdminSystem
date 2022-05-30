using KormoranMobile.Maui.ViewModels;

namespace KormoranMobile.Maui.Views;

public partial class EditScoresPage : ContentPage
{
	public EditScoresPage()
	{
		InitializeComponent();
		this.BindingContext = new EditScoresPageViewModel();
	}
}