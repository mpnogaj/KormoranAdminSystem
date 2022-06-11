using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace KormoranMobile.Maui.ViewModels.Abstraction
{
	public abstract class ViewModelBase : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler? PropertyChanged;

		public void OnPropertyChanged([CallerMemberName] string? propertyName = null)
		{
			if(propertyName == null)
			{
				throw new ArgumentNullException(nameof(propertyName));
			}
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}

		protected void SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = "")
		{
			if (EqualityComparer<T>.Default.Equals(storage, value)) return;
			storage = value;
			this.OnPropertyChanged(propertyName);
		}
	}
}