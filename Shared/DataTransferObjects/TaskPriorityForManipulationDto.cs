using System.ComponentModel.DataAnnotations;

namespace Shared.DataTransferObjects
{
    public record TaskPriorityForManipulationDto
    {
        [Required(ErrorMessage = "Task title is a required field.")]
        [MaxLength(60, ErrorMessage = "Maximum length for the title is 60 characters.")]
        public string? TaskTitle { get; init; }
        [Required(ErrorMessage = "Task description is a required field.")]
        [MaxLength(255, ErrorMessage = "Maximum length for the description is 255 characters.")]
        public string? TaskDescription { get; init; }
        [Required(ErrorMessage = "Task createdBy is a required field.")]
        [Range(1, int.MaxValue, ErrorMessage = "Age is required and it can't be lower than 0")]
        public int TaskCreatedBy { get; init; }
        public DateTime? TaskToSee { get; init; }
        [Required(ErrorMessage = "Task created date is a required field.")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "yyyy-MM-ddTHH:mm:ss")]
        public DateTime? CreatedDate { get; init; }
        [Range(0, int.MaxValue, ErrorMessage = "Age is required and it can't be lower than equal 0")]
        public int? Hour { get; init; }
        public string? TaskStatus { get; init; }
    }
}
