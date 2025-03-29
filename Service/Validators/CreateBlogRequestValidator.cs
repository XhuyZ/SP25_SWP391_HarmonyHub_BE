using Domain.DTOs.Requests;
using FluentValidation;

namespace Service.Validators
{
    public class CreateBlogRequestValidator : AbstractValidator<CreateBlogRequest>
    {
        public CreateBlogRequestValidator()
        {
            RuleFor(x => x.ImageUrl)
                .NotEmpty().WithMessage("ImageUrl cannot be empty");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Description cannot be empty");

            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Title cannot be empty");

            RuleFor(x => x.Content)
                .NotEmpty().WithMessage("Content cannot be empty");

            RuleFor(x => x.TherapistId)
                .NotEmpty().WithMessage("TherapistId cannot be empty");
        }
    }
}
