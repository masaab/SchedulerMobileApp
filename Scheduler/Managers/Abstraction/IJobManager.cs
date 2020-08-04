using Scheduler.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Scheduler.Managers.Abstraction
{
    public interface IJobManager
    {
        Task<Job> GetSingleJob();

        Task<Job> AddJob(Job job);

        Task DeleteJob(Job job);

        Task<IEnumerable<Job>> GetJobsForSingleClient(string clientId);
    }
}
