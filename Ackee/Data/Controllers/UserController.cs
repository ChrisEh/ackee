﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ackee.Data.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Ackee.Data.Controllers
{
    [Produces("application/json")]
    [Route("api/users")]
    public class UserController : Controller
    {
        AckeeCtx ctx = new AckeeCtx();

        // GET: api/users
        [HttpGet]
        public async Task<IEnumerable<ApplicationUser>> Get()
        {
            return await ctx.Users.ToListAsync();
        }

        // GET api/user/3137a909-73ee-4665-a74e-1ad574962795
        [HttpGet("{id}")]
        public ApplicationUser GetUser(string id)
        {
            return ctx.Users.FirstOrDefault(u => u.Id == id);
        }
    }
}