using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ackee.Data;
using Ackee.Data.Model;

namespace Ackee.Services
{
    public class ProjectService
    {
        private AckeeCtx _context;
        public ProjectService(AckeeCtx context)
        {
            _context = context;
        }

        public async Task<List<AspNetProjects>> GetProjects()
        {
            List<AspNetProjects> listOfProjects = await Task.Run(() => _context.Project.ToList());

            return listOfProjects;
        }
    }
}
