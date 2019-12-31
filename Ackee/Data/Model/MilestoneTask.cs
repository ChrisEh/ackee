using System;
using System.ComponentModel.DataAnnotations;

namespace Ackee.Data.Model
{
    public class MilestoneTask
    {
        public string MilestoneID { get; set; }
        public AspNetMilestones Milestone { get; set; }
        public string TaskID { get; set; }
        public AspNetTasks Task { get; set; }
    }
}
