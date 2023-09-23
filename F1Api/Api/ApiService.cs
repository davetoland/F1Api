using FluentResults;

namespace F1Api.Api;

public class ApiService<T>
{
    private readonly ApiClient _api;
    private readonly ILogger<ApiService<T>> _logger;

    public ApiService(ApiClient api, ILogger<ApiService<T>> logger)
    {
        _api = api;
        _logger = logger;
    }

    public async Task<Result<IList<T>>> Get(string url, CancellationToken token)
    {
        var result = await _api.Get<T>(url, token);
        if (result.IsFailed)
            _logger.LogError("{message}", string.Join('|', result.Errors));

        return result;
    }
}
