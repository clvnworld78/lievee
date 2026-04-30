namespace lievee.Models.Endpoint
{
    public class NewVisitorDataRequest
    {
        public required string Name { get; set; }
        public required string PhoneNumber { get; set; }
        public required DateOnly VisitDate { get; set; }
    }
}
