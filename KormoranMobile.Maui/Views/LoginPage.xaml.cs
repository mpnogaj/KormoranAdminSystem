using KormoranMobile.Maui.ViewModels;

namespace KormoranMobile.Maui.Views;

public partial class LoginPage : ContentPage
{
	public LoginPage()
	{
		InitializeComponent();
		this.BindingContext = new LoginPageViewModel();
	}
}