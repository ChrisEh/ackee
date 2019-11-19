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

        public async Task<List<Projects>> GetProjects()
        {
            List<Projects> listOfProjects = new List<Projects>();

            return listOfProjects;
        }
    }
}
