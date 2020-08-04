using Syncfusion.SfCalendar.XForms;
using Xamarin.Forms;
using System;
using System.Collections.ObjectModel;
using Scheduler.Managers.Abstraction;
using System.Threading.Tasks;
using System.Linq;

namespace Scheduler.ViewModels
{
    public class WeekViewModel : BaseViewModel
    {
        private CalendarEventCollection appointments;
        private ICalenderManager CalenderManager { get; }

        public WeekViewModel(INavigationManager navigationManager, ICalenderManager calenderManger)
            :base(navigationManager)
        {
            CalenderManager = calenderManger;
            Appointments = new CalendarEventCollection();
            AddAppointments().ConfigureAwait(false);

        }

        public override async Task InitializeAsync(object data)
        {
           
        }
  
        public CalendarEventCollection Appointments
        {
            get
            {
                return appointments;
            }
            set
            {
                appointments = value;
                OnPropertyChanged("Appointments");
            }
        }

        private async Task AddAppointments()
        {
            try
            {
                var content = await CalenderManager.GetJobsForSelectedDate(DateTime.Now.Date).ConfigureAwait(false);
                content.ToList().ForEach(a => {
                    var appointment = new CalendarInlineEvent
                    {
                        Subject = a.ClientName,
                        Color = Color.Green,
                        StartTime = a.JobDate,
                        EndTime = a.JobDate.AddHours(2)
                    };
                    this.Appointments.Add(appointment);
                });
            }
            catch (Exception ex)
            {

            }
        }
    }
}
