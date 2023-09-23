namespace F1Api.Contracts.Ouput;

public record struct DriverDto(
    string Name,
    string Nationality,
    int GrandsPrixEntered,
    int HighestFinish,
    int Podiums,
    int WorldChampionships,
    IReadOnlyCollection<TeamDto> Teams
);

public record struct TeamDto(
    string Name,
    int Season
);
