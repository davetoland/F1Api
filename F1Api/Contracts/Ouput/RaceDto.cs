namespace F1Api.Contracts.Ouput;

public record struct RaceDto(
    string Competition,
    string Circuit,
    string Location,
    string Distance,
    string FastestLap,
    string Date,
    string Status
);
