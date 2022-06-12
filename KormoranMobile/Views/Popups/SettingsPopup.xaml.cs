using CommunityToolkit.Maui.Views;
using KormoranMobile.ViewModels.Popups;

namespace KormoranMobile.Views.Popups;

public partial class SettingsPopup : Popup
{
	public SettingsPopup()
	{
		InitializeComponent();
		this.BindingContext = new SettingsPopupViewModel
		{
			ClosePopup = (string? res) => this.Close(res)
		};
	}
}