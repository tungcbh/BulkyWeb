using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Bulky.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(30, ErrorMessage = "Name must less than 30 characters")]
        [DisplayName("Name:")]
        public string Name { get; set; }
        [Range(1, 100, ErrorMessage = "Display Order must be in 1 -> 100")]
        [DisplayName("Display Order:")]
        public string DisplayOrder { get; set; }
    }
}
