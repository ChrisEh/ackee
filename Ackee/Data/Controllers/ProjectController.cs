﻿using System;
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
        public IEnumerable<AspNetProjects> GetUserProjects(string userId)
        {
            return ctx.Projects.Where(p => p.UserProjects.Any(up => up.UserId == userId));
        }

        [HttpGet("{userId}/{projectName}")]
        public AspNetProjects CreateProjectForOwner (string userId, string projectName)
        {
            // Get the user.
            var user = ctx.Users.FirstOrDefault(u => u.Id == userId);
            var existingProjectForUser = ctx.Projects.FirstOrDefault(p => p.UserProjects.Any(u => u.UserId == userId));

            // Return if project for user already exists or userName is null.
            if (user == null && existingProjectForUser != null)
                return null;

            // Create the new project.
            var newProject = new AspNetProjects();
            newProject.ProjectID = ctx.Projects.Count().ToString();
            newProject.Owner = user;
            newProject.ProjectName = projectName;
            newProject.UserProjects = new List<UserProject>()
            {                
                new UserProject
                {
                    Project = newProject,
                    User = user
                }
            };

            // Add project to DB.
            ctx.Projects.Add(newProject);
            ctx.SaveChanges();
            return ctx.Projects.FirstOrDefault(p => p.ProjectID == newProject.ProjectID);
        }

        [HttpGet("{userId}/{projectName}"]

    }
}
