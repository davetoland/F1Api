using F1Api.Api;
using F1Api.Contracts.Ouput;
using F1Api.Mappers;
using F1Api.Models.Input;
using F1Api.Validation;

namespace F1Api.Endpoints;

public static class DriverEndpoints
{
    public static void MapDriverEndpoints(this WebApplication app)
    {
        app.MapGet("drivers/{name}", GetDriversByName).WithOpenApi();
    }

    public static async Task<IResult> GetDriversByName(string name, ApiService<Driver> service, IModelMapper<Driver, DriverDto> mapper, NameValidator validator, CancellationToken token)
    {
        var validation = await validator.ValidateAsync(name, token);
        if (!validation.IsValid)
            return Results.BadRequest(validation.Errors.Select(x => x.ErrorMessage));

        var (success, failed, drivers) = await service.Get($"drivers?search={name}", token);

        return (success, failed) switch
        {
            (true, _) => Results.Ok(mapper.Map(drivers)),
            (false, false) => Results.NoContent(),
            (false, true) => Results.Problem("Failed to retrieve Driver information", statusCode: 500)
        };
    }
}
