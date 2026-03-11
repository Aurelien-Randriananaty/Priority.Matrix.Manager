namespace Shared.DataTransferObjects
{
    public record TaskPriorityDto
    {
        public int Id { get; init; }
        public string? TaskTitle { get; init; }
        public string? TaskDescription { get; init; }
        public int TaskCreatedBy { get; init; }
        public DateTime? TaskToSee { get; init; }
        public DateTime CreatedDate { get; init; }
        public int? Hour { get; init; }
        public string? TaskStatus { get; init; }
        public int CategoryID { get; init; }
        public string? UserId { get; init; }
        public float? PosX { get; init; }
        public float? PosY { get; init; }
        public int? ZIndex { get; init; }
        public UserIdentitiesDto? User { get; init; }
        public CategoryDto? Category {get; init; }
    }
}
