namespace lievee.Models
{
    public class Users
    {
        public required int Id { get; set; }
        public required string Username { get; set; }
        public required UserRole Role { get; set; }
    }

    public enum UserRole
    {
        Admin,
        User
    }
}
