using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ackee.Data.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Ackee.Data.Controllers
{
    [Produces("application/json")]
    [Route("api/users")]
    public class UserController : Controller
    {
        // GET: api/users
        [HttpGet]
        public async Task<IEnumerable<ApplicationUser>> Get()
        {
            using (var ctx = new AckeeCtx())
            {
                return await ctx.Users.ToListAsync();
            }            
        }

        // GET api/users/3137a909-73ee-4665-a74e-1ad574962795
        //[HttpGet("{userId}")]
        //public ApplicationUser GetUser(string id)
        //{
        //    return ctx.Users.FirstOrDefault(u => u.Id == id);
        //}

        // GET: api/users/3137a909-73ee-4665-a74e-1ad574962795
        [HttpGet("projects/{userId}")]
        public IEnumerable<AspNetProjects> GetUserprojects(string userId)
        {
            using (var ctx = new AckeeCtx())
            {
                return ctx.Projects.Where(
                p => p.UserProjects.Any(up => up.UserId == userId));
            }                
        }

        // GET: api/users/userEmail
        [HttpGet("{userEmail}")]
        public async Task<ApplicationUser> GetUserByEmail(string userEmail)
        {
            using (var ctx = new AckeeCtx())
            {
                return await ctx.Users.FirstOrDefaultAsync(u => u.Email == userEmail);
            }                
        }

        [HttpGet("{userEmail}/tasks")]
        public async Task<List<AspNetTasks>> GetUserTasks(string userEmail)
        {
            using (var ctx = new AckeeCtx())
            {
                return await ctx.Tasks
                    .Include(t => t.Project)
                    .Where(t => t.UserTasks.Any(ut => ut.User.Email == userEmail)).ToListAsync();                
            }
        }
    }
}