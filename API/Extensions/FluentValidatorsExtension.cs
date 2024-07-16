using API.Validators;
using FluentValidation;
using FluentValidation.AspNetCore;

namespace API.Extensions;

public static class FluentValidatorsExtension
{
    public static void AddFluentValidators(this IServiceCollection services)
    {
        services.AddFluentValidationAutoValidation()
            .AddFluentValidationClientsideAdapters();
        services.AddValidatorsFromAssemblyContaining<RegisterUserDtoValidator>();
    }
}