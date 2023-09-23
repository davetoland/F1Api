using F1Api.Services;
using FluentResults;
using Microsoft.Extensions.Caching.Distributed;
using System.Text;
using System.Web;

namespace F1Api.Middleware;

public class CachedResponseHandler : DelegatingHandler
{
    private readonly IDistributedCache _cache;
    private readonly ICacheDateProvider _cacheDateProvider;

    public CachedResponseHandler(IDistributedCache cache, ICacheDateProvider cacheDateProvider)
    {
        _cache = cache;
        _cacheDateProvider = cacheDateProvider;
    }

    protected async override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var cacheKey = BuildCacheKey(request).ValueOrDefault; 
        if (string.IsNullOrWhiteSpace(cacheKey))
            return await base.SendAsync(request, cancellationToken);

        var cacheRequest = await GetContentFromCache(cacheKey, cancellationToken);
        if (cacheRequest.IsSuccess)
            return cacheRequest.Value;

        var response = await base.SendAsync(request, cancellationToken);
        await CacheResponseContent(response, cacheKey, cancellationToken);

        return response;
    }

    private static Result<string> BuildCacheKey(HttpRequestMessage request)
    {
        if (request.RequestUri is not null)
            return HttpUtility.HtmlEncode(request.RequestUri.PathAndQuery);

        return Result.Fail("Cannot build cache key");
    }

    private async Task<Result<HttpResponseMessage>> GetContentFromCache(string key, CancellationToken cancellationToken)
    {
        var cached = await _cache.GetAsync(key, cancellationToken);
        if (cached is not null)
        {
            var cacheString = Encoding.UTF8.GetString(cached);
            var message = new HttpResponseMessage { Content = new StringContent(cacheString) };
            return message.ToResult();
        }

        return Result.Fail("Value is not in cache");
    }

    private async Task CacheResponseContent(HttpResponseMessage response, string cacheKey, CancellationToken cancellationToken)
    {
        var content = await response.Content.ReadAsStringAsync(cancellationToken);
        var cacheDate = await _cacheDateProvider.GetCacheExpiration(cancellationToken);

        await _cache.SetStringAsync(cacheKey, content,
            new DistributedCacheEntryOptions { AbsoluteExpirationRelativeToNow = cacheDate },
            cancellationToken);
    }
}
