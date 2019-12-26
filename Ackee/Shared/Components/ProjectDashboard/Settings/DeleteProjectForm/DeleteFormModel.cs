using System;
using System.ComponentModel.DataAnnotations;

namespace Ackee.Shared.Components.ProjectDashboard.Settings.DeleteProjectForm
{
    public class DeleteFormModel
    {
        [Required]
        public string ProjectName { get; set; }
    }
}
