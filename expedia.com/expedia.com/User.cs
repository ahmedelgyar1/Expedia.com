using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace expedia.com
{
    public class User : IUser
    {
        private string email;
        private string password;

        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string Email
        {
            get { return email; }
            set
            {
                if (value.Contains("@"))
                {
                    email = value;
                }
                else
                {
                    throw new ArgumentException("Invalid email format.");
                }
            }
        }

        public string Password
        {
             get { return password; }
             set
                { password = value; }
            
        }

        public string GetFullName()
        {
            return $"{FirstName} {LastName}";
        }

        public void UpdatePassword(string newPassword)
        {
            Password = newPassword;
        }
    }
}
