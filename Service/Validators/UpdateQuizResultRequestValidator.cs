using Domain.DTOs.Requests;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Validators
{
    public class UpdateQuizResultRequestValidator : AbstractValidator<UpdateQuizResultRequest>
    {
        public UpdateQuizResultRequestValidator()
        {
            RuleFor(x => x.Content)
                .NotEmpty().WithMessage("Content cannot be empty");

            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Id cannot be empty");

            RuleFor(x => x.Type)
                .NotEmpty().WithMessage("Type cannot be empty")
                .InclusiveBetween(1, 4).WithMessage("Type must be between 1 and 4");
        }
    }
}
