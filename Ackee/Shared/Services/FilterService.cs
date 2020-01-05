using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ackee.Data.Model;

namespace Ackee.Shared.Services
{
    public class FilterService
    {
        public List<AspNetTasks> FindInTasks(List<AspNetTasks> taskList, string searchTerm)
        {
            return taskList.Where(t => t.TaskName.ToLower().Contains(searchTerm.ToLower()) || 
                t.UserTasks.Any(ut => ut.User.Email.ToLower().Contains(searchTerm.ToLower())) ||
                t.MilestoneTasks.Any(mt => mt.Milestone.MilestoneName.ToLower().Contains(searchTerm.ToLower())))
                .ToList();
        }
    }
}
