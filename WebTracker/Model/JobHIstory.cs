using System;
using System.Collections.Generic;
using System.Text;

namespace WebTracker.Model
{
    internal class JobHIstory
    {
        public int Id { get; set; }
        public DateTime CreatedOn { get; set; }
        public string JobName { get; set; }
        public string Result { get; set; }
        public int JobId { get; set; }
    }
}
