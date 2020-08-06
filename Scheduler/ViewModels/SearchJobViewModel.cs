using Scheduler.Managers.Abstraction;
using Scheduler.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Scheduler.ViewModels
{
    public class SearchJobViewModel :BaseViewModel
    {
        public IEnumerable<Job> jobs { get; private set; }
        public SearchJobViewModel(INavigationManager navigationManager)
            :base(navigationManager)
        {
        }

        public override Task InitializeAsync(object data)
        {
            return base.InitializeAsync(data);
        }
    }
}
