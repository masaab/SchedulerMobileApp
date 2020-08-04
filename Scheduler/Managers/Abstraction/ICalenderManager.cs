using Scheduler.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Scheduler.Managers.Abstraction
{
    public interface ICalenderManager
    {
        public Task<IEnumerable<ScheduledJob>> GetJobsForSelectedDate(DateTime selectedDate);
    }
}
