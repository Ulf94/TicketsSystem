using System;
using System.ComponentModel.DataAnnotations;
using TicketSystemAPI.Entities;

namespace TicketSystemAPI
{
    public class Ticket
    {
        public int Id { get; set; }
        [StringLength(30)]
        public string TicketName { get; set; } = string.Empty;
        public string TicketDescription { get; set; } = string.Empty;
        public int CategoryTypeId { get; set; }
        public CategoryType CategoryType { get; set; }
        public int StatusId { get; set; } = (int)StatusesTypes.Pending;
        public Status Status { get; set; }
        public int AddedByUserId { get; set; }
        public User AddedByUser { get; set; }
        public int? ResponsibleUserId { get; set; } = null!;
        public User ResponsibleUser{ get; set; }
        public DateTime CreatedOn { get; set; }

    }
}
