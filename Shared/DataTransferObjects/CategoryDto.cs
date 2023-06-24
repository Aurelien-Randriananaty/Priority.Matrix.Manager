namespace Shared.DataTransferObjects
{
    public record CategoryDto 
    { 
        public int Id { get; init; }
        public string? CategoryName { get; init; }
        public string? CategoryCode { get; init; }
    };
}