using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace expedia.com
{
    public interface IUser
    {
        string FirstName { get; set; }
        string LastName { get; set; }
        string Email { get; set; }
        string Password { get; set; }

        string GetFullName();
    }
    public interface IUserManager
    {
        IUser RegisterNewUser();
        IUser SignInUser();
    }

    
    public class UserManager : IUserManager
    {
        private IDictionary<string, IUser> users;

        public UserManager()
        {
            users = new Dictionary<string, IUser>();
        }

        public IUser RegisterNewUser()
        {
            var newUser = new User();

            Console.WriteLine("Enter your email:");
            newUser.Email = Console.ReadLine();  

            Console.WriteLine("Enter your First Name:");
            newUser.FirstName = Console.ReadLine();

            Console.WriteLine("Enter your Last Name:");
            newUser.LastName = Console.ReadLine();

            Console.WriteLine("Enter your Password:");
            newUser.Password = Console.ReadLine();  
           while(newUser.Password.Length<10)
            {
                Console.WriteLine("Password must be at least 10 characters long.");
                Console.WriteLine("Enter another password");
                newUser.Password = Console.ReadLine();
            }
            users.Add(newUser.Email, newUser);
            return newUser;
        }

        public IUser SignInUser()
        {
            Console.WriteLine(new string('=', 50));
            Console.Write("Enter your email: ");
            string email = Console.ReadLine();
            if (users.ContainsKey(email))
            {
                var user = users[email];
                Console.WriteLine("Enter your Password:");
                string password = Console.ReadLine();
                if (user.Password == password)
                {
                    return user;
                }
                else
                {
                    Console.WriteLine("Incorrect password.");
                }
            }
            else
            {
                Console.WriteLine("User not found.");
            }
            return null;
        }
    }
}
