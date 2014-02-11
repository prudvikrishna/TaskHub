
namespace TaskHub.Web.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    
    public partial class Tasks
    {
        [Key]
        public System.Guid TaskId { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Status { get; set; }
        [Required]
        public string Priority { get; set; }
        [Required]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        public System.DateTime DueDate { get; set; }
        public int AssignedTo { get; set; }
        public int RequestedBy { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public Nullable<System.DateTime> LastModifiedDate { get; set; }

        
        [ForeignKey("AssignedTo")]
        public virtual UserProfile AssignedUser { get; set; }
        
        [ForeignKey("RequestedBy")]
        public virtual UserProfile RequestedUser { get; set; }
    }
}
