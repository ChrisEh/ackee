using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ackee.Data.Model;

namespace Ackee.Shared.Services
{
    public class SortService
    {
        public List<AspNetTasks> OrderTasksBy(List<AspNetTasks> listOfTasks, string orderOption)
        {
            return orderOption switch
            {
                "START_DATE_ASC" => listOfTasks.OrderBy(t => t.StartDate).ToList(),
                "START_DATE_DESC" => listOfTasks.OrderByDescending(t => t.StartDate).ToList(),
                "END_DATE_ASC" => listOfTasks.OrderBy(t => t.EndDate).ToList(),
                "END_DATE_DESC" => listOfTasks.OrderByDescending(t => t.EndDate).ToList(),
                "TASK_NAME_ASC" => listOfTasks.OrderBy(t => t.TaskName).ToList(),
                "TASK_NAME_DESC" => listOfTasks.OrderByDescending(t => t.TaskName).ToList(),
                _ => listOfTasks.OrderBy(t => t.EndDate).ToList(),
            };
        }

        public List<OrderOption> GetOrderOptions()
        {
            List<OrderOption> orderOptions = new List<OrderOption>()
            {
                new OrderOption("DEFAULT", "Default (End Date: Ascending)"),
                new OrderOption("START_DATE_ASC", "Start Date: Ascending"),
                new OrderOption("START_DATE_DESC", "Start Date: Descending"),
                new OrderOption("END_DATE_ASC", "End Date: Ascending"),
                new OrderOption("END_DATE_DESC", "End Date: Descending"),
                new OrderOption("TASK_NAME_ASC", "Task Name: Ascending"),
                new OrderOption("TASK_NAME_DESC", "Task Name: Descending")
            };

            return orderOptions;
        }
    }
}
