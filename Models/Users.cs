namespace lievee.Models
{
    public class Users
    {
        public required int Id { get; set; }
        public required string Username { get; set; }
        public required UserRole Role { get; set; }
        public required bool Valid { get; set; }

        private Users() { }

        public static Users NewAuthenticatedUser(int id, string username, UserRole role)
        {
            return new Users { Id = id, Username = username, Role = role, Valid = true };
        }

        public static Users NewInvalidUser()
        {
            return new Users { Id = 0, Username = "", Role = UserRole.User, Valid = false };
        }
    }

    public enum UserRole
    {
        Admin,
        User
    }
}
