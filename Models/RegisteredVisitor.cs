namespace lievee.Models
{
    public class RegisteredVisitor
    {
        public int? VisitorId { get; set; }
        public required int LinkId { get; set; }
        public required string Name { get; set; }
        public required int PhoneNumber { get; set; }
        public required DateOnly Date { get; set; }

        public static RegisteredVisitor NewVisitor(int linkId, string name, int phoneNumber, DateOnly visitDate)
        {
            return new RegisteredVisitor { LinkId = linkId, Name = name, PhoneNumber = phoneNumber, Date = visitDate };
        }

        public static RegisteredVisitor NewRegisteredVisitor(int visitorId, int linkId, string name, int phoneNumber, DateOnly visitDate)
        {
            return new RegisteredVisitor { VisitorId = visitorId, LinkId = linkId, Name = name, PhoneNumber = phoneNumber, Date = visitDate };
        }
    }
}
