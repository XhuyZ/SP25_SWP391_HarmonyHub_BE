using Domain.DTOs.Requests;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Validators
{
    public class UpdateQuizRequestValidator : AbstractValidator<UpdateQuizRequest>
    {
        public UpdateQuizRequestValidator()
        {
            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Description cannot be empty");

            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Title cannot be empty");
        }
    }
}
