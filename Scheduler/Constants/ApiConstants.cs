namespace Scheduler.Constants
{
    public class ApiConstants
    {
        public static string BaseApiUrl = App.AzureBackendUrl.ToString();
        public const string SchedulerBaseUrl = "/api/Clients";
        public const string GetScheduledJobs = "api/ScheduledJob";
        public const string HttpScheduledJobFunctionUrl = "https://schedulerhttpfunctionccfnr.azurewebsites.net/api/SchedulerJob?code=soNvs2ugPG50601swGAncn8/sh/8iplybwXLqB0NRZSsLsWm2FxgeA==";//"http://10.0.2.2:7071/api/SchedulerJob";
        

    }
}
