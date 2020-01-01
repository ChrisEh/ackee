using System;
using System.ComponentModel.DataAnnotations;

namespace Ackee.Shared.Components.ProjectDashboard.Tasks.UpdateTaskForm
{
    public class UpdateFormModel
    {
        [Required]
        public string TaskName { get; set; }

        public string TaskDescription { get; set; }

        public DateTime TaskStartDate { get; set; }

        public DateTime TaskEndDate { get; set; }

        public bool TaskCompleted { get; set; }
    }
}
