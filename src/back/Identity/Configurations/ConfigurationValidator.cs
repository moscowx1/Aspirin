using FluentValidation;

namespace Configurations;

public class ConnectionStringsValidator : AbstractValidator<ConnectionStrings>
{
    public ConnectionStringsValidator()
    {
        RuleFor(cs => cs.Identity).NotEmpty();
    }
}

public class ConfigurationValidator : AbstractValidator<Configuration>
{
    public ConfigurationValidator()
    {
        RuleFor(c => c.ConnectionStrings).SetValidator(new ConnectionStringsValidator());
    }
}
