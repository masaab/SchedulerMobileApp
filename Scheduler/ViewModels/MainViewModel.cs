using Scheduler.Managers.Abstraction;
using System.Threading.Tasks;

namespace Scheduler.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private MenuViewModel _menuViewModel;
        public MainViewModel(INavigationManager navigationManager, MenuViewModel menuViewModel)
            : base(navigationManager)
        {
            MenuViewModel = menuViewModel;
        }

        public MenuViewModel MenuViewModel 
        { 
            get => _menuViewModel;
            set 
            {
                _menuViewModel = value;
                OnPropertyChanged();
            } 
        }

        public override async Task InitializeAsync(object data)
        {
            await Task.WhenAll
                (
                _menuViewModel.InitializeAsync(data),
                 NavigationManager.NavigateToAsync<ClientViewModel>()
                ); 
        }
    }
}
