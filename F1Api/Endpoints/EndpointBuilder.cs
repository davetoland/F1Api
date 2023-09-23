namespace F1Api.Endpoints;

public static class EndpointBuilder
{
    public static void MapApiEndpoints(this WebApplication app)
    {
        app.MapDriverEndpoints();
        app.MapRaceEndpoints();
    }
}