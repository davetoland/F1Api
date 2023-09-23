using FluentValidation;

namespace F1Api.Validation;

public class NameValidator : AbstractValidator<string>
{
    public NameValidator()
    {
        RuleFor(x => x)
            .NotNull()
            .MinimumLength(2)
            .Matches("[a-zA-Z]+")
            .Configure(x => x.SetDisplayName("name"));
    }
}
