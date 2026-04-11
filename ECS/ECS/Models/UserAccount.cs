using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECS.Models
{
    public class UserAccount
    {
        public int UserID { get; set; }
        public int EmployeeID { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public string Role { get; set; }
        public bool IsLocked { get; set; }
        public int FailedLoginAttempts { get; set; }

        public UserAccount()
        {
            Username = string.Empty;
            PasswordHash = string.Empty;
            Role = string.Empty;
            IsLocked = false;
            FailedLoginAttempts = 0;
        }

        public bool VerifyPassword(string password)
        {
            string hash = ECS.Security.PasswordHelper.HashPassword(password);
            return PasswordHash == hash;
        }

        public void RegisterFailedAttempt()
        {
            FailedLoginAttempts++;

            if (FailedLoginAttempts >= 3)
            {
                IsLocked = true;
            }
        }

        public void ResetFailedAttempts()
        {
            FailedLoginAttempts = 0;
        }
    }
}