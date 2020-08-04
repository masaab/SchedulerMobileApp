using Scheduler.Managers.Abstraction;
using Scheduler.Models;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace Scheduler.ViewModels
{
    public class MenuViewModel :BaseViewModel
    {
        private ObservableCollection<MainMenuItem> _menuItems;
        public MenuViewModel(INavigationManager navigationManager)
            :base(navigationManager)
        {
            MenuItems = new ObservableCollection<MainMenuItem>();
            LoadMenuItem();
        }

        public ICommand MenuItemTappedCommand => new Command(OnMenuItemTapped);
        public ObservableCollection<MainMenuItem> MenuItems
        {
            get => _menuItems;
            set
            {
                _menuItems = value;
                OnPropertyChanged();
            }
        }

        private void OnMenuItemTapped(object menuItemTappedEventArgs)
        {
            var menuItem = ((menuItemTappedEventArgs as ItemTappedEventArgs)?.Item as MainMenuItem);

            var type = menuItem?.ViewModelToLoad;
            NavigationManager.NavigateToAsync(type).ConfigureAwait(true);
        }

        private void LoadMenuItem()
        {
            try
            {
                MenuItems.Add(new MainMenuItem
                {
                    MenuItemType = Enums.MenuItemType.Client,
                    MenuText = "Client",
                    ViewModelToLoad = typeof(ClientViewModel)
                });
                MenuItems.Add(new MainMenuItem
                {
                    MenuItemType = Enums.MenuItemType.Calender,
                    MenuText = "Calender",
                    ViewModelToLoad = typeof(CalenderViewModel)
                });
            }
            catch (Exception e)
            {
                
            }
        }
    }
}
