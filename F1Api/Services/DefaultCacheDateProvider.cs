namespace F1Api.Services;

public class DefaultCacheDateProvider : ICacheDateProvider
{
    public async Task<TimeSpan> GetCacheExpiration(CancellationToken cancellationToken) =>
        await Task.FromResult(TimeSpan.FromDays(1));
}