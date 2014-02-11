
namespace TaskHub.Web.Models
{
    using System;
    using System.Data;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class TaskHubEntities : DbContext, ITaskHubContext
    {
        public TaskHubEntities()
            : base("name=DefaultConnection")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Tasks>().HasRequired(x => x.AssignedUser).WithMany(u => u.TaskAssigned).HasForeignKey(m => m.AssignedTo);
            modelBuilder.Entity<Tasks>().HasRequired(x => x.RequestedUser).WithMany(u => u.TaskRequested).HasForeignKey(m => m.RequestedBy);
            //base.OnModelCreating(modelBuilder);
        }
    
        public IDbSet<Tasks> Tasks { get; set; }
        public IDbSet<UserProfile> UserProfile { get; set; }

        public void MarkAsModified(Tasks item)
        {
            Entry(item).State = EntityState.Modified;
        }

    }
}
