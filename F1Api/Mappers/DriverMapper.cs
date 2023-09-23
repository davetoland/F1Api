using F1Api.Contracts.Ouput;
using F1Api.Models.Input;

namespace F1Api.Mappers;

public class DriverMapper : IModelMapper<Driver, DriverDto>
{
    public IList<DriverDto> Map(IList<Driver> drivers)
    {
        if (drivers is null)
            return Array.Empty<DriverDto>();

        return drivers.Select(x => 
            new DriverDto(
                x.Name,
                x.Nationality,
                x.GrandsPrixEntered ?? 0,
                x.HighestRaceFinish.Position ?? 0,
                x.Podiums,
                x.WorldChampionships,
                x.Teams?.Select(t =>
                    new TeamDto(t.Team.Name, t.Season)).ToList()
                        ?? new List<TeamDto>()))
            .ToList();
    }
}
