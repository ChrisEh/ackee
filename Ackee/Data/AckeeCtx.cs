using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Ackee.Data
{
    public partial class AckeeCtx : IdentityDbContext
    {
        public AckeeCtx() : base()
        {
        }

        public AckeeCtx(DbContextOptions<AckeeCtx> options)
            : base(options)
        {
        }
    }
}
