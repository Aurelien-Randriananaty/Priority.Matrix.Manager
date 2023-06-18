using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models;

public class TaskPriority
{
    [Key]
    [Column("TaskId")]
    public int Id { get; set; }
    [Required(ErrorMessage = "Task titre is a required field")]
    [MaxLength(60, ErrorMessage = "Maximum length for the titre is 60 characters")]
    public string? TaskTitre { get; set; }
    [MaxLength(255, ErrorMessage = "Maximum length for the titre is 255 characters")]
    public string? TaskDescription { get; set; }
    // TO DO: add proprity user to complice this task
    //public int UserId { get; set; }
    [Required(ErrorMessage = "Task createdBy is a required field")]
    public int TaskCreatedBy { get; set; }
    public DateTime? TaskToSee { get; set; }
    public DateTime CreatedDate { get; set; }
    public int? Hour { get; set; }
    public string? TaskStatus { get; set; }
    [ForeignKey(nameof(Category))]
    public int CategoryID { get; set; }
    public Category? Category { get; set; }
}