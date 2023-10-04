using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Razor_temp.Model
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [DisplayName("Category Name")]
        [MaxLength(35, ErrorMessage = "Must below 35 characters")]
        public string? Name { get; set; }
        [Range(1, 200, ErrorMessage = "Must be within 1-200")]
        [DisplayName("Display Order")]

        public int DisplayOrder { get; set; }
    }
}
