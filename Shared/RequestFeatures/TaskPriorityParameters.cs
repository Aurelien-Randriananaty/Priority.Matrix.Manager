using Shared.RequestFeatures;

namespace Shared.RequestFeatures;

public class TaskPriorityParameters : RequestParameters
{
    public int MinHour { get; set; }
    public int MaxHour { get; set; } = int.MaxValue;
    public bool ValidHourRange => MaxHour > MinHour;

    public string? SearchTerm { get; set; }
}
