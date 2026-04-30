namespace lievee.Models.Endpoint
{
    public class UserLoginInfoRequest
    {
        public required string Username { get; set; }
        public required string Password { get; set; }
    }
}
