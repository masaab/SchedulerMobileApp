using Scheduler.Constants;
using Scheduler.Managers.Abstraction;
using Scheduler.Models;
using Scheduler.Repository;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Scheduler.Managers
{
    public class JobManager : IJobManager
    {
        private IDataRepository DataRepository { get; }
        public JobManager(IDataRepository dataRepository)
        {
            DataRepository = dataRepository;
        }
        public async Task<Job> AddJob(Job job)
        {
            var urlPath = GetUriBuilder(ApiConstants.SchedulerBaseUrl);
            var newJob = await DataRepository.PostAsync($"{urlPath}/{job.ClientID}/jobs", job).ConfigureAwait(false);
            if(!string.IsNullOrEmpty(newJob.ID))
            {
                var result = await DataRepository.PostAsync($"{ApiConstants.HttpScheduledJobFunctionUrl}", new JobScheduled { JobId = newJob.ID, JobDate = newJob.ScheduledOn },new Result());
            }
            return job;
        }

        public async Task DeleteJob(Job job)
        {
            var urlPath = GetUriBuilder(ApiConstants.SchedulerBaseUrl);
             await DataRepository.DeleteAsync($"{urlPath}/{job.ClientID}/jobs/{job.ID}").ConfigureAwait(false);
        }

        public Task<IEnumerable<Job>> GetJobsForSingleClient(string clientId)
        {
            var urlPath = GetUriBuilder(ApiConstants.SchedulerBaseUrl);

            var data = DataRepository.GetAsync<IEnumerable<Job>>($"{urlPath}/{clientId}/jobs");

            return data;
        }

        public Task<Job> GetSingleJob()
        {
            throw new NotImplementedException();
        }

        private string GetUriBuilder(string path)
            => new UriBuilder(ApiConstants.BaseApiUrl) { Path = path }.ToString();
    }
}
