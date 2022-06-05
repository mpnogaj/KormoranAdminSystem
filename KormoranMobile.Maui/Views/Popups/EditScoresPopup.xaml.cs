using CommunityToolkit.Maui.Views;
using KormoranMobile.Maui.ViewModels.Popups;
using KormoranShared.Models;

namespace KormoranMobile.Maui.Views.Popups;

public partial class EditScoresPopup : Popup
{
	public EditScoresPopup(Match match)
	{
		InitializeComponent();
		this.BindingContext = new EditScoresPopupViewModel(match, (res) => this.Close(res));
	}
}