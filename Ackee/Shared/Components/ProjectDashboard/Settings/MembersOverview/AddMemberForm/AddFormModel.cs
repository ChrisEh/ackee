using System;
using System.ComponentModel.DataAnnotations;

namespace Ackee.Shared.Components.ProjectDashboard.Settings.MembersOverview.AddMemberForm
{
    public class AddFormModel
    {
        [Required]
        public string UserEmail { get; set; }
    }
}
