using Scheduler.Managers.Abstraction;
using Scheduler.Models;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Scheduler.ViewModels
{
    public class NewClientViewModel : BaseViewModel
    {
        private IClientManager ClientManager { get; }
        private NewClient _Client { get; set; }

        private bool isVisible;

        
        public Command OnSaveClientCommand => new Command<object>(async (T) => await OnClientSave(T));

        public NewClientViewModel(INavigationManager navigationManager, IClientManager clientManager)
            : base(navigationManager)
        {
            ClientManager = clientManager;
            Client = new NewClient();
            isVisible = true;
            Title = "New Client";
        }

        public NewClient Client
        {
            get { return _Client; }
            set { _Client = value; }
        }

        public bool IsVisible 
        { 
            get 
            {
                return isVisible;
            }
            set
            {
                isVisible = value;
                OnPropertyChanged();
            }
        }

        public async Task OnClientSave(object dataForm)
        {
            var dataFormLayout = dataForm as Syncfusion.XForms.DataForm.SfDataForm;
            var isValid = dataFormLayout.Validate();
            dataFormLayout.Commit();
            if (!isValid)
            {
                await Application.Current.MainPage.DisplayAlert("Alert", "Please enter valid details", "Ok");
                return;
            }
            
            dataFormLayout.IsReadOnly = true;
            this.IsVisible = false;

            var newClient = dataFormLayout.DataObject as NewClient;
            var client = await ClientManager.AddClient(new Client
            {
                FullName = newClient.FullName,
                Email = newClient.Email,
                Address = newClient.Address,
                Phone = newClient.Phone
            });

            await NavigationManager.NavigateToAsync<ClientViewModel>(client.ID).ConfigureAwait(false);

        }

        public override Task InitializeAsync(object data)
        {
            return Task.FromResult(false);
        }
    }
}
