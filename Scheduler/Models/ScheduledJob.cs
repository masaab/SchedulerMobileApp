using System;

namespace Scheduler.Models
{
    public class ScheduledJob
    {
        public Guid Id { get; set; }
        public DateTime JobDate { get; set; }
        public string Description { get; set; }
        public string ClientName { get; set; }
        public string ClientAddress { get; set; }

    }
}
