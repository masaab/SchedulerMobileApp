using System;

namespace Scheduler.Models
{
    public class Client 
    {
        public string ID { get; set; } = Guid.Empty.ToString();

        public string FullName { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }
        
        public string Address { get; set; }
    }
}
