using KormoranMobile.ViewModels.Popups;
using KormoranShared.Models;

namespace KormoranMobile.Views.Popups;

public partial class EditScoresPopup
{
	public EditScoresPopup(Match match)
	{
		InitializeComponent();
		this.BindingContext = new EditScoresPopupViewModel(match, this.Close);
	}
}