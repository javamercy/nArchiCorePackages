namespace Core.Application.Pipelines.Caching;

public interface ICachableRequest
{
    string CacheKey { get; }
    string? CacheGroupKey { get; }
    bool ByPassCache { get; }
    TimeSpan? SlidingExpiration { get; }
}
