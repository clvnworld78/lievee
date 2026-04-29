namespace lievee.Models
{
    public class Users
    {
        public required long Id { get; set; }
        public required string Username { get; set; }
        public required byte[] Password { get; set; }
        public required UserRole Role { get; set; }
        public required bool Valid { get; set; }

        private Users() { }

        public static Users NewAuthenticatedUser(long id, string username, byte[] pass, UserRole role)
        {
            return new Users { Id = id, Username = username, Password = pass, Role = role, Valid = true };
        }

        public static Users NewInvalidUser()
        {
            return new Users { Id = 0, Username = "", Password = [], Role = UserRole.User, Valid = false };
        }

        public static Users NewLogin(long id, string username, byte[] hashedPass, UserRole role)
        {
            return new Users { Id = id, Username = username, Password = hashedPass, Role = role, Valid = true };
        }
    }

    public enum UserRole
    {
        Admin,
        User
    }
}
