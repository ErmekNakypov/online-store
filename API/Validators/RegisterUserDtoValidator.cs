using FluentValidation;
using Model.Dtos.Identity;

namespace API.Validators;

public class RegisterUserDtoValidator : AbstractValidator<RegisterUserDto>
{
    public RegisterUserDtoValidator()
    {
        RuleFor(x => x.PersonName)
            .NotEmpty().WithMessage("Person Name can't be blank");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email can't be blank")
            .EmailAddress().WithMessage("Email should be in proper format");

        RuleFor(x => x.PhoneNumber)
            .NotEmpty().WithMessage("Phone number can't be blank")
            .Matches("^[0-9]*$").WithMessage("Phone number should be in proper format");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password can't be blank");

        RuleFor(x => x.ConfirmPassword)
            .NotEmpty().WithMessage("Confirm password can't be blank")
            .Equal(x => x.Password).WithMessage("Password and Confirm Password should match");
    }
}