using System.Text.Json.Serialization;

namespace F1Api.Models.Input.Api;

public record struct ApiParameters(
    [property: JsonPropertyName("search")] string Search,
    [property: JsonPropertyName("season")] string Season,
    [property: JsonPropertyName("next")] string Next
);
