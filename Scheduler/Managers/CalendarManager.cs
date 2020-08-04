using Scheduler.Constants;
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
        public CalendarManager(IDataRepository dataRepository)
        {
            DataRepository = dataRepository;
        }
        public async Task<IEnumerable<ScheduledJob>> GetJobsForSelectedDate(DateTime selectedDate)
        {
            var urlPath = GetUriBuilder(ApiConstants.GetScheduledJobs);
            return await DataRepository.GetAsync<IEnumerable<ScheduledJob>>($"{urlPath}/{selectedDate.Date:yyyy-MM-dd}");
        }
        private string GetUriBuilder(string path)
           => new UriBuilder(ApiConstants.BaseApiUrl) { Path = path }.ToString();
    }
}
