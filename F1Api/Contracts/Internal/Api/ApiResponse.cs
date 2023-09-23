using System.Text.Json.Serialization;

namespace F1Api.Models.Input.Api;

public record struct ApiResponse<T>(
    [property: JsonPropertyName("get")] string Get,
    [property: JsonPropertyName("parameters")] ApiParameters Parameters,
    [property: JsonPropertyName("errors")] IList<object> Errors,
    [property: JsonPropertyName("results")] int Results,
    [property: JsonPropertyName("response")] IList<T> Data
);
