using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace CustomControls.Helpers
{
	public class NotifyPropertyChangedHelper : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;

		protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}

		public void OnAllPropertiesChanged()
		{
			OnPropertyChanged(string.Empty);
		}
	}
}
