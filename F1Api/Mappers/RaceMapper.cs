using F1Api.Contracts.Ouput;
using F1Api.Models.Input;

namespace F1Api.Mappers;

public class RaceMapper : IModelMapper<Race, RaceDto>
{
    private const string RaceType = "Race";

    public IList<RaceDto> Map(IList<Race> races)
    {
        if (races is null)
            return Array.Empty<RaceDto>();

        return races
            .Where(x => x.Type is RaceType)
            .Select(x => new RaceDto(
                x.Competition.Name,
                x.Circuit.Name,
                $"{x.Competition.Location.City}, {x.Competition.Location.Country}",
                x.Distance,
                x.FastestLap.Time,
                x.Date.ToShortDateString(),
                x.Status))
            .ToList();
    }
}
