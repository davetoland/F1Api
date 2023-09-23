namespace F1Api.Services;

public interface IDateTimeProvider
{
    abstract DateTime DateTimeNow { get; }
}