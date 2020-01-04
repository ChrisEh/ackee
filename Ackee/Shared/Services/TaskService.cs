using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ackee.Data.Model;
using Ackee.Shared.Services.Model;
using Ackee.Shared.Services;

namespace Ackee.Shared.Services
{
    public class TaskService
    {
        private DateTimeService dateService;

        public TaskService(DateTimeService dateService)
        {
            this.dateService = dateService;
        }

        public List<List<AspNetTasks>> GetTasksGroupedPerDay(List<AspNetTasks> listOfTasks)
        {
            return listOfTasks
                .GroupBy(t => t.EndDate)
                .Select(day => day.ToList())
                .ToList();
        }

        public List<List<AspNetTasks>> GetTasksGroupedPerProject(List<AspNetTasks> listOfTasks)
        {
            return listOfTasks
                .GroupBy(t => t.Project.ProjectID)
                .Select(group => group.ToList())
                .ToList();
        }

        public List<DayProjects> GetTasksGroupedPerDayPerProject(List<AspNetTasks> listOfTasks)
        {
            List<DayProjects> tasksGroupedPerDayPerProject = new List<DayProjects>();

            List<AspNetTasks> distinctDays = listOfTasks
                .GroupBy(t => t.EndDate.Date)
                .Select(g => g.First())
                .OrderBy(t => t.EndDate)
                .ToList();

            foreach(var day in distinctDays)
            {
                DayProjects dayWithProjectsWithTasks = new DayProjects();
                dayWithProjectsWithTasks.Date = day.EndDate;

                List<AspNetTasks> dailyTasks = listOfTasks.Where(t => t.EndDate.Date == day.EndDate.Date).ToList();

                List<AspNetTasks> distinctProjects = dailyTasks
                    .GroupBy(t => t.Project.ProjectID)
                    .Select(g => g.First())
                    .ToList();

                foreach(var project in distinctProjects)
                {
                    ProjectTasks projectWithDailyTasks = new ProjectTasks();
                    projectWithDailyTasks.ProjectName = project.Project.ProjectName;
                    projectWithDailyTasks.ProjectID = project.Project.ProjectID;
                    projectWithDailyTasks.ListOfTasks = dailyTasks.Where(t => t.Project.ProjectID == project.Project.ProjectID).ToList();

                    dayWithProjectsWithTasks.ProjectsWithTasks.Add(projectWithDailyTasks);
                }

                tasksGroupedPerDayPerProject.Add(dayWithProjectsWithTasks);
            }

            return tasksGroupedPerDayPerProject;
        }

        public List<List<AspNetTasks>> GetTasksByWeekNrGroupedByDay(List<AspNetTasks> tasks, int weekNr)
        {
            List<DateTime> datesOfWeek = dateService.GetAllDatesPerWeek(weekNr);

            return tasks                            
                .Where(t => datesOfWeek.Contains(t.EndDate.Date))
                .OrderBy(t => t.EndDate)
                .GroupBy(t => t.EndDate.Date)
                .Select(g => g.ToList())
                .ToList();
        }
    }
}
