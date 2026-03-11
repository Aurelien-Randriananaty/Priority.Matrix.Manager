namespace Shared.RequestFeatures;

public abstract class RequestParameters
{
    const int maxPageSize = 500000000;
    public int PageNumber { get; set; } = 1;
    private int _pageSize = 500000000;
    public int PageSize
    {
        get
        {
            return _pageSize;
        }
        set
        {
            _pageSize = (value > maxPageSize) ? maxPageSize : value;
        }
    }
}
