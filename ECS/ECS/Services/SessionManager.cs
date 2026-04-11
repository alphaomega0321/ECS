using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECS.Models;

namespace ECS.Services
{
    public static class SessionManager
    {
        public static UserAccount CurrentUser { get; private set; }

        public static bool IsLoggedIn
        {
            get { return CurrentUser != null; }
        }

        public static void Login(UserAccount user)
        {
            CurrentUser = user;
        }

        public static void Logout()
        {
            CurrentUser = null;
        }
    }
}