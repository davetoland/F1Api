using F1Api.Models.Input.Api;
using FluentResults;
using System.Text.Json;

namespace F1Api.Api;

public class ApiClient
{
    private const string _apiKeyHeader = "x-apisports-key";
    private const string _apiKeyName = "apisports:api-key";
    private const string _uriBase = "https://v1.formula-1.api-sports.io";

    protected readonly HttpClient _httpClient;
    protected readonly Uri _uri;

    public static readonly JsonSerializerOptions JsonOptions = new() { PropertyNameCaseInsensitive = true };

    public ApiClient(HttpClient httpClient, IConfiguration config)
    {
        var apiKey = config[_apiKeyName]
            ?? throw new ArgumentException("API Key not present in Config");

        _uri = new Uri(_uriBase);
        _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        _httpClient.DefaultRequestHeaders.Add(_apiKeyHeader, apiKey);
    }

    public async Task<Result<IList<T>>> Get<T>(string url, CancellationToken cancellationToken)
    {
        var request = new HttpRequestMessage(HttpMethod.Get, new Uri(_uri, url.Trim('/')));
        using var result = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, cancellationToken).ConfigureAwait(false);
        if (result == null)
            return Result.Fail("Unable to fetch results from API");

        using var contentStream = await result.Content.ReadAsStreamAsync(cancellationToken);
        var response = await JsonSerializer.DeserializeAsync<ApiResponse<T>>(contentStream, JsonOptions, cancellationToken);
        return response.Data.ToResult();
    }
}
