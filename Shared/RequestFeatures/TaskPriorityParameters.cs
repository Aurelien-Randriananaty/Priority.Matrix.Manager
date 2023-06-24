using Shared.RequestFeatures;

namespace Shared.RequestFeatures;

public class TaskPriorityParameters : RequestParameters
{
    public int MinHour { get; set; }
    public int MaxMax { get; set; } = int.MaxValue;
    public bool ValidHourRange => MaxMax > MinHour;
}
