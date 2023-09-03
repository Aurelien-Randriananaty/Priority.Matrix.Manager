namespace Shared.DataTransferObjects;

public record UserIdentitiesDto
{
    public Guid Id { get; init; }
    public string? FirstName { get; init; }
    public string? LastName { get; init; }
}
