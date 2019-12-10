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
    [Route("api/milestones")]
    public class MilestoneController : ControllerBase
    {
        AckeeCtx ctx = new AckeeCtx();

        [HttpGet]
        public async Task<IEnumerable<AspNetMilestones>> GetAllmilestones()
        {
            return await ctx.Milestones.ToListAsync();
        }

    }
}
