using Domain.DTOs.Requests;
using FluentValidation;

namespace Service.Validators
{
    public class UpdateBlogRequestValidator : AbstractValidator<UpdateBlogRequest>
    {
        public UpdateBlogRequestValidator()
        {
            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Description cannot be empty");

            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Title cannot be empty");

            RuleFor(x => x.Content)
                .NotEmpty().WithMessage("Content cannot be empty");
        }
    }
}
