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

        [HttpGet("user/{id}")]
        public async Task<IEnumerable<AspNetProjects>> GetUserProjects(string userId)
        {
            return await ctx.Projects.Where(p => p.UserProjects.Any(
                up => up.UserId == userId)).ToListAsync();
        }

        [HttpGet("create/{userId}/{projectName}")]
        public async Task<object> CreateProjectForOwner(string userId, string projectName)
        {
            // Get the user.
            var user = ctx.Users.FirstOrDefault(u => u.Id == userId);

            var existingProject = await ctx.UserProjects.FirstOrDefaultAsync(
                up => up.UserId == userId && up.Project.ProjectName == projectName);

            // Return if project for user already exists or userId is null.
            if (user == null || existingProject != null)
                return null;

            // Create the new project.
            var newProject = new AspNetProjects();
            newProject.ProjectID = ctx.Projects.Count().ToString();
            newProject.Owner = user;
            newProject.ProjectName = projectName;

            // Add project to DB.
            ctx.Projects.Add(newProject);
            await ctx.SaveChangesAsync();

            var savedProject = ctx.Projects.FirstOrDefault(
                p => p.ProjectID == newProject.ProjectID);

            var userProject = new UserProject()
            {
                Project = savedProject,
                ProjectId = savedProject.ProjectID,
                User = user,
                UserId = user.Id
            };

            ctx.UserProjects.Add(userProject);
            await ctx.SaveChangesAsync();
            return savedProject;
        }

        [HttpDelete("delete/{projectId}")]
        public async Task<bool> DeleteProject(string projectId)
        {
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

        [HttpGet("members/{projectId}")]
        public async Task<IEnumerable<ApplicationUser>> GetProjectMembers(string projectId)
        {
            // Get the project. 
            var users = await ctx.Users.Where(
                u => u.UserProjects.Any(up => up.ProjectId == projectId)).ToListAsync();

            if (users == null)
                return null;

            return users;
        }
    }
}
