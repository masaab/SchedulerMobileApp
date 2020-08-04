using Scheduler.Managers.Abstraction;
using Scheduler.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.ViewModels
{
    public class SearchJobViewModel :BaseViewModel
    {
        private INavigationManager NavigationManager { get; }
        public IEnumerable<Job> jobs { get; private set; }
        public SearchJobViewModel(INavigationManager navigationManager)
            :base(navigationManager)
        {
            NavigationManager = navigationManager;
        }

        public override Task InitializeAsync(object data)
        {
            return base.InitializeAsync(data);
        }
    }
}
