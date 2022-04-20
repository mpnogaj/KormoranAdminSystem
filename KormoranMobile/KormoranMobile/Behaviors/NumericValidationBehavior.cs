using System.Linq;
using Xamarin.Forms;

namespace KormoranMobile.Behaviors
{
    public class NumericValidationBehavior : Behavior<Entry>
    {
        protected override void OnAttachedTo(Entry entry)
        {
            entry.TextChanged += ValidateText;
            base.OnAttachedTo(entry);
        }

        protected override void OnDetachingFrom(Entry entry)
        {
            entry.TextChanged -= ValidateText;
            base.OnDetachingFrom(entry);
        }

        private void ValidateText(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(e.NewTextValue)) return;
            bool valid = e.NewTextValue.ToCharArray().All(c => char.IsDigit(c));
            ((Entry)sender).Text = valid ? e.NewTextValue : e.OldTextValue;
        }
    }
}