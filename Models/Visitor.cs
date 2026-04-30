namespace lievee.Models
{
    public class Visitor
    {
        public required Guid LinkCode { get; set; }
        public required string Name { get; set; }
        public required string PhoneNumber { get; set; }
        public required DateOnly Date { get; set; }
    }
}
