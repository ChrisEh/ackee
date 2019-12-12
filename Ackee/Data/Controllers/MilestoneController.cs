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
        public async Task<IEnumerable<AspNetMilestones>> GetAllMilestones()
        {
            return await ctx.Milestones.ToListAsync();
        }

        [HttpGet("delete/{milestoneId}")]
        public async Task<bool> DeleteMilestone(string mileStoneId)
        {
            var ms = await ctx.Milestones.FirstOrDefaultAsync(
                m => m.MilestoneID == mileStoneId);

            if (ms != null)
            {
                ctx.Remove(ms);
                await ctx.SaveChangesAsync();
                return true;
            }
            return false; 
        }
    }
}
