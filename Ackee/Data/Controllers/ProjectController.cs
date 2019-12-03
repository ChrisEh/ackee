using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ackee.Data.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Ackee.Data.Controllers
{
    [Produces("application/json")]
    [Route("api/projects")]
    public class ProjectController : ControllerBase
    {
        AckeeCtx ctx = new AckeeCtx();

        // GET api/projects
        [HttpGet]
        public async Task<IEnumerable<AspNetProjects>> GetAllProjects()
        {
            return await ctx.Projects.ToListAsync();
        }

        [HttpGet("{userId}")]
        public IEnumerable<AspNetProjects> GetUserProjects(string userId)
        {
            return ctx.Projects.Where(p => p.UserProjects.Any(up => up.UserId == userId));
        }
    }
}
