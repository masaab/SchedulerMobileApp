using Scheduler.Managers.Abstraction;
using Scheduler.Models;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Scheduler.ViewModels
{
    public class JobViewModel : BaseViewModel
    {
        public ObservableCollection<Job> Jobs { get; set; } 

        private IJobManager JobManager { get; }
        private string ClientId { get; set; }
        public Command LoadJobCommand => new Command(async () => await ExecuteLoadJobCommand());
        public Command SelectedItemCommand => new Command<Job>(OnJobSelected);
        public Command NavigateToAddJobCommand => new Command(async () => await OnJobAddCommand());
        public Command DeleteJobCommand => new Command<Job>(async (T) => await OnDeleteJobCommand(T));

        public JobViewModel(INavigationManager navigationManager, IJobManager jobManager)
            :base(navigationManager)
        {
            JobManager = jobManager;
            Jobs = new ObservableCollection<Job>();
        }

        private async Task OnJobAddCommand()
        {
            await NavigationManager.NavigateToAsync<NewJobViewModel>(ClientId);
        }

        private async Task OnDeleteJobCommand(Job job)
        {
            Jobs.Remove(job);
            await JobManager.DeleteJob(job);
        }

        private void OnJobSelected(Job obj)
        {
            NavigationManager.NavigateToAsync<JobDetailViewModel>(obj);
        }
        private async Task ExecuteLoadJobCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                Jobs.Clear();
                var items = await JobManager.GetJobsForSingleClient(ClientId);
                if (items != null)
                {
                    foreach (var item in items)
                    {
                        Jobs.Add(item);
                    }
                }
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

        public override async Task InitializeAsync(object data)
        {
            ClientId = (string)data;
            await ExecuteLoadJobCommand();
        }
    }
}
