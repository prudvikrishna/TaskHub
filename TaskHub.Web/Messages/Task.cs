using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TaskHub.Web.Messages
{
    public class Task
    {
        public System.Guid TaskId { get; set; }
        public string Title { get; set; }
        public string Status { get; set; }
        public string Priority { get; set; }
        public System.DateTime DueDate { get; set; }
    }
}