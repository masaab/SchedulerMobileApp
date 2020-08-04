using Scheduler.Managers.Abstraction;
using Scheduler.Models;
using System.Threading.Tasks;

namespace Scheduler.ViewModels
{
    public class JobDetailViewModel : BaseViewModel
    {
        private Job _job;
        private INavigationManager NavigationManager { get; }
        public JobDetailViewModel(INavigationManager manager)
            :base(manager)
        {
            NavigationManager = manager;
        }

        public Job Job 
        {
            get => _job;
            set 
            {
                _job = value;
                OnPropertyChanged();
            }
        }

        public override async Task InitializeAsync(object data)
            => Job = (Job)data;
    }
}
