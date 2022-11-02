using System;
using System.Collections.Generic;
using System.Text;

namespace WebTracker.Model
{
    public class Job
    {
        public int Id { get; set; }
        public bool Status { get; set; }
        public DateTime Time { get; set; }
        public string CronExpression { get; set; }
        public string Parameter { get; set; }
        public string JobName { get; set; }
        public string ErrorMessage { get; set; }
    }
}
