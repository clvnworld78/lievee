namespace lievee.Models
{
    public class RegisteredVisitor
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required int PhoneNumber { get; set; }
        public required string Date { get; set; }
    }
}
