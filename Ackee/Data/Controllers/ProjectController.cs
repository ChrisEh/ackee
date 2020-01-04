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
        #region PROJECT ENDPOINTS
        //
        //  Project Endpoints
        //

        // GET api/projects
        [HttpGet]
        public async Task<IEnumerable<AspNetProjects>> GetAllProjects()
        {
            var ctx = new AckeeCtx();

            return await ctx.Projects.OrderBy(p => p.DateCreated).ToListAsync();
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
                    .Include(p => p.Milestones)
                    .Include(p => p.Tasks)
                    .FirstOrDefaultAsync(p => p.ProjectID == projectId);                
            }           
        }


        [HttpGet("user/{userId}")]
        public async Task<IEnumerable<AspNetProjects>> GetUserProjects(string userId)
        {
            using (var ctx = new AckeeCtx())
            {
                var users = ctx.Users.ToList();
                var userProject = ctx.UserProjects.ToList();

                return await ctx.Projects.Where(p => p.UserProjects.Any(
                    up => up.UserId == userId)).OrderByDescending(p => p.DateCreated).ToListAsync();
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
                project.Owner = user;
                project.DateCreated = DateTime.Now;

                // Add project to DB.
                ctx.Projects.Add(project);

                var userProject = new UserProject()
                {
                    ProjectId = project.ProjectID,
                    UserId = user.Id
                };

                ctx.UserProjects.Add(userProject);
                await ctx.SaveChangesAsync();
                return ctx.Projects.FirstOrDefault(p => p.ProjectID == project.ProjectID);
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

        [HttpDelete("{projectId}")]
        public async Task<Object> DeleteProject(string projectId)
        {
            var ctx = new AckeeCtx();

            // Get the project.
            var project = ctx.Projects.FirstOrDefault(p => p.ProjectID == projectId);

            if (project == null)
                return BadRequest();

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

        #endregion

        #region PROJECT MEMBER ENDPOINTS

        //
        // Project Member Endpoints
        //
        [HttpGet("{projectId}/members")]
        public async Task<IEnumerable<ApplicationUser>> GetProjectMembers(string projectId)
        {
            var ctx = new AckeeCtx();

            // Get the members. 
            var users = await ctx.Users.Where(
                u => u.UserProjects.Any(up => up.ProjectId == projectId)).ToListAsync();

            if (users == null)
                return null;

            return users;
        }

        [HttpPost("{projectId}/members/{userEmail}")]
        public async Task<ActionResult<bool>> AddProjectMember(string projectId, string userEmail)
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

                ctx.UserProjects.Add(userProject);
                await ctx.SaveChangesAsync();
                return true;
            }
        }

        [HttpDelete("{projectId}/members/{userEmail}")]
        public async Task<ActionResult<bool>> DeleteProjectMember(string projectId, string userEmail)
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

        #endregion

        #region PROJECT MILESTONE ENDPOINTS
        [HttpGet("{projectId}/milestones")]
        public async Task<IEnumerable<AspNetMilestones>> GetProjectMilestones(string projectId)
        {
            var ctx = new AckeeCtx();

            // Get the milestones
            var milestones = await ctx.Milestones
                .Where(m => m.Project.ProjectID == projectId)
                .OrderBy(m => m.EndDate)
                .ToListAsync();

            return milestones;
        }


        // This method expects a Milestone object to be sent in the body of the request.
        // This object can have null for the project and the milestone id, as they will be set inside this method.
        [HttpPost("{projectId}/milestones")]
        public async Task<Object> AddProjectMilestone([FromRoute] string projectId, [FromBody] AspNetMilestones newMilestone)
        {
            using (var ctx = new AckeeCtx())
            {
                var project = ctx.Projects.FirstOrDefault(p => p.ProjectID == projectId);

                if (project == null || string.IsNullOrWhiteSpace(newMilestone.MilestoneName))
                    return BadRequest();

                newMilestone.Project = project;

                ctx.Milestones.Add(newMilestone);
                await ctx.SaveChangesAsync();
                return newMilestone;
            }            
        }        

        [HttpDelete("{projectId}/milestones/{milestoneId}")]
        public async Task<ActionResult<bool>> DeleteProjectMilestone(string projectId, string milestoneId)
        {
            using (var ctx = new AckeeCtx())
            {
                var project = await ctx.Projects.FirstOrDefaultAsync(p => p.ProjectID == projectId);
                var milestone = await ctx.Milestones.FirstOrDefaultAsync(m => m.MilestoneID == milestoneId);                

                if (milestone == null || project == null)
                {
                    return BadRequest();
                }

                ctx.Remove(milestone);
                await ctx.SaveChangesAsync();
                return true;
            }
        }

        [HttpGet("{projectId}/milestones/{milestoneId}/tasks")]
        public async Task<IEnumerable<AspNetTasks>> GetMilestoneTasks(string projectId, string milestoneId)
        {
            using (var ctx = new AckeeCtx())
            {
                var users = ctx.ApplicationUser.ToList();
                var milestones = ctx.Milestones.ToList();

                // Get the tasks
                var tasks = await ctx.Tasks
                    .Include(t => t.MilestoneTasks)
                    .Include(t => t.UserTasks)
                    .Include(t => t.Project)
                    .Include(t => t.Project.UserProjects)
                    .Where(t => t.Project.ProjectID == projectId && t.MilestoneTasks.Any(mt => mt.MilestoneID == milestoneId))
                    .OrderBy(t => t.EndDate)
                    .ToListAsync();

                return tasks;
            }
        }

        #endregion

        #region PROJECT TASKS ENDPOINTS
        [HttpGet("{projectId}/tasks")]
        public async Task<IEnumerable<AspNetTasks>> GetProjectTasks(string projectId)
        {
            var ctx = new AckeeCtx();

            var users = ctx.ApplicationUser.ToList();
            var milestones = ctx.Milestones.ToList();

            // Get the tasks
            var tasks = await ctx.Tasks
                .Include(t => t.MilestoneTasks)
                .Include(t => t.UserTasks)
                .Include(t => t.Project)
                .Include(t => t.Project.UserProjects)
                .Where(t => t.Project.ProjectID == projectId)
                .OrderBy(t => t.EndDate)
                .ToListAsync();

            return tasks;
        }

        // Add milestone to task
        [HttpPost("{projectId}/tasks/{taskName}/milestones")]
        public async Task<object> AddMilestoneToTask([FromRoute] string projectId, [FromRoute] string taskName, [FromBody] string milestoneId)
        {
            using (var ctx = new AckeeCtx())
            {
                var task = await ctx.Tasks.FirstOrDefaultAsync(t => t.TaskName == taskName && t.Project.ProjectID == projectId);
                var milestone = await ctx.Milestones.FirstOrDefaultAsync(m => m.MilestoneID == milestoneId);

                if (task == null || milestone == null)
                {
                    return NotFound();
                }

                var milestoneTask = new MilestoneTask();
                milestoneTask.MilestoneID = milestone.MilestoneID;
                milestoneTask.TaskID = task.TaskID;

                ctx.MilestoneTasks.Add(milestoneTask);
                await ctx.SaveChangesAsync();
                return true;
            }
        }

        // Add assignee to task
        [HttpPost("{projectId}/tasks/{taskName}/assignees")]
        public async Task<ActionResult<bool>> AddAssigneeToTask([FromRoute] string projectId, [FromRoute] string taskName, [FromBody] string userId)
        {
            using (var ctx = new AckeeCtx())
            {
                var task = await ctx.Tasks.FirstOrDefaultAsync(t => t.TaskName == taskName && t.Project.ProjectID == projectId);
                var assignee = await ctx.ApplicationUser.FirstOrDefaultAsync(a => a.Id == userId);

                if (task == null || assignee == null)
                {
                    return NotFound();
                }

                var userTask = new UserTask();
                userTask.UserID = assignee.Id;
                userTask.TaskID = task.TaskID;

                ctx.UserTasks.Add(userTask);
                await ctx.SaveChangesAsync();
                return true;
            }
        }
        #endregion

    }
}
