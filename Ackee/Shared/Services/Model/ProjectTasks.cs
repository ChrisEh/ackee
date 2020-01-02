using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ackee.Data.Model;

namespace Ackee.Shared.Services.Model
{
    public class ProjectTasks
    {
        public string ProjectID { get; set; }
        public string ProjectName { get; set; }
        public List<AspNetTasks> ListOfTasks { get; set; }

        public ProjectTasks()
        {
            ListOfTasks = new List<AspNetTasks>();
        }
    }
}
