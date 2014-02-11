using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;

namespace TaskHub.Web.Models
{
    public interface ITaskHubContext : IDisposable
    {
        IDbSet<Tasks> Tasks { get; set; }
        IDbSet<UserProfile> UserProfile { get; set; }
        int SaveChanges();
        void MarkAsModified(Tasks item);

    }
}
