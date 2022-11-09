namespace TicketSystemAPI.Entities.Dto
{
    public class TicketUnauthorizedDto
    {
        public int Id { get; set; }
        public string TicketName { get; set; } = string.Empty;
        public string TicketDescription { get; set; } = string.Empty;
        public int CategoryTypeId { get; set; }
        public int StatusId { get; set; } = 1;

    }
}
