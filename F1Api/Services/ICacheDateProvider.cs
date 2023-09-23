namespace F1Api.Services;

public interface ICacheDateProvider
{
    Task<TimeSpan> GetCacheExpiration(CancellationToken cancellationToken);
}