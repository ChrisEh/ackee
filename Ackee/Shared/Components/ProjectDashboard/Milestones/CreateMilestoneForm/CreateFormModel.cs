using System;
using System.ComponentModel.DataAnnotations;

namespace Ackee.Shared.Components.ProjectDashboard.Milestones.CreateMilestoneForm
{
    public class CreateFormModel
    {
        [Required]
        public string MilestoneName { get; set; }

        public string MilestoneDescription { get; set; }

        public DateTime MilestoneEndDate { get; set; }
    }
}
