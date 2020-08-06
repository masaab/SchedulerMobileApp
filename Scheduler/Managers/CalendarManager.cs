using Scheduler.AppSetting;
using Scheduler.Models;
using Scheduler.Repository;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Scheduler.Managers.Abstraction
{
    public class CalendarManager : ICalenderManager
    {
        private IDataRepository DataRepository { get; }
        private AppSettingDetail AppSettingDetails { get; }
        public CalendarManager(IDataRepository dataRepository, IApplicationSettingManager applicationSettingManager)
        {
            DataRepository = dataRepository;
            AppSettingDetails = applicationSettingManager.AppSettingDetails;
        }
        public async Task<IEnumerable<ScheduledJob>> GetJobsForSelectedDate(DateTime selectedDate)
        {
            var urlPath = GetUriBuilder(AppSettingDetails.GetScheduledJobs);
            return await DataRepository.GetAsync<IEnumerable<ScheduledJob>>($"{urlPath}/{selectedDate.Date:yyyy-MM-dd}");
        }
        private string GetUriBuilder(string path)
           => new UriBuilder(AppSettingDetails.BackendUrl) { Path = path }.ToString();
    }
}
