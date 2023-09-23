using System.Text.Json.Serialization;

namespace F1Api.Models.Input;

public record struct Race(
    [property: JsonPropertyName("id")] int Id,
    [property: JsonPropertyName("competition")] Competition Competition,
    [property: JsonPropertyName("circuit")] Circuit Circuit,
    [property: JsonPropertyName("season")] int Season,
    [property: JsonPropertyName("type")] string Type,
    [property: JsonPropertyName("laps")] Laps Laps,
    [property: JsonPropertyName("fastest_lap")] FastestLap FastestLap,
    [property: JsonPropertyName("distance")] string Distance,
    [property: JsonPropertyName("timezone")] string Timezone,
    [property: JsonPropertyName("date")] DateTime Date,
    [property: JsonPropertyName("weather")] object Weather,
    [property: JsonPropertyName("status")] string Status
);

public record struct Circuit(
    [property: JsonPropertyName("id")] int Id,
    [property: JsonPropertyName("name")] string Name,
    [property: JsonPropertyName("image")] string Image
);

public record struct Competition(
    [property: JsonPropertyName("id")] int Id,
    [property: JsonPropertyName("name")] string Name,
    [property: JsonPropertyName("location")] Location Location
);

public record struct Location(
    [property: JsonPropertyName("country")] string Country,
    [property: JsonPropertyName("city")] string City);

public record struct FastestLap(
    [property: JsonPropertyName("driver")] FastestLapDriver Driver,
    [property: JsonPropertyName("time")] string Time
);

public record struct FastestLapDriver(
    [property: JsonPropertyName("driver")] int? Id
);

public record struct Laps(
    [property: JsonPropertyName("current")] object Current,
    [property: JsonPropertyName("total")] int? Total
);
