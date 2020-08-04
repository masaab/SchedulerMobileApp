using Scheduler.Models;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using System.Threading.Tasks;
using Scheduler.Managers.Abstraction;
using Scheduler.Extensions;
using System;

namespace Scheduler.ViewModels
{
    public class ClientViewModel : BaseViewModel
    {
        private ObservableCollection<Client> _clients { get; set; } 
        private IClientManager ClientManager { get; }
        
        public ClientViewModel(INavigationManager navigationManager, IClientManager clientManager)
            :base(navigationManager)
        {
            Title = "Client";
            Clients = new ObservableCollection<Client>();
            ClientManager = clientManager;
        }

        private DateTime _searchDate = DateTime.Today;
        public DateTime SearchDate 
        {
            get => _searchDate;
            set
            {
                _searchDate = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<Client> Clients 
        {
            get =>  _clients;
            set
            {
                _clients = value;
                OnPropertyChanged();
            }
        }

        public Command LoadClientsCommand => new Command(async () => await ExecuteLoadClientsCommand());
        public Command DeleteClientCommand => new Command<Client>(async (T) => await ExecuteDeleteClientCommand(T));
        public Command SelectedItemCommand => new Command<Client>(OnClientSelected);


        public Command NavigateToClientDetailCommand => new Command(async () =>  await OnClientDetailNavigate());

        private async Task OnClientDetailNavigate()
        {
            await NavigationManager.NavigateToAsync<NewClientViewModel>();
        }

        private void OnClientSelected(Client obj)
        {
            NavigationManager.NavigateToAsync<ClientDetailViewModel>(obj);
        }

        private async Task ExecuteDeleteClientCommand(Client client)
        {
            Clients.Remove(client);
            await ClientManager.DeleteClient(client.ID);
        }

        public override async Task InitializeAsync(object data)
        {
            await ExecuteLoadClientsCommand();
        }

        private async Task ExecuteLoadClientsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                Clients = (await ClientManager.GetAllClient()).ToObservableCollection();
            }
            catch (System.Exception)
            {
                throw;
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
