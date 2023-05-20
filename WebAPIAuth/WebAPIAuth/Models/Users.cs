namespace WebAPIAuth.Models
{
    public class Users
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string Roles { get; set; }

        public static List<Users> GetAllUsers()
        {
            List<Users> users = new List<Users>()
            {
                new Users{Email="ali@gmail.com", Password="ali123", Roles="admin"},
                new Users{Email="abdul@gmail.com", Password="abdul123", Roles="user"},
                new Users{Email="hassan@gmail.com", Password="hassan123", Roles="employee"}
            };
            return users;
        }
    }
    public class Jwt
    {
        public string key { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public string Subject { get; set; }
    }
}
