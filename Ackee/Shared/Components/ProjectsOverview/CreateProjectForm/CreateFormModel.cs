using System;
using System.ComponentModel.DataAnnotations;

namespace Ackee.Shared.Components.ProjectsOverview.CreateProjectForm
{
    public class CreateFormModel
    {
        [Required]
        public string ProjectName { get; set; }
    }
}
