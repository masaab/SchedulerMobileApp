using Scheduler.AppSetting;
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
        private AppSettingDetail AppSettingDetails { get; }
        public JobManager(IDataRepository dataRepository, 
            IApplicationSettingManager applicationSettingManager)
        {
            DataRepository = dataRepository;
            AppSettingDetails = applicationSettingManager.AppSettingDetails;
        }
        public async Task<Job> AddJob(Job job)
        {
            var urlPath = GetUriBuilder(AppSettingDetails.ClientBaseUrl);
            var newJob = await DataRepository.PostAsync($"{urlPath}/{job.ClientID}/jobs", job).ConfigureAwait(false);

            if(!string.IsNullOrEmpty(newJob.ID))
            {
                _ = await DataRepository.PostAsync($"{AppSettingDetails.HttpScheduledJobFunctionUrl}", new JobScheduled { JobId = newJob.ID, JobDate = newJob.ScheduledOn },new Result());
            }
            return job;
        }

        public async Task DeleteJob(Job job)
        {
            var urlPath = GetUriBuilder(AppSettingDetails.ClientBaseUrl);
             await DataRepository.DeleteAsync($"{urlPath}/{job.ClientID}/jobs/{job.ID}").ConfigureAwait(false);
        }

        public Task<IEnumerable<Job>> GetJobsForSingleClient(string clientId)
        {
            var urlPath = GetUriBuilder(AppSettingDetails.ClientBaseUrl);

            var data = DataRepository.GetAsync<IEnumerable<Job>>($"{urlPath}/{clientId}/jobs");

            return data;
        }

        public Task<Job> GetSingleJob()
        {
            throw new NotImplementedException();
        }

        private void ExecuteRequestInPollyService()
        {
            
        }
        private string GetUriBuilder(string path)
            => new UriBuilder(AppSettingDetails.BackendUrl) { Path = path }.ToString();
    }
}
