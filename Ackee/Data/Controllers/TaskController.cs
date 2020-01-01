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
                var existingTask = ctx.Tasks.FirstOrDefault(t => t.TaskID == task.TaskID);

                if (task == null || existingTask != null)
                {
                    return BadRequest();
                }

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
                return await ctx.Tasks
                    .Include(t => t.MilestoneTasks)
                    .Include(t => t.UserTasks)
                    .Include(t => t.Project)
                    .Include(t => t.Project.UserProjects)
                    .FirstOrDefaultAsync(t => t.TaskID == taskId);
            }
        }
    }
}
