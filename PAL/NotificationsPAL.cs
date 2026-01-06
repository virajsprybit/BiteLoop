namespace PAL
{
    using System;
    using System.Runtime.CompilerServices;

    public class NotificationsPAL
    {
        public long ID { get; set; }
        public string Title { get; set; }
        public string Groups { get; set; }
        public string Message { get; set; }
        public string Users { get; set; }
        public string Vendors { get; set; }
        public string SalesAdmin { get; set; }
        public int IsSchedule { get; set; }
        public DateTime ScheduleTime{ get; set; } 
    }
}

