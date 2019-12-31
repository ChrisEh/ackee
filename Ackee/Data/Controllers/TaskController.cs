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
            var ctx = new AckeeCtx();

            var users = ctx.ApplicationUser.ToList();
            var milestones = ctx.Milestones.ToList();

            return await ctx.Tasks
                .Include(t => t.MilestoneTasks)
                .Include(t => t.UserTasks)
                .ToListAsync();
        }
    }
}
