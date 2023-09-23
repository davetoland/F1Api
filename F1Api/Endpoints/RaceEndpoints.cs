using F1Api.Api;
using F1Api.Contracts.Ouput;
using F1Api.Mappers;
using F1Api.Models.Input;
using F1Api.Validation;

namespace F1Api.Endpoints;

public static class RaceEndpoints
{
    public static void MapRaceEndpoints(this WebApplication app)
    {
        app.MapGet("races/{year}", GetRacesByYear).WithOpenApi();
    }

    public static async Task<IResult> GetRacesByYear(int year, ApiService<Race> service, IModelMapper<Race, RaceDto> mapper, YearValidator validator, CancellationToken token)
    {
        var validation = await validator.ValidateAsync(year, token);
        if (!validation.IsValid)
            return Results.BadRequest(validation.Errors.Select(x => x.ErrorMessage));

        var (success, failed, races) = await service.Get($"races?season={year}", token);

        return (success, failed) switch
        {
            (true, _) => TypedResults.Ok(mapper.Map(races)),
            (false, false) => TypedResults.NoContent(),
            (false, true) => TypedResults.Problem("Failed to retrieve Race information", statusCode: 500)
        };
    }
}
