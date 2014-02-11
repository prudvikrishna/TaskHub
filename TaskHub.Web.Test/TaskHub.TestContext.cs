using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using TaskHub.Web.Models;

namespace TaskHub.Web.Test
{
    class TaskHubTestContext : ITaskHubContext
    {
        public TaskHubTestContext()
        {
            this.Tasks = new FakeTaskSet();
        }

        public IDbSet<Tasks> Tasks { get; set; }
        public IDbSet<UserProfile> UserProfile { get; set; }

        public int SaveChanges()
        {
            return 0;
        }

        public void MarkAsModified(Tasks item) { }

        public void Dispose() { }

    }
}
