using System;
using System.ComponentModel.DataAnnotations;

namespace Ackee.Shared.Components.ProjectDashboard.Milestones.UpdateMilestoneForm
{
    public class UpdateFormModel
    {
        [Required]
        public string MilestoneName { get; set; }

        public string MilestoneDescription { get; set; }

        public DateTime MilestoneEndDate { get; set; }

        public bool MilestoneCompleted { get; set; }
    }
}
