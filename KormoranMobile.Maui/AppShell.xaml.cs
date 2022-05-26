using KormoranMobile.Maui.Helpers;
using KormoranMobile.Maui.Views;

namespace KormoranMobile.Maui
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute($"{nameof(TournamentsPage)}/{nameof(LoginPage)}", typeof(LoginPage));
        }
    }
}