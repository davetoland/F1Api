namespace F1Api.Services;

public class DefaultDateTimeProvider : IDateTimeProvider
{
    public DateTime DateTimeNow => DateTime.Now;
}
