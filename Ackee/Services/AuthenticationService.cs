using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ackee.Services
{
    public class AuthenticationService
    {
        private bool userIsAuthenticated = false;

        public void AuthenticateUser()
        {
            userIsAuthenticated = true;
        }

        public void LogoutUser()
        {
            userIsAuthenticated = false;
        }

        public bool GetUserAuthentication()
        {
            return userIsAuthenticated;
        }
    }
}
