using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ECS.Models;

namespace ECS.Services
{
    public class AuthenticationService
    {
        public UserAccount Login(string username, string password)
        {
            UserAccount user = DatabaseManager.Instance.UserAccounts
                .FirstOrDefault(u => u.Username.Equals(username, StringComparison.OrdinalIgnoreCase));

            if (user == null)
            {
                Console.WriteLine("Login failed. User not found.");
                return null;
            }

            if (user.IsLocked)
            {
                Console.WriteLine("Account is locked due to too many failed login attempts.");
                return null;
            }

            if (!user.VerifyPassword(password))
            {
                user.RegisterFailedAttempt();
                DatabaseManager.Instance.UpdateRecord("UserAccount");
                Console.WriteLine("Login failed. Invalid password.");
                return null;
            }

            user.ResetFailedAttempts();
            DatabaseManager.Instance.UpdateRecord("UserAccount");
            SessionManager.Login(user);

            Console.WriteLine("Login successful. Welcome, " + user.Username + ".");
            return user;
        }

        public void Logout()
        {
            if (SessionManager.IsLoggedIn)
            {
                Console.WriteLine("User " + SessionManager.CurrentUser.Username + " logged out.");
                SessionManager.Logout();
            }
        }
    }
}