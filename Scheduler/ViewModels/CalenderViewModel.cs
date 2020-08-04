using Scheduler.Managers.Abstraction;
using System.Threading.Tasks;

namespace Scheduler.ViewModels
{
    public class CalenderViewModel : BaseViewModel
    {
        public CalenderViewModel(INavigationManager navigationManager)
            :base(navigationManager)
        {
            
        }

        public override async Task InitializeAsync(object data)
            => await Task.Run(() => { Title = "My Calender"; });
    }
}
