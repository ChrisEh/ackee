using System;
using System.ComponentModel.DataAnnotations;

namespace Ackee.Shared.Components.ProjectDashboard.Settings.UpdateProjectInformationForm
{
    public class UpdateFormModel
    {
        [Required]
        public string ProjectName { get; set; }

        public string ProjectDescription { get; set; }
    }
}
