using CustomControls.Helpers;

namespace CustomControls.Models
{
    public class MenuListModel : NotifyPropertyChangedHelper
    {
        public string _name = string.Empty;
		public string Name
		{
			get => _name;
			set
			{
				_name = value;
				OnPropertyChanged();
			}
		}

		public bool _isSelected = false;
		public bool IsSelected
		{
			get => _isSelected;
			set
			{
				_isSelected = value;
				OnPropertyChanged();
			}
		}
    }
}
