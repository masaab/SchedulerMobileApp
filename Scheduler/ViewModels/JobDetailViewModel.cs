using Scheduler.Managers.Abstraction;
using Scheduler.Models;
using System.Threading.Tasks;

namespace Scheduler.ViewModels
{
    public class JobDetailViewModel : BaseViewModel
    {
        private Job _job;
        public JobDetailViewModel(INavigationManager manager)
            :base(manager)
        {
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
