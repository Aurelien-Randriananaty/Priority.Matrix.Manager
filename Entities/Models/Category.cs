using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models;

public class Category
{
    [Key]
    [Column("CategoryId")]
    public int Id { get; set; }
    [Required(ErrorMessage = "Category name is a required field")]
    [MaxLength(60, ErrorMessage = "Maximum length for the category name is 60 characters")]
    public string? CategoryName { get; set; }
    [Required(ErrorMessage = "Category code is a required field")]
    [MaxLength(60, ErrorMessage = "Maximum length for the category code is 60 characters")]
    public string? CategoryCode { get; set; }
    public ICollection<TaskPriority>? TaskPriorities { get; set; }
}
