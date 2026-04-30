namespace lievee.Models
{
    public class RegisteredVisitor
    {
        public long? VisitorId { get; set; }
        public required long LinkId { get; set; }
        public required string Name { get; set; }
        public required string PhoneNumber { get; set; }
        public required DateOnly Date { get; set; }

        public static RegisteredVisitor NewVisitor(long linkId, string name, string phoneNumber, DateOnly visitDate)
        {
            return new RegisteredVisitor { LinkId = linkId, Name = name, PhoneNumber = phoneNumber, Date = visitDate };
        }

        public static RegisteredVisitor NewRegisteredVisitor(long visitorId, long linkId, string name, string phoneNumber, DateOnly visitDate)
        {
            return new RegisteredVisitor { VisitorId = visitorId, LinkId = linkId, Name = name, PhoneNumber = phoneNumber, Date = visitDate };
        }
    }
}
