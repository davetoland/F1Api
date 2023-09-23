using System.Text.Json.Serialization;

namespace F1Api.Models.Input;

public record struct Driver(
    [property: JsonPropertyName("id")] int Id,
    [property: JsonPropertyName("name")] string Name,
    [property: JsonPropertyName("abbr")] string Abbr,
    [property: JsonPropertyName("image")] string Image,
    [property: JsonPropertyName("nationality")] string Nationality,
    [property: JsonPropertyName("country")] Country Country,
    [property: JsonPropertyName("birthdate")] string Birthdate,
    [property: JsonPropertyName("birthplace")] string Birthplace,
    [property: JsonPropertyName("number")] int Number,
    [property: JsonPropertyName("grands_prix_entered")] int? GrandsPrixEntered,
    [property: JsonPropertyName("world_championships")] int WorldChampionships,
    [property: JsonPropertyName("podiums")] int Podiums,
    [property: JsonPropertyName("highest_race_finish")] HighestRaceFinish HighestRaceFinish,
    [property: JsonPropertyName("highest_grid_position")] int? HighestGridPosition,
    [property: JsonPropertyName("career_points")] string CareerPoints,
    [property: JsonPropertyName("teams")] IReadOnlyList<Contract> Teams
);

public record struct Contract(
    [property: JsonPropertyName("season")] int Season,
    [property: JsonPropertyName("team")] Team Team
);

public record struct Team(
    [property: JsonPropertyName("id")] int Id,
    [property: JsonPropertyName("name")] string Name,
    [property: JsonPropertyName("logo")] string Logo
);

public record struct Country(
    [property: JsonPropertyName("name")] string Name,
    [property: JsonPropertyName("code")] string Code
);

public record struct HighestRaceFinish(
    [property: JsonPropertyName("position")] int? Position,
    [property: JsonPropertyName("number")] int? Number
);
