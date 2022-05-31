using CommunityToolkit.Maui.Views;
using KormoranMobile.Maui.ViewModels;
using KormoranMobile.Maui.ViewModels.Popups;

namespace KormoranMobile.Maui.Views.Popups;

public partial class LoginPopup : Popup
{
	public LoginPopup()
	{
		InitializeComponent();
		this.BindingContext = new LoginPopupViewModel
		{
			ClosePopup = () => this.Close()
		};
	} 
}