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
    [Route("api/labels")]
    public class LabelController : ControllerBase
    {
        [HttpGet]
        public async Task<IEnumerable<AspNetLabels>> GetAllLabels()
        {
            var ctx = new AckeeCtx();

            return await ctx.Labels.ToListAsync();
        }

        [HttpPost]
        public async Task<object> CreateLabel([FromBody] AspNetLabels newLabel)
        {
            using (var ctx = new AckeeCtx())
            {
                if (newLabel.LabelName == null || newLabel.LabelColor == null)
                {
                    return BadRequest();
                }

                ctx.Labels.Add(newLabel);
                await ctx.SaveChangesAsync();
                return true;
            }
        }

        [HttpDelete("{labelId}")]
        public async Task<object> DeleteLabel([FromRoute] string labelId)
        {
            using (var ctx = new AckeeCtx())
            {
                var label = ctx.Labels.FirstOrDefault(l => l.LabelID == labelId);

                if (label == null)
                {
                    return BadRequest();
                }

                ctx.Remove(label);
                await ctx.SaveChangesAsync();
                return true;
            }
        }
    }
}
