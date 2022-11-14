using System;

namespace TicketSystemAPI.Entities.Dto
{
    public class TicketDto
    {
        public int Id { get; set; }
        public string TicketName { get; set; } = string.Empty;
        public string TicketDescription { get; set; } = string.Empty;
        public int CategoryTypeId { get; set; }
        public int StatusId { get; set; }
        public int AddedByUserId { get; set; }
        public int? ResponsibleUserId { get; set; } = null!;
        public DateTime CreatedOn { get; set; }
    }
}
