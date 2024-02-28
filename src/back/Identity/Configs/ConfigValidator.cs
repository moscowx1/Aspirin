using FluentValidation;

namespace Configs;

public class ConnectionStringsValidator : AbstractValidator<ConnectionStrings>
{
    public ConnectionStringsValidator()
    {
        RuleFor(cs => cs.Identity).NotEmpty();
    }
}

public class ConfigValidator : AbstractValidator<Config>
{
    public ConfigValidator()
    {
        RuleFor(c => c.ConnectionStrings).SetValidator(new ConnectionStringsValidator());
    }
}
