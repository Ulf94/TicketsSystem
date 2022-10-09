using System.ComponentModel.DataAnnotations;

namespace TicketSystemAPI
{
    public class CategoryType
    {
        public int Id { get; set; }
        [StringLength(20)]
        public string CategoryName { get; set; } = string.Empty;
    }
}
