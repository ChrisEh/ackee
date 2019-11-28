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
    }
}
