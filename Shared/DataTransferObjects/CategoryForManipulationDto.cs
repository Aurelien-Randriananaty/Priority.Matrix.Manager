using System.ComponentModel.DataAnnotations;

namespace Shared.DataTransferObjects
{
    public record CategoryForManipulationDto
    {
        [Required(ErrorMessage = "Category name is a required field.")]
        [MaxLength(60, ErrorMessage = "Maximum length for the category name is 60 characters.")]
        public string? CategoryName { get; set; }
        [Required(ErrorMessage = "Category code is a required field.")]
        [MaxLength(60, ErrorMessage = "Maximum length for the category code is 60 characters.")]
        public string? CategoryCode { get; set; }
    }
}
