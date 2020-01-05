using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ackee.Data.Model;

namespace Ackee.Shared.Services.Model
{
    public class DayProjects
    {
        public DateTime Date { get; set; }
        public List<ProjectTasks>ProjectsWithTasks { get; set; }

        public DayProjects()
        {
            ProjectsWithTasks = new List<ProjectTasks>();
        }
    }
}
