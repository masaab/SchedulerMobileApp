using Scheduler.Managers.Abstraction;
using Scheduler.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Scheduler.ViewModels
{
    public class NewJobViewModel : BaseViewModel
    {
        private IJobManager JobManager { get; }
        private NewJob _job { get; set; }
        private bool isVisible;
        public NewJobViewModel(INavigationManager navigationManager, IJobManager jobManager)
            :base(navigationManager)
        {
            JobManager = jobManager;
            _job = new NewJob();
            isVisible = true;
            Title = "New Job";
        }
        public List<JobType> JobTypes { get; set; }
            = Enum.GetValues(typeof(JobType)).Cast<JobType>().ToList();
        public Command OnSaveJobCommand => new Command<Object>(async (T) => await CreateNewJob(T));

        public NewJob Job
        {
            get => _job;
            set => _job = value;
        }

        public string ClientID
        {
            get;set;
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

        private async Task CreateNewJob(object dataForm)
        {

            var dataFormLayout = dataForm as Syncfusion.XForms.DataForm.SfDataForm;
            var isValid = dataFormLayout.Validate();
            dataFormLayout.Commit();
            if (!isValid)
            {
                await Application.Current.MainPage.DisplayAlert("Alert", "Please enter valid details", "Ok");
                return;
            }

            var newJob = dataFormLayout.DataObject as NewJob;
            var job = await JobManager.AddJob(new Job
            {
                ID = Guid.NewGuid().ToString(),
                ClientID = ClientID,
                Description = newJob.Description,
                Quote = newJob.Quote,
                ScheduledOn = newJob.ScheduledOn,
                Type  = newJob.Type,
                ScheduleTime = newJob.ScheduledOn.TimeOfDay,
                Reminder = newJob.Reminder,
            });

            await NavigationManager.NavigateToAsync<JobViewModel>(ClientID).ConfigureAwait(false);
        }

        public override async Task InitializeAsync(object data)
        {
            ClientID = (string)data;
        }
    }
}
