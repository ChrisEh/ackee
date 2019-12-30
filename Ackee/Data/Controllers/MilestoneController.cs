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
        [HttpGet("{milestoneId}")]
        public async Task<AspNetMilestones> GetProjectMilestones(string milestoneId)
        {
            var ctx = new AckeeCtx();

            // Get the milestones
            var milestone = await ctx.Milestones.FirstOrDefaultAsync(m => m.MilestoneID == milestoneId);

            return milestone;
        }
    }
}
