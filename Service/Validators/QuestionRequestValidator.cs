using Domain.DTOs.Requests;
using FluentValidation;

namespace Service.Validators
{
    public class QuestionRequestValidator : AbstractValidator<QuestionRequest>
    {
        public QuestionRequestValidator()
        {
            RuleFor(x => x.Content)
                .NotEmpty().WithMessage("Content cannot be empty");

            RuleFor(x => x.Options)
                .NotEmpty().WithMessage("Options cannot be empty");
        }
    }
}
