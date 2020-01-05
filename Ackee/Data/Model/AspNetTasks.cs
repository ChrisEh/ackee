using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ackee.Data.Model
{
    public class AspNetTasks
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string TaskID { get; set; }

        [StringLength(128)]
        public string TaskName { get; set; }

        [StringLength(256)]
        public string TaskDescription { get; set; }

        public AspNetProjects Project { get; set; }

        public IEnumerable<MilestoneTask> MilestoneTasks { get; set; }

        public IEnumerable<UserTask> UserTasks { get; set; }

        public IEnumerable<TaskLabel> TaskLabels { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public bool Completed { get; set; }
    }
}
