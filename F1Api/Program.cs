using F1Api.Api;
using F1Api.Contracts.Ouput;
using F1Api.Endpoints;
using F1Api.Mappers;
using F1Api.Middleware;
using F1Api.Models.Input;
using F1Api.Services;
using F1Api.Validation;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddScoped<CachedResponseHandler>();
builder.Services.AddTransient<UnhandledExceptionHandler>();

builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = "redis:6379";
    options.InstanceName = "f1api";
});

builder.Services.AddHttpClient<ApiClient>()
    .AddHttpMessageHandler<CachedResponseHandler>();

builder.Services.AddSingleton<ApiService<Race>>();
builder.Services.AddSingleton<ApiService<Driver>>();

builder.Services.AddTransient<YearValidator>();
builder.Services.AddTransient<NameValidator>();
builder.Services.AddTransient<IModelMapper<Race, RaceDto>, RaceMapper>();
builder.Services.AddTransient<IModelMapper<Driver, DriverDto>, DriverMapper>();
builder.Services.AddTransient<IDateTimeProvider, DefaultDateTimeProvider>();
builder.Services.AddTransient<ICacheDateProvider, DefaultCacheDateProvider>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseUnhandledExceptionHandler();
app.UseHttpsRedirection();
app.MapApiEndpoints();

app.Run();