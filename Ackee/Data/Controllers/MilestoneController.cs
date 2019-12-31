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
        public async Task<AspNetMilestones> GetMilestoneById(string milestoneId)
        {
            using (var ctx = new AckeeCtx())
            {
                // Get the milestone                
                var milestone = await ctx.Milestones
                    .Include(m => m.Project)
                    .FirstOrDefaultAsync(m => m.MilestoneID == milestoneId);

                return milestone;
            }            
        }

        [HttpPut("{milestoneId}")]
        public async Task<Object> UpdateMilestoneById([FromRoute] string milestoneId, [FromBody] AspNetMilestones updatedMilestone)
        {
            using (var ctx = new AckeeCtx())
            {
                if (string.IsNullOrEmpty(updatedMilestone.MilestoneName))
                {
                    return BadRequest();
                }

                // Get the milestone
                var milestone = await ctx.Milestones.FirstOrDefaultAsync(m => m.MilestoneID == milestoneId);                

                // Update the milestone
                milestone.MilestoneName = updatedMilestone.MilestoneName;
                milestone.Description = updatedMilestone.Description;
                milestone.EndDate = updatedMilestone.EndDate;
                milestone.Completed = updatedMilestone.Completed;

                // Save changes
                await ctx.SaveChangesAsync();
                return true;
            }
        }

        [HttpDelete("{milestoneId}")]
        public async Task<ActionResult<bool>> DeleteProjectMember(string milestoneId)
        {
            using (var ctx = new AckeeCtx())
            {
                var milestone = await ctx.Milestones.FirstOrDefaultAsync(m => m.MilestoneID == milestoneId);

                if (milestone == null)
                {
                    return BadRequest();
                }

                ctx.Remove(milestone);
                await ctx.SaveChangesAsync();
                return true;
            }
        }
    }
}
