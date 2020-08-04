using Scheduler.Managers.Abstraction;
using Scheduler.Models;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Scheduler.ViewModels
{
    public class ClientDetailViewModel : BaseViewModel
    {

        private INavigationManager NavigationManager { get; }
        public Command NavigateToJobsCommand => new Command<Client>(async (T) => await NavigateToJobs(T));
        public ClientDetailViewModel(INavigationManager manager):
            base(manager)
        {
            NavigationManager = manager;
        }
        private async Task NavigateToJobs(Client client)
        {
            await NavigationManager.NavigateToAsync<JobViewModel>(client.ID);
        }

        private Client _client;
        public Client SelectedClient
        {
            get => _client;
            set 
            {
                _client = value;
                OnPropertyChanged();
            }
        
        }

        public override async Task InitializeAsync(object data)
        {
           SelectedClient = (Client) data;
        }
    }
}
