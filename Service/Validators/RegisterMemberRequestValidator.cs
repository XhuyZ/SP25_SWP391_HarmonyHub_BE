using Domain.DTOs.Requests;
using FluentValidation;

namespace Service.Validators;

public class RegisterMemberRequestValidator: AbstractValidator<RegisterMemberRequest>
{
    public RegisterMemberRequestValidator()
    {
        RuleFor(x => x.AvatarUrl)
            .NotEmpty().WithMessage("AvatarUrl cannot be empty");

        RuleFor(x => x.Email)
            .EmailAddress().WithMessage("Email must be in valid email format");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password cannot be empty");

        RuleFor(x => x.Phone)
            .NotEmpty().WithMessage("Phone cannot be empty")
            .Matches("^[0-9]*$").WithMessage("Phone only contains numbers");

        RuleFor(x => x.FirstName)
            .NotEmpty().WithMessage("FirstName cannot be empty");

        RuleFor(x => x.LastName)
            .NotEmpty().WithMessage("LastName cannot be empty");
    }
}