using Xamarin.Forms;
using System.Windows.Input;
using System.Collections.ObjectModel;
using CustomControls.Models;

namespace CustomControls.ViewModels
{
    public class FabMenuViewModel : BaseViewModel
    {
        
        public FabMenuViewModel()
        {
            ObservableCollection<MenuListModel> items = new ObservableCollection<MenuListModel>();
			MenuListModel rachael = new MenuListModel();
			rachael.Name = "Rachael Stone";
			items.Add(rachael);

			MenuListModel kira = new MenuListModel();
			kira.Name = "Kira Russell";
			items.Add(kira);

			MenuListModel rodney = new MenuListModel();
			rodney.Name = "Rodney Weber";
			items.Add(rodney);

			MenuListModel chris = new MenuListModel();
			chris.Name = "Christian Nichols";
			items.Add(chris);

			MenuListModel summer = new MenuListModel();
			summer.Name = "Summer Graham";
			items.Add(summer);

			MenuListItems = items;
        }

		public ICommand ToggleViewMenuCommand => new Command(ToggleViewMenu);
		public ICommand SelectViewCommand => new Command((e) => { SelectView(e); });


		private void ToggleViewMenu()
		{
			if (IsLoading || IsBusy)
				return;

			if (IsViewMenuShown)
			{

			}
			else
			{

			}

			IsViewMenuShown = !IsViewMenuShown;
		}

		private void SelectView(object param)
		{
			IsViewMenuShown = false;

		}

		private bool _isViewMenuShown = false;
		public bool IsViewMenuShown
		{
			get => _isViewMenuShown;
			set
			{
				_isViewMenuShown = value;
				OnPropertyChanged();
			}
		}

		private ObservableCollection<MenuListModel> _menuListItems = new ObservableCollection<MenuListModel>();
		public ObservableCollection<MenuListModel> MenuListItems
		{
			get => _menuListItems;
			set
			{
				_menuListItems = value;
				OnPropertyChanged();
			}
		}
    }
}
