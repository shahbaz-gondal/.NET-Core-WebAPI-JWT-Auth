using WebAPIAuth.Models;

namespace WebAPIAuth.BasicAuth
{
    public class ValidateUser
    {
        public static bool Login(string email, string password)
        {
            return Users.GetAllUsers().Any(u => u.Email == email && u.Password == password);
        }

        public static Users GetUser(string email, string password)
        {
            return Users.GetAllUsers().FirstOrDefault(u => u.Email == email && u.Password == password);
        }
    }
}
