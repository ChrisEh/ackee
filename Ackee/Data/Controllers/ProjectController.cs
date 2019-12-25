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
    [Route("api/projects")]
    public class ProjectController : ControllerBase
    {
        //
        //  Project Endpoints
        //

        // GET api/projects
        [HttpGet]
        public async Task<IEnumerable<AspNetProjects>> GetAllProjects()
        {
            var ctx = new AckeeCtx();

            return await ctx.Projects.ToListAsync();
        }

        [HttpGet("{projectId}")]
        public async Task<ActionResult<AspNetProjects>> GetProjectById(string projectId)
        {
            using (var ctx = new AckeeCtx())
            {
                // Query user projects, so that they are in memory
                var userprojects = await ctx.UserProjects.Include(up => up.User).ToListAsync();

                return await ctx.Projects.Include(p => p.Owner)
                    .Include(p => p.UserProjects)
                    .FirstOrDefaultAsync(p => p.ProjectID == projectId);                
            }           
        }


        [HttpGet("user/{userId}")]
        public async Task<IEnumerable<AspNetProjects>> GetUserProjects(string userId)
        {
            using (var ctx = new AckeeCtx())
            {
                return await ctx.Projects.Where(p => p.UserProjects.Any(
                    up => up.UserId == userId)).ToListAsync();
            }                
        }

        [HttpPost("user/{userId}")]
        public async Task<object> CreateProjectForOwner([FromRoute] string userId, [FromBody] AspNetProjects project)
        {
            using (var ctx = new AckeeCtx())
            {
                // Get the user.
                var user = ctx.Users.FirstOrDefault(u => u.Id == userId);

                var existingProject = await ctx.UserProjects.FirstOrDefaultAsync(
                    up => up.UserId == userId &&
                        up.Project.ProjectName == project.ProjectName);

                // Return if project for user already exists or userId is null.
                if (user == null || existingProject != null)
                    return BadRequest();

                // Create the new project.
                var projectId = (ctx.Projects.Count() + 1).ToString();
                project.Owner = user;
                project.ProjectID = projectId;
                project.DateCreated = DateTime.Now;

                // Add project to DB.
                ctx.Projects.Add(project);

                var userProject = new UserProject()
                {
                    ProjectId = projectId,
                    UserId = user.Id
                };

                ctx.UserProjects.Add(userProject);
                await ctx.SaveChangesAsync();
                return ctx.Projects.FirstOrDefault(p => p.ProjectID == projectId);
            }
            
        }

        [HttpPut("user/{userId}")]
        public async Task<Object> UpdateProjectForOwner([FromRoute] string userId, [FromBody] AspNetProjects project)
        {
            using (var ctx = new AckeeCtx())
            {
                // Get the user.
                var user = ctx.Users.FirstOrDefault(u => u.Id == userId);

                // Get the UserProject
                var userProject = await ctx.UserProjects.FirstOrDefaultAsync(
                    up => up.UserId == userId &&
                        up.Project.ProjectID == project.ProjectID);

                // Get the project
                var existingProject = ctx.Projects.FirstOrDefault(p => p.ProjectID == project.ProjectID);

                // Return if project for user already exists or userId is null.
                if (user == null || userProject == null || user.Id != existingProject.Owner.Id || existingProject == null)
                    return BadRequest();

                // Update the project
                if (!string.IsNullOrWhiteSpace(project.ProjectName))
                {
                    existingProject.ProjectName = project.ProjectName;
                }

                if (!string.IsNullOrWhiteSpace(project.ProjectDescription))
                {
                    existingProject.ProjectDescription = project.ProjectDescription;
                }

                // Save changes
                await ctx.SaveChangesAsync();
                return true;
            }
        }        

        [HttpDelete("delete/{projectId}")]
        public async Task<bool> DeleteProject(string projectId)
        {
            var ctx = new AckeeCtx();

            // Get the project.
            var project = ctx.Projects.FirstOrDefault(p => p.ProjectID == projectId);

            if (project == null)
                return false;

            ctx.Projects.Remove(project);
            await ctx.SaveChangesAsync();
            return true;
        }

        [HttpDelete("delete/{ownerId}/{projectId}")]
        public async Task<bool> DeleteProjectOfOwner(string ownerId, string projectId)
        {
            var ctx = new AckeeCtx();

            // Get the user.
            var user = ctx.Users.FirstOrDefault(u => u.Id == ownerId);
            var existingProjectForUser = ctx.Projects.FirstOrDefault(
                p => p.ProjectID == projectId && p.UserProjects.Any(
                    up => up.UserId == ownerId));

            // Return if project for user already exists or userName is null.
            if (user == null || existingProjectForUser == null)
                return false;

            ctx.Projects.Remove(existingProjectForUser);
            await ctx.SaveChangesAsync();
            return true;
        }

        //
        // Project Member Endpoints
        //
        [HttpGet("{projectId}/members")]
        public async Task<IEnumerable<ApplicationUser>> GetProjectMembers(string projectId)
        {
            var ctx = new AckeeCtx();

            // Get the project. 
            var users = await ctx.Users.Where(
                u => u.UserProjects.Any(up => up.ProjectId == projectId)).ToListAsync();

            if (users == null)
                return null;

            return users;
        }

        [HttpPost("{projectId}/members")]
        public async Task<ActionResult<bool>> AddProjectMember(string projectId, [FromBody] string userEmail)
        {
            using (var ctx = new AckeeCtx())
            {
                var project = await ctx.Projects.FirstOrDefaultAsync(p => p.ProjectID == projectId);
                var user = await ctx.ApplicationUser.FirstOrDefaultAsync(u => u.Email == userEmail);

                if (user == null || project == null)
                {
                    return NotFound();
                }

                var userProject = new UserProject();
                userProject.ProjectId = projectId;
                userProject.UserId = user.Id;

                ctx.Add(userProject);
                await ctx.SaveChangesAsync();
                return true;
            }
        }

        [HttpDelete("{projectId}/members")]
        public async Task<ActionResult<bool>> DeleteProjectMember(string projectId, [FromBody] string userEmail)
        {
            using (var ctx = new AckeeCtx())
            {
                var project = await ctx.Projects.FirstOrDefaultAsync(p => p.ProjectID == projectId);
                var user = await ctx.ApplicationUser.FirstOrDefaultAsync(u => u.Email == userEmail);
                var userProject = await ctx.UserProjects.FirstOrDefaultAsync(up => up.ProjectId == projectId && up.UserId == user.Id);

                if (user == null || project == null || userProject == null)
                {
                    return BadRequest();
                }

                ctx.Remove(userProject);
                await ctx.SaveChangesAsync();
                return true;
            }
        }

        [HttpGet("addMilestone/{projectId}/{endDate}/{name}/{description}")]
        public async Task<ActionResult<AspNetMilestones>> AddMilestone(
            string projectId, DateTime endDate, string name, string description)
        {
            var ctx = new AckeeCtx();

            var project = await ctx.Projects.FirstOrDefaultAsync(p => p.ProjectID == projectId);

            if (project == null || string.IsNullOrWhiteSpace(name))
                return null;

            var newMilestone = new AspNetMilestones()
            {
                MilestoneID = (await ctx.Milestones.CountAsync()).ToString(),
                Description = description,
                EndDate = endDate,
                MilestoneName = name,
                Project = project
            };

            project.Milestones.ToList().Add(newMilestone);

            ctx.Update(project);

            await ctx.SaveChangesAsync();
            return CreatedAtAction("Milestone", await ctx.Milestones.FirstOrDefaultAsync(m => m.MilestoneID == newMilestone.MilestoneID));
        }
    }
}
