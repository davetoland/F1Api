using F1Api.Services;
using FluentValidation;

namespace F1Api.Validation;

public class YearValidator : AbstractValidator<int>
{
    public YearValidator(IDateTimeProvider dateTimeProvider)
    {
        RuleFor(x => x)
            .GreaterThanOrEqualTo(1950)
            .LessThanOrEqualTo(dateTimeProvider.DateTimeNow.Year)
            .Configure(x => x.SetDisplayName("year"));
    }
}
