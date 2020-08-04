using System;

namespace Scheduler.Models
{
    public class Job
    { 
        public string ID { get; set; } = Guid.Empty.ToString();
        public string Description { get; set; }
        public float Quote { get; set; }
        public DateTime ScheduledOn { get; set; }
        public JobType Type { get; set; } 
        public TimeSpan ScheduleTime 
        {
            get 
            {
                return ScheduledOn.TimeOfDay;
            }
            set
            {
                ScheduledOn = ScheduledOn.Date + value;
            }
        }
        public bool Reminder { get; set; } = true;
        public string ClientID { get; set; }
    }

    public enum JobType
    {
        Week,
        Fortnight,
        ThirdWeek,
        Monthly
    };
}
