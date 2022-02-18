using System.ComponentModel.DataAnnotations;
using ToDoListAPI.Entities;

namespace ToDoListAPI
{
    public class Task
    {
        public int Id { get; set; }
        [StringLength(20)]
        public string TaskName { get; set; } = string.Empty;
        public int CategoryTypeId { get; set; }
        public CategoryType CategoryType { get; set; }
        public string TaskDescription { get; set; } = string.Empty;
        public string Status { get; set; }
        public int AddedByUserId { get; set; }
        public User AddedByUser { get; set; }
        public int? ResponsibleUserId { get; set; } = null!;
        public User ResponsibleUser{ get; set; }



    }
}
