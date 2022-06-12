using CommunityToolkit.Maui.Views;
using KormoranMobile.ViewModels.Popups;

namespace KormoranMobile.Views.Popups;

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