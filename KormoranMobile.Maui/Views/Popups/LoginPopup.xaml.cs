using CommunityToolkit.Maui.Views;
using KormoranMobile.Maui.ViewModels;

namespace KormoranMobile.Maui.Views.Popups;

public partial class LoginPopup : Popup
{
	public LoginPopup()
	{
		InitializeComponent();
		this.BindingContext = new LoginPageViewModel
		{
			ClosePopup = () => this.Close()
		};
	} 
}