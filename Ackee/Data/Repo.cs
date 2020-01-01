using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ackee.Data
{
    public static class Repo
    {
        private static AckeeCtx ctx = new AckeeCtx();

        public static string GetUserFirstName(string email)
        {
            var user = ctx.Users.FirstOrDefault(u => u.Email == email);

            return user.FirstName ?? null;
        }

        public static string GetUserLastName(string email)
        {
            var user = ctx.Users.FirstOrDefault(u => u.Email == email);

            return user.LastName ?? null;
        }
        public static string GetProjectID(string proid)
        {
            var project = ctx.Projects.FirstOrDefault(u => u.ProjectID==proid);

            return project.ProjectName;
        }

        public static List<string> GetAllProject()
        {
            List<string> projectname = new List<string>();
            var projects = ctx.Projects;

            foreach(var p in projects)
            {
                projectname.Add(p.ProjectName);
            }
            return projectname;
        }

    }
}
