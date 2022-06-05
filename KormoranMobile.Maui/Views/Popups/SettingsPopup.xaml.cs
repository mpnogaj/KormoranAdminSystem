using CommunityToolkit.Maui.Views;
using KormoranMobile.Maui.ViewModels.Popups;

namespace KormoranMobile.Maui.Views.Popups;

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