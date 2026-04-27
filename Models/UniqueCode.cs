namespace lievee.Models
{
    public class UniqueCode
    {
        public required Guid Code { get; set; }
        public bool IsUsed { get; set; } = false;
    }
}
