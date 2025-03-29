using Domain.DTOs.Requests;
using FluentValidation;

namespace Service.Validators
{
    public class ResultRequestValidator : AbstractValidator<ResultRequest>
    {
        public ResultRequestValidator()
        {
            RuleFor(x => x.Content)
                .NotEmpty().WithMessage("Content cannot be empty");

            RuleFor(x => x.Type)
                .NotEmpty().WithMessage("Type cannot be empty")
                .InclusiveBetween(1, 4).WithMessage("Type must be between 1 and 4");
        }
    }

}
