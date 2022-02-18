using System.ComponentModel.DataAnnotations;

namespace ToDoListAPI
{
    public class CategoryType
    {
        public int Id { get; set; }
        [StringLength(20)]
        public string CategoryName { get; set; } = string.Empty;
    }
}
