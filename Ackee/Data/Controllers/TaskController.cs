using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ackee.Data.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoreLinq;

namespace Ackee.Data.Controllers
{
    [Produces("application/json")]
    [Route("api/tasks")]
    public class TaskController : ControllerBase
    {
        // GET api/projects
        [HttpGet]
        public async Task<IEnumerable<AspNetTasks>> GetAllTasks()
        {
            using (var ctx = new AckeeCtx())
            {
                var users = ctx.ApplicationUser.ToList();
                var milestones = ctx.Milestones.ToList();

                return await ctx.Tasks
                    .Include(t => t.MilestoneTasks)
                    .Include(t => t.UserTasks)
                    .Include(t => t.Project)
                    .Include(t => t.Project.UserProjects)
                    .ToListAsync();
            }            
        }

        [HttpPost]
        public async Task<object> CreateTask([FromBody] AspNetTasks task)
        {
            using (var ctx = new AckeeCtx())
            {
                var existingTask = ctx.Tasks.FirstOrDefault(t => t.TaskName == task.TaskName && t.Project.ProjectID == task.Project.ProjectID);
                var project = ctx.Projects.FirstOrDefault(p => p.ProjectID == task.Project.ProjectID);

                if (task == null || existingTask != null || project == null)
                {
                    return BadRequest();
                }

                task.Project = project;

                ctx.Tasks.Add(task);
                await ctx.SaveChangesAsync();
                return true;
            }
        }

        [HttpGet("{taskId}")]
        public async Task<AspNetTasks> GetTaskById(string taskId)
        {
            using (var ctx = new AckeeCtx())
            {
                var users = ctx.ApplicationUser.ToList();
                var milestones = ctx.Milestones.ToList();

                return await ctx.Tasks
                    .Include(t => t.MilestoneTasks)
                    .Include(t => t.UserTasks)
                    .Include(t => t.Project)
                    .Include(t => t.Project.UserProjects)
                    .FirstOrDefaultAsync(t => t.TaskID == taskId);
            }
        }

        [HttpDelete("{taskId}")]
        public async Task<object> DeleteTaskById(string taskId)
        {
            using (var ctx = new AckeeCtx())
            {
                var task = ctx.Tasks.FirstOrDefault(t => t.TaskID == taskId);

                if ( task == null)
                {
                    return BadRequest();
                }

                ctx.Tasks.Remove(task);
                await ctx.SaveChangesAsync();
                return true;
            }
        }

        [HttpPut("{taskId}")]
        public async Task<object> UpdateTask([FromRoute] string taskId, [FromBody] AspNetTasks updatedTask)
        {
            using (var ctx = new AckeeCtx())
            {
                var task = ctx.Tasks.FirstOrDefault(t => t.TaskID == updatedTask.TaskID);
                
                if (task == null)
                {
                    return BadRequest();
                }

                task.TaskName = updatedTask.TaskName;
                task.TaskDescription = updatedTask.TaskDescription;
                task.StartDate = updatedTask.StartDate;
                task.EndDate = updatedTask.EndDate;
                task.Completed = updatedTask.Completed;

                await ctx.SaveChangesAsync();
                return true;
            }
        }        

        // Remove milestone from task
        [HttpDelete("{taskId}/milestones/{milestoneId}")]
        public async Task<ActionResult<bool>> RemoveMilestoneFromTask(string taskId, string milestoneId)
        {
            using (var ctx = new AckeeCtx())
            {
                var task = await ctx.Tasks.FirstOrDefaultAsync(t => t.TaskID == taskId);
                var milestone = await ctx.Milestones.FirstOrDefaultAsync(m => m.MilestoneID == milestoneId);
                var milestoneTask = await ctx.MilestoneTasks.FirstOrDefaultAsync(mt => mt.MilestoneID == milestoneId && mt.TaskID == taskId);

                if (task == null || milestone == null || milestoneTask == null)
                {
                    return BadRequest();
                }

                ctx.Remove(milestoneTask);
                await ctx.SaveChangesAsync();
                return true;
            }
        }        

        // Remove milestone from task
        [HttpDelete("{taskId}/assignees/{userId}")]
        public async Task<ActionResult<bool>> RemoveAssigneeFromTask(string taskId, string userId)
        {
            using (var ctx = new AckeeCtx())
            {
                var task = await ctx.Tasks.FirstOrDefaultAsync(t => t.TaskID == taskId);
                var assignee = await ctx.ApplicationUser.FirstOrDefaultAsync(a => a.Id == userId);
                var userTask = await ctx.UserTasks.FirstOrDefaultAsync(ut => ut.UserID == userId && ut.TaskID == taskId);

                if (task == null || assignee == null || userTask == null)
                {
                    return BadRequest();
                }

                ctx.Remove(userTask);
                await ctx.SaveChangesAsync();
                return true;
            }
        }
    }
}
