using System.ComponentModel.DataAnnotations;
using TicketSystemAPI.Entities;

namespace TicketSystemAPI
{
    public class Ticket
    {
        public int Id { get; set; }
        [StringLength(20)]
        public string TicketName { get; set; } = string.Empty;
        public int CategoryTypeId { get; set; }
        public CategoryType CategoryType { get; set; }
        public string TicketDescription { get; set; } = string.Empty;
        public string Status { get; set; }
        public int AddedByUserId { get; set; }
        public User AddedByUser { get; set; }
        public int? ResponsibleUserId { get; set; } = null!;
        public User ResponsibleUser{ get; set; }
    }
}
